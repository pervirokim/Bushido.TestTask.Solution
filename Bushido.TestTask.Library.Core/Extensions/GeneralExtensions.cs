using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bushido.TestTask.Library.Core.Extensions
{
    public static class GeneralExtensions
    {

        public static string TrimIfNull(this object text)
        {
            return text == null ? string.Empty : text.ToString().Trim();
        }

        public static bool IsEmpty(this object text)
        {
            return (text == null || string.IsNullOrEmpty(text.TrimIfNull()));
        }

        public static string ToJson(this object obj)
        {
            return obj != null ? JsonConvert.SerializeObject(obj) : null;
        }

        public static T CloneJson<T>(this T source)
        {
            return source.CloneJson<T, T>();
        }

        /// <summary>
        /// Perform a deep Copy of the object, using Json as a serialisation method. NOTE: Private members are not cloned using this method.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static R CloneJson<T, R>(this T source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
                return default(R);

            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

            return JsonConvert.DeserializeObject<R>(JsonConvert.SerializeObject(source), deserializeSettings);
        }

        public static R CloneStringJson<R>(this string source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
                return default(R);

            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

            return JsonConvert.DeserializeObject<R>(source, deserializeSettings);
        }
    }
}
