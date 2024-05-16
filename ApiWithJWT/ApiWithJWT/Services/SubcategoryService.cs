using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiWithJWT.Data;
using ApiWithJWT.Dtos;
using ApiWithJWT.Dtos.Categories;
using ApiWithJWT.Dtos.SubCategories;
using ApiWithJWT.Models;
using ApiWithJWT.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiWithJWT.Services
{
    public class SubcategoryService : ISubcategoryService
	{
		private readonly ApplicationDbContext _dbContext;

		public SubcategoryService(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		// Create (Add)
		public async Task<Guid> AddAsync(CreateSubcategoryDto subcategoryModel)
		{
			var subcategory = new Subcategory
			{
				SubcategoryId = Guid.NewGuid(),
				Name = subcategoryModel.Name,
				CategoryId = subcategoryModel.CategoryId
			};

			await _dbContext.Subcategories.AddAsync(subcategory);
			await _dbContext.SaveChangesAsync();

			return subcategory.SubcategoryId;
		}

		// Read (Get All)
		public async Task<List<SubcategoryDto>> GetAllAsync()
		{
			return await _dbContext
				.Subcategories.Include(s => s.Category)
				.Select(s => new SubcategoryDto
				{
					SubcategoryId = s.SubcategoryId,
					Name = s.Name,
					CategoryId = s.CategoryId,
				})
				.ToListAsync();
		}

		// Read (Get by ID)
		public async Task<SubcategoryDto> GetByIdAsync(Guid subcategoryId)
		{
			var subcategory = await _dbContext
				.Subcategories.Include(s => s.Category)
				.FirstOrDefaultAsync(s => s.SubcategoryId == subcategoryId);

			if (subcategory == null)
			{
				throw new Exception("not found");
			}
			return new SubcategoryDto
			{
				SubcategoryId = subcategory.SubcategoryId,
				Name = subcategory.Name,
				CategoryId = subcategory.CategoryId,
			};
		}

		// Update
		public async Task<bool> UpdateAsync(Guid subcategoryId, UpdateSubcategoryDto subcategoryModel)
		{
			var subcategory = await _dbContext.Subcategories.FindAsync(subcategoryId);
			if (subcategory == null)
				return false;

			subcategory.Name = subcategoryModel.Name;
			subcategory.CategoryId = subcategoryModel.CategoryId;
			await _dbContext.SaveChangesAsync();
			return true;
		}

		// Delete
		public async Task<bool> DeleteAsync(Guid subcategoryId)
		{
			var subcategory = await _dbContext.Subcategories.FindAsync(subcategoryId);
			if (subcategory == null)
				return false;

			_dbContext.Subcategories.Remove(subcategory);
			await _dbContext.SaveChangesAsync();
			return true;
		}
	}
}
