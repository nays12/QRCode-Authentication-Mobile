using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCodeAuthMobile;
using QRCodeAuthMobile.Models;

namespace MobileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredentiaslController : ControllerBase
    {
        // GET: api/Credentiasl
        [HttpGet]
		[Route("api/Events/GetAllCredentials")]
		public async Task<List<Credential>> Get()
        {
			List<Credential> credentials = await App.CredentialRepo.GetAllCredentialsAsync();
			return credentials;
		}

        // GET: api/Credentiasl/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Credentiasl
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Credentiasl/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
