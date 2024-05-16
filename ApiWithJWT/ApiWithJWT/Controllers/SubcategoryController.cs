using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiWithJWT.Dtos;
using ApiWithJWT.Dtos.SubCategories;
using ApiWithJWT.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiWithJWT.Controllers
{
    [ApiController]
    [Route("api/subcategory")]
    public class SubcategoryController : ControllerBase
    {
        private readonly ISubcategoryService _subcategoryService;

        public SubcategoryController(ISubcategoryService subcategoryService)
        {
            _subcategoryService = subcategoryService;
        }

        // GET: api/subcategory
        [HttpGet]
        public async Task<ActionResult<List<SubcategoryDto>>> GetAllSubcategories()
        {
            var subcategories = await _subcategoryService.GetAllAsync();
            return Ok(subcategories);
        }

        // GET: api/subcategory/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SubcategoryDto>> GetSubcategoryById(Guid id)
        {
            var subcategory = await _subcategoryService.GetByIdAsync(id);
            if (subcategory == null)
                return NotFound();

            return Ok(subcategory);
        }

        // POST: api/subcategory
        [HttpPost]
        public async Task<ActionResult<Guid>> AddSubcategory(CreateSubcategoryDto subcategoryModel)
        {
            var subcategoryId = await _subcategoryService.AddAsync(subcategoryModel);
            return CreatedAtAction(
                nameof(GetSubcategoryById),
                new { id = subcategoryId },
                subcategoryId
            );
        }

        // PUT: api/subcategory/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSubcategory(
            Guid id,
			UpdateSubcategoryDto subcategoryModel
        )
        {
        
            var result = await _subcategoryService.UpdateAsync(id, subcategoryModel);
            if (!result)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/subcategory/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubcategory(Guid id)
        {
            var result = await _subcategoryService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
