using AutoMapper;
using FluentValidation;
using MyPortfolio.BAL.Interfaces;
using MyPortfolio.DAL.UnitOfWork;
using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.BAL.Services
{
    public class CategoryService : Service<CategoryCreateDto, CategoryUpdateDto, CategoryListDto, Category>
        , ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        private readonly IValidator<CategoryCreateDto> _createContentValidator;
        private readonly IValidator<CategoryUpdateDto> _updateContentValidator;

        public CategoryService(IMapper mapper, IValidator<CategoryCreateDto> createDtoValidator, IValidator<CategoryUpdateDto> updateDtoValidator, IUow uow) : base(mapper, createDtoValidator, updateDtoValidator, uow)
        {
            _mapper = mapper;
            _uow = uow;
            _createContentValidator = createDtoValidator;
            _updateContentValidator = updateDtoValidator;
        }



    }
}
