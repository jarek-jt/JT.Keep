using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JT.Keep.DataLayer;
using JT.Keep.Domain;
using AutoMapper;
using System.Drawing;

namespace JT.Keep.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private ICardRepository _repository;

        public CardsController(ICardRepository repository)
        {
            _repository = repository;
        }


        #region view

        // GET: api/Cards
        [HttpGet]
        public IEnumerable<DTO.Card> GetCards()
        {
            var cards = _repository.GetAll();
            return Mapper.Map<IEnumerable<DTO.Card>>(cards);
        }

        // GET: api/Cards/Red
        [HttpGet]
        [Route("{Colour}")]
        public IEnumerable<DTO.Card> GetCardsByColour([FromRoute]Color colour)
        {
            var cards = _repository.GetCardsByColour(colour);
            return Mapper.Map<IEnumerable<DTO.Card>>(cards);
        }

        //GET: api/Cards/Reminder/2018-09-01
        [HttpGet]
        [Route("reminder/{date:datetime:regex(\\d{{4}}-\\d{{2}}-\\d{{2}})}")]
        public IEnumerable<DTO.Card> GetCardsByReminderDate([FromRoute]DateTime date)
        {
            var cards = _repository.GetCardsByReminderDate(date);
            return Mapper.Map<IEnumerable<DTO.Card>>(cards);
        }

        //GET: api/Cards/Todos?done=false
        [HttpGet]
        [Route("todos")]
        public async Task<IEnumerable<DTO.ToDo>> GetCardsTodosAsync([FromQuery]bool? done)
        {
            var todos = await _repository.GetCardsTodosAsync(done);
            return Mapper.Map<IEnumerable<DTO.ToDo>>(todos);
        }

        // GET: api/Cards/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCard([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var card = await _repository.GetByIdAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<DTO.Card>(card));
        }

        // GET: api/Cards/5/todos
        [HttpGet("{id}/todos")]
        public async Task<IActionResult> GetCardTodos([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todos = await _repository.GetTodosByIdAsync(id);

            if (todos == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<IEnumerable<DTO.ToDo>>(todos));
        }

        #endregion

        #region edit

        // PUT: api/Cards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard([FromRoute] int id, [FromBody] DTO.Card card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != card.Id)
            {
                return BadRequest();
            }

            await _repository.Update(Mapper.Map<Card>(card));

            return NoContent();
        }

        // POST: api/Cards
        [HttpPost]
        public async Task<IActionResult> PostCard([FromBody] DTO.Card card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.Insert(Mapper.Map<Card>(card));
            
            return CreatedAtAction("GetCard", new { id = card.Id }, card);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.Delete(id);
            
            return Ok();
        }

        #endregion
    }
}