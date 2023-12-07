using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection;
using Bushido.TestTask.Library.Core.Extensions;
using Bushido.TestTask.Cloud.Authentication.Auth;

namespace Bushido.TestTask.Library.Core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseTestTaskController : ControllerBase
    {
        [HttpGet]
        [Route("ping")]
        [AllowAnonymous]
        public IActionResult Ping()
        {
            return new ContentResult
            {
                Content = @$"
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <meta charset=""utf-8"" />
                        <title>Bushido.TestTask | WEB API</title>
                    </head>
                    <body>
                        <div style=""display: flex;
                        justify-content: center;
                        align-items: center;
                        flex-direction: column;"">

                            <div><h1>Pong</h1></div>
                            <div><p>{this.GetType().Name} are working; Build Version {Assembly.GetEntryAssembly().GetName().Version}</p></div>
                        </div>
                    </body>
                    </html>",
                ContentType = "text/html"
            };
        }
    }
}
