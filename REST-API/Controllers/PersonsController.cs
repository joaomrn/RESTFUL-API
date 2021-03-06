﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTAPI.Business;
using RESTAPI.Data.VO;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using Tapioca.HATEOAS;

namespace REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        //Declaração do serviço usado
        private IPersonBusiness _personBusiness;

        public PersonsController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }

        // GET api/Persons
        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        [SwaggerResponse((200), Type = typeof(List<PersonVO>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [Authorize("Bearer")]
        public ActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        // GET api/Persons
        [HttpGet("find-by-name")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [SwaggerResponse((200), Type = typeof(List<PersonVO>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [Authorize("Bearer")]
        public ActionResult GetByName([FromQuery] string firstName, [FromQuery] string lastName)
        {
            return Ok(_personBusiness.FindByName(firstName, lastName));
        }
        
        // GET api/Persons
        [HttpGet("find-with-page-search/{sortDirection}/{pageSize}/{page}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [SwaggerResponse((200), Type = typeof(List<PersonVO>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [Authorize("Bearer")]
        public ActionResult GetPageSearch([FromQuery] string name, string sortDirection, int pageSize, int page)
        {
            return Ok(_personBusiness.FindWithPagedSearch(name, sortDirection, pageSize, page));
        }

        // GET api/Persons/5
        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [SwaggerResponse((200), Type = typeof(PersonVO))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [Authorize("Bearer")]
        public ActionResult Get(int id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        // POST api/Persons
        [HttpPost]
        [SwaggerResponse((201), Type = typeof(PersonVO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [Authorize("Bearer")]
        public ActionResult Post([FromBody]PersonVO person)
        {
            if (person == null) return BadRequest();
            return new ObjectResult(_personBusiness.Create(person));
        }

        // PUT api/Persons
        [HttpPut]
        [SwaggerResponse((202), Type = typeof(PersonVO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [Authorize("Bearer")]
        public ActionResult Put([FromBody]PersonVO person)
        {
            if (person == null) return BadRequest();
            var updatedPerson = _personBusiness.Update(person);
            if (updatedPerson == null) return NoContent();
            return new ObjectResult(updatedPerson);
        }

        // PUT api/Persons
        [HttpPatch]
        [SwaggerResponse((202), Type = typeof(PersonVO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [Authorize("Bearer")]
        public ActionResult Patch([FromBody]PersonVO person)
        {
            if (person == null) return BadRequest();
            var updatedPerson = _personBusiness.Update(person);
            if (updatedPerson == null) return NoContent();
            return new ObjectResult(updatedPerson);
        }

        // DELETE api/Persons/5
        [HttpDelete("{id}")]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [Authorize("Bearer")]
        public ActionResult Delete(int id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}
