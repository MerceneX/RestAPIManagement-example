using Microsoft.AspNetCore.Mvc;
using RestAPI.Models;
using RestAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccidentsController : ControllerBase
    {
        private readonly IAccidentsRepository _accidentsRepository;

        public AccidentsController(IAccidentsRepository accidentsRepository) =>
            _accidentsRepository = accidentsRepository;

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<List<Accident>> Get() =>
            await _accidentsRepository.GetAccident();

        // GET api/<ValuesController>/5
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Accident>> Get(string id)
        {
            var accident = await _accidentsRepository.GetAccidentByID(id);

            if (accident is null)
            {
                return NotFound();
            }

            return accident;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Post(Accident newAccident)
        {
            await _accidentsRepository.InsertAccident(newAccident);

            return CreatedAtAction(nameof(Get), new { id = newAccident.Id }, newAccident);
        }


        // PUT api/<ValuesController>/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Accident updatedAccident)
        {
            var book = await _accidentsRepository.GetAccidentByID(id);

            if (book is null)
            {
                return NotFound();
            }

            updatedAccident.Id = book.Id;

            await _accidentsRepository.UpdateAccident(id, updatedAccident);

            return NoContent();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _accidentsRepository.GetAccidentByID(id);

            if (book is null)
            {
                return NotFound();
            }

            await _accidentsRepository.DeleteAccident(id);

            return NoContent();
        }
    }
}
