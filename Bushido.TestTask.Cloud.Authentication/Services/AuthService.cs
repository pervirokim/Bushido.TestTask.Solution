using Bushido.TestTask.Cloud.Authentication.Auth;
using Bushido.TestTask.Cloud.Authentication.Interfaces;
using Bushido.TestTask.Cloud.Authentication.Models;
using Bushido.TestTask.Cloud.Authentication.Models.DBModel;
using Bushido.TestTask.Library.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bushido.TestTask.Cloud.Authentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContext;
        private readonly AuthenticationDbContext _authenticationDbContext;

        public AuthService(IConfiguration configuration, IHttpContextAccessor httpContext, AuthenticationDbContext authenticationDbContext)
        {
            _configuration = configuration;
            _httpContext = httpContext;
            _authenticationDbContext = authenticationDbContext;
        }


        public async Task<JWTtoken> SignUp(SignUpModel signUpModel)
        {
            JWTtoken? accessToken = null;
            User user = await _authenticationDbContext.Users.FirstOrDefaultAsync(u => u.Email == signUpModel.Email);
            if (user == null)
            {
                user = new User(signUpModel.Email, signUpModel.FirstName, signUpModel.LastName, PasswordHasher.HashPassword(signUpModel.Password));
                _authenticationDbContext.Users.Add(user);
                _authenticationDbContext.SaveChanges();
                accessToken = GenerateToken(user.Id);
            }

            return accessToken;
        }


        public async Task<JWTtoken> SignIn(SignInModel signInModel)
        {
            JWTtoken? accessToken = null;
            User user = await _authenticationDbContext.Users.FirstOrDefaultAsync(u => u.Email == signInModel.Email);
            if (user != null && PasswordHasher.VerifyPassword(signInModel.Password, user.Passwordhash))
                accessToken = GenerateToken(user.Id);

            return accessToken;
        }


        public JWTtoken GenerateToken(string userId)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetValue<string>("TokenSignIn")));
            User? user = _authenticationDbContext.Users.FirstOrDefault(u => u.Id == userId);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                { new Claim(AuthOptions.IdClaim, user.Id),
                    new Claim(AuthOptions.EmailClaim, user.Email),
                    new Claim(AuthOptions.NameClaim, user.Email),
                    new Claim(AuthOptions.FirstNameClaim, user.Firstname ?? ""),
                    new Claim(AuthOptions.LastNameClaim, user.LastName ?? ""),
                }),

                Expires = DateTime.UtcNow.AddMinutes(AuthOptions.TokenExpirationInMinutes),
                TokenType = AuthOptions.BearerToken,
                Issuer = AuthOptions.ISSUER,
                Audience = AuthOptions.AUDIENCE,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            JWTtoken? accessToken = null;

            if (!token.IsEmpty())
            {
                List<JWTtoken> tokensFromDb = _authenticationDbContext.AccessTokens.Where(t => t.UserId == userId).ToList();

                if (tokensFromDb.Count > 0)
                    _authenticationDbContext.AccessTokens.RemoveRange(tokensFromDb);

                accessToken = new JWTtoken() { Id = Guid.NewGuid().ToString(), RefreshToken = Guid.NewGuid().ToString().ToLower(), IdToken = token, UserId = userId };

                _authenticationDbContext.AccessTokens.Add(accessToken);
                _authenticationDbContext.SaveChanges();
            }
            return accessToken;
        }

        public JWTtoken RefreshToken(string refreshToken)
        {
            JWTtoken? accessToken = null;
            User currentUser = GetCurrentUser();
            if (!currentUser.IsEmpty())
            {
                JWTtoken accessTokenFromDb = _authenticationDbContext.AccessTokens.FirstOrDefault(t => t.UserId == GetCurrentUser().Id && t.RefreshToken == refreshToken);
                if (!accessTokenFromDb.IsEmpty())
                    accessToken = GenerateToken(currentUser.Id);
            }
            return accessToken;
        }

        public JWTtoken ValidateAccessTokenAndGet(string token)
        {
            JWTtoken? accessToken = null;
            if (ValidateAccessToken(token))
                accessToken = _authenticationDbContext.AccessTokens.FirstOrDefault(t => t.IdToken == token);

            return accessToken;
        }

        public bool ValidateAccessToken(string token)
        {
            string key = _configuration.GetValue<string>("TokenSignIn");
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidAudience = AuthOptions.AUDIENCE,
                    IssuerSigningKey = securityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }


        public Claim GetClaim(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var stringClaim = securityToken.Claims.First(claim => claim.Type == claimType);
            return stringClaim;
        }

        public User? GetCurrentUser()
        {
            User? user = null;
            if (!_httpContext.HttpContext.User.Identity.IsEmpty())
                user = _authenticationDbContext.Users.FirstOrDefault(u => u.Email == _httpContext.HttpContext.User.Identity.Name);

            return user;
        }

    }
}
