using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using YouGotMailAPI.Database;
using YouGotMailAPI.EmailServices;
using YouGotMailAPI.Managers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YouGotMailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class EmailController : ControllerBase
    {
        private IEmailManager _eMailManager;

        public EmailController(MailDBContext context)
        {
            _eMailManager = new DBEmailManager(context);
        }

        // GET: api/<EmailController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<UserEmailOptions>> Get()
        {
            IEnumerable<UserEmailOptions> result = _eMailManager.GetAllUserEmailOptions();
            if (result.Count() == 0) NotFound("No Emails found");
            return Ok(result);
        }

        // GET api/<EmailController>/5

        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<EmailController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<UserEmailOptions> Post([FromBody] UserEmailOptions newUserEmailOptions)
        {
            UserEmailOptions userEmailOptions = new UserEmailOptions();
            if (newUserEmailOptions.ToEmails == null) return BadRequest(newUserEmailOptions);
            userEmailOptions = _eMailManager.AddUserEmailOptions(newUserEmailOptions);
            return Created("api/Email/" + userEmailOptions.Id, userEmailOptions);
        }

        //// PUT api/<EmailController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<EmailController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<UserEmailOptions> Delete(int id)
        {
            UserEmailOptions userEmailOptionsToBeDeleted = _eMailManager.GetEmailOptionsById(id);
            if (userEmailOptionsToBeDeleted == null) return NotFound("Email not found: " + id);
            _eMailManager.DeleteUserEmailOptions(id);
            return Ok(userEmailOptionsToBeDeleted);
        }
    }
}
