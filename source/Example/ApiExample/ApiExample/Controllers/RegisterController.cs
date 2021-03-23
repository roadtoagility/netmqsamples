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
        MessageService<Policy> _registerService;
        public RegisterController(MessageService<Policy> service)
        {
            _registerService = service;
        }

        [HttpPost]
        public ActionResult<string> Send(Policy policy)
        {
            _registerService.Send(policy);
            return CreatedAtAction("Register", policy.Type);
        }
    }

    
}
