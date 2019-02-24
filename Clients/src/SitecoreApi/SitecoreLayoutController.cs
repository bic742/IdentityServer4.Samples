using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SitecoreApi
{
    [Route("sitecorelayout")]
    [Authorize]
    public class SitecoreLayoutController : ControllerBase
    {
        public async Task<IActionResult> Get()
        {
            var client = new HttpClient();
            var content = await client.GetStringAsync("http://sitecore910.sitecore/sitecore/api/layout/render/jss?item=/&sc_apikey={9B8A0FCF-BA5A-483E-9AB0-E263866B9EAF}");
            return new JsonResult(JsonConvert.DeserializeObject(content));
        }
    }
}