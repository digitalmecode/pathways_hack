using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Configuration;

namespace Microsoft.Bot.Sample.FormBot
{
    public static class ObjectExtensions
    {
        public static async Task<string> PostAsJsonToApi(this object jsonObject, string apiAppSetting)
        {
            var json = JsonConvert.SerializeObject(jsonObject);
            var requestData = new StringContent(json, Encoding.UTF8, "application/json");
            string logicAppsUrl = ConfigurationManager.AppSettings[apiAppSetting];
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(logicAppsUrl, requestData);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}