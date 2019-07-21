using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiAiSDK;
using ApiAiSDK.Model;

namespace DialogControl.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ApiAi apiAi;
       

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var config = new AIConfiguration("39a7903370124c139fd317f5e1e78bec", SupportedLanguage.English);
            apiAi = new ApiAi(config);
            var response = apiAi.TextRequest("What is your name");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var config = new AIConfiguration("39a7903370124c139fd317f5e1e78bec", SupportedLanguage.English);
            apiAi = new ApiAi(config);
            var response = apiAi.TextRequest("hello");
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
