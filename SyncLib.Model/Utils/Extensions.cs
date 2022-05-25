using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncLib.Model.Common
{
    public static class Extensions
    {
        public static T ToObject<T>(this string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static string ToJsonString<T>(this T data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
