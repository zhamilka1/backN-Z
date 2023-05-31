using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Items.DB;
using Items.DB.Entities;
using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;

namespace User.API.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class ItemsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MainContext _context;

        public ItemsController(
            IMapper mapper,
            MainContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// GET: Items
        /// Возвращается список товаров
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DtoItem>>> GetItems()
        {
            return _mapper.Map<List<DtoItem>>(await _context.Items.ToListAsync<DbItem>());
        }

        //GET Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DtoItem>> GetDtoItem(int id)
        {
            var dbItem = await _context.Items.FindAsync(id);

            if (dbItem == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<DtoItem>(dbItem);
            
            return result;
        }

        //POST Items
        [HttpPost()]
        public async Task<ActionResult<DbItem>> PostDtoItem(DtoItem dtoItem)
        {
            var dbItem = _mapper.Map<DbItem>(dtoItem);

            await _context.Items.AddAsync(dbItem);

            await _context.SaveChangesAsync();

            var newDtoItem = _mapper.Map<DtoItem>(dbItem);

            return CreatedAtAction("GetDtoItem", new { id = newDtoItem.Id }, newDtoItem);
        }

        //PUT Items
        [HttpPut("{id}")]
        public async Task<ActionResult<DbItem>> PutDtoItem(int id, [FromBody] DtoItem dtoItem)
        {
            var dbItemFromDto = _mapper.Map<DbItem>(dtoItem);

            if (dbItemFromDto.Id != id || dbItemFromDto == null)
            {
                return BadRequest();
            }

            try
            {
                _context.Items.Update(dbItemFromDto);

                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                return BadRequest();
            }
            
            return new NoContentResult();
        }

        //PATCH Items
        [HttpPatch("{id}")]
        public async Task<ActionResult<DbItem>> PatchDtoItem(int id, [FromBody] JsonPatchDocument<DbItem> patchItem)
        {
            var dbItem = await _context.Items.FindAsync(id);

            if (dbItem == null || patchItem == null)
            {
                return BadRequest();
            }

            try
            {
                patchItem.ApplyTo(dbItem);
            }
            catch(JsonPatchException e)
            {
                return BadRequest();
            }
            
            await _context.SaveChangesAsync();

            return Ok(dbItem);
        }
        //DELETE Items
        [HttpDelete("{id}")]
        public async Task<ActionResult<DbItem>> DeleteDtoItem(int id)   
        {
            var dbItem = await _context.Items.FindAsync(id);

            if(dbItem == null)
            {
                return BadRequest();
            }

            _context.Items.Remove(dbItem);

            await _context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}