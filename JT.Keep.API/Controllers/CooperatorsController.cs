using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JT.Keep.DataLayer;
using JT.Keep.Domain;
using AutoMapper;

namespace JT.Keep.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CooperatorsController : ControllerBase
    {
        private IRepository<Cooperator> _repository;

        public CooperatorsController(IRepository<Cooperator> repository)
        {
            _repository = repository;
        }

        // GET: api/Cooperators
        [HttpGet]
        public IEnumerable<DTO.Cooperator> GetCooperators()
        {
            var cooperators = _repository.GetAll();
            return Mapper.Map<IEnumerable<DTO.Cooperator>>(cooperators);
        }

        // GET: api/Cooperators/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCooperator([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cooperator = await _repository.GetByIdAsync(id);

            if (cooperator == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<DTO.Cooperator>(cooperator));
        }

        // PUT: api/Cooperators/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCooperator([FromRoute] int id, [FromBody] Cooperator cooperator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cooperator.Id)
            {
                return BadRequest();
            }

            await _repository.Update(Mapper.Map<Cooperator>(cooperator));

            return NoContent();
        }

        // POST: api/Cooperators
        [HttpPost]
        public async Task<IActionResult> PostCooperator([FromBody] Cooperator cooperator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.Insert(Mapper.Map<Cooperator>(cooperator));

            return CreatedAtAction("GetCooperator", new { id = cooperator.Id }, cooperator);
        }

        // DELETE: api/Cooperators/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCooperator([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.Delete(id);

            return Ok();
        }
    }
}