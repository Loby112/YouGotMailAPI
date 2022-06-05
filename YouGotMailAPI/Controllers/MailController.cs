using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using YouGotMailAPI.Database;
using YouGotMailAPI.Managers;
using YouGotMailAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YouGotMailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class MailController : ControllerBase
    {
        private IMailManager _mailManager;

        public MailController(MailDBContext context)
        {
            _mailManager = new DBMailManager(context);
        }


        // GET: api/<MailController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<Mail>> GetAllMail()
        {
            IEnumerable<Mail> result = _mailManager.GetAllMail();
            if (result.Count() == 0) NotFound("No mails found");
            return Ok(result);
        }

        //// GET api/<MailController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value"; 
        //}
        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST api/<MailController>
        [HttpPost]
        public ActionResult<Mail> Post([FromBody] Mail newMail)
        {
            Mail mail = new Mail();
            if (newMail.Detected == null)
            {
                return BadRequest(newMail);
            } 

            mail = _mailManager.AddMail(newMail);
            return Created("api/mail/"  + mail.Id, mail);
        }

        //// PUT api/<MailController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<MailController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<Mail> Delete(int id)
        {
            Mail mailToBeDeleted = _mailManager.GetMailById(id);
            if (mailToBeDeleted == null) return NotFound("Mail not found " + id);
            _mailManager.DeleteMail(id);
            return Ok(mailToBeDeleted);
        }
    }
}
