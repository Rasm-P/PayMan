using PayManApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PayManApi.Controllers
{
    public class AccountController : ApiController
    {
        Account account = new Account("UserOne", "oooops");

        [HttpGet]
        public IHttpActionResult GetProduct()
        {
            return Ok(account);
        }
    }
}
