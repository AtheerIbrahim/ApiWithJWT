using ApiWithJWT.Dtos;
using ApiWithJWT.Dtos.SubCategories;

namespace ApiWithJWT.Services.Interfaces;
public interface ISubcategoryService
{
    Task<Guid> AddAsync(CreateSubcategoryDto subcategoryModel);
    Task<bool> DeleteAsync(Guid subcategoryId);
    Task<List<SubcategoryDto>> GetAllAsync();
    Task<SubcategoryDto> GetByIdAsync(Guid subcategoryId);
    Task<bool> UpdateAsync(Guid subcategoryId, UpdateSubcategoryDto subcategoryModel);
}