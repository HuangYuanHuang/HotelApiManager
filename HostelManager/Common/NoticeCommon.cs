using MessageWeb.Config;
using MessageWeb.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HostelManager.Common
{
    public class NoticeCommon
    {
        private readonly IOptions<JpushAppSettings> options;

        public NoticeCommon(IOptions<JpushAppSettings> _options)
        {
            this.options = _options;
        }
        public async Task<object> SendNotice(NoticeModel value)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", options.Value.Base64);
                var httpContent = new StringContent(value.ToJson());
                var data = await httpClient.PostAsync(options.Value.SmsMessage.ApiUrl, httpContent);
                var result = await data.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<object>(result);
            }
        }
    }
}
