using App.Domain.Features.Places;
using App.Domain.Interfaces.Places;
using System.Collections.Generic;

namespace App.Application.Features.Places
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IList<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetByFoursquareId(string foursquareId)
        {
            return _categoryRepository.GetByFoursquareId(foursquareId);
        }

        public Category Insert(Category entity)
        {
            return _categoryRepository.Insert(entity);
        }
    }
}
