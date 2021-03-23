using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        IRegisterService _registerService;
        public RegisterController(IRegisterService dealer)
        {
            _registerService = dealer;
        }

        [HttpPost]
        public ActionResult<string> Send(Policy policy)
        {
            _registerService.SendMessage(policy);
            return CreatedAtAction("Register", policy.Type);
        }
    }

    
}
