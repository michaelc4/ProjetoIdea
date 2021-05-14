using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Api.Service.Util
{
    public class ContentHelper
    {
        public static StringContent GetStringContent(object obj) => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");

        public static dynamic GetObject(string obj) => JsonConvert.DeserializeObject<dynamic>(obj);
    }
}
