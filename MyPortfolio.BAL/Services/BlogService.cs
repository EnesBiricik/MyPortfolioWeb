using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.BAL.Extensions;
using MyPortfolio.BAL.Helpers;
using MyPortfolio.BAL.Interfaces;
using MyPortfolio.Common;
using MyPortfolio.DAL.UnitOfWork;
using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.BAL.Services
{
    public class BlogService : Service<BlogCreateDto, BlogUpdateDto, BlogListDto, Blog>, IBlogService
    {

        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<BlogCreateDto> _validator;
        private readonly IValidator<BlogUpdateDto> _updateValidator;

        public BlogService(IMapper mapper, IValidator<BlogCreateDto> createDtoValidator, IValidator<BlogUpdateDto> updateDtoValidator, IUow uow) : base(mapper, createDtoValidator, updateDtoValidator, uow)
        {
            _uow = uow;

            _mapper = mapper;
            _validator = createDtoValidator;
            _updateValidator = updateDtoValidator;

        }

        public async Task<IResponse<BlogCreateDto>> CreateAsync(BlogCreateDto dto, IFormFile CoverPhoto)
        {
            var result = _validator.Validate(dto);
            if (result.IsValid)
            {
                var path = await FileHelper.CreateFile(CoverPhoto);
                var createEntity = _mapper.Map<Blog>(dto);
                createEntity.CoverPhoto = path;
                createEntity.ReadingTime = ReadingTime.Calculate(dto.Content);
                await _uow.GetRepository<Blog>().CreateAsync(createEntity);
                await _uow.SaveChanges();
                return new Response<BlogCreateDto>(ResponseType.Success, dto);
            }
            return new Response<BlogCreateDto>(dto, result.ConvertToCustomValidationError());
        }

        public async Task<IResponse<List<BlogListDto>>> GetAllAsync(bool isFeatured=false)
        {
           var data =  isFeatured==false? await _uow.GetRepository<Blog>().GetQuery().OrderByDescending(x => x.Date).Include(x => x.Category).ToListAsync() : await _uow.GetRepository<Blog>().GetQuery().OrderByDescending(x => x.Date).Include(x => x.Category).Where(x => x.IsFeatured == true).ToListAsync();

            var dto = _mapper.Map<List<BlogListDto>>(data);
            return new Response<List<BlogListDto>>(ResponseType.Success, dto);
        }

        public async Task<IResponse<List<BlogListDto>>> GetAllAsyncForBlogs()
        {
            var data = await _uow.GetRepository<Blog>().GetQuery().OrderByDescending(x => x.Date).Include(x => x.Category).Take(10).ToListAsync();

            var dto = _mapper.Map<List<BlogListDto>>(data);
            return new Response<List<BlogListDto>>(ResponseType.Success, dto);
        }

        public async Task<IResponse<List<BlogListDto>>> LoadMore(int pageIndex)
        {
            var pageSize = 10;

            var data = await _uow.GetRepository<Blog>().GetQuery()
                .OrderByDescending(x => x.Date)
                .Skip((pageIndex - 1) * pageSize)
                .Include(x => x.Category)
                .Take(pageSize)
                .ToListAsync();

            var dto = _mapper.Map<List<BlogListDto>>(data);
            return new Response<List<BlogListDto>>(ResponseType.Success, dto);
        }


        public async Task<IResponse<List<BlogListDto>>> GetAllByCategoryIdAsync(int id)
        {
           var data = await _uow.GetRepository<Blog>().GetQuery().Include(x => x.Category).Where(x=> x.CategoryId == id).ToListAsync();

            var dto = _mapper.Map<List<BlogListDto>>(data);
            return new Response<List<BlogListDto>>(ResponseType.Success, dto);
        }

        public async Task<IResponse<IDto>> GetByIdAsync<IDto>(int Id)
        {
            var data = await _uow.GetRepository<Blog>().GetQuery()
                .Include(x => x.Comments.Where(x => x.IsShared == true))
                .ThenInclude(x => x.Replies.Where(x => x.IsShared == true))
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == Id);
            if (data == null)
                return new Response<IDto>(ResponseType.NotFound, $"{Id}' ye ait veri bulunamadı!");
            var dto = _mapper.Map<IDto>(data);
            return new Response<IDto>(ResponseType.Success, dto);
        }

        public async Task<IResponse> RemoveAsync(int Id)
        {
            var data = await _uow.GetRepository<Blog>().FindAsync(Id);
            if (data == null)
                return new Response<Blog>(ResponseType.NotFound, $"{Id} ye sahip veri bulunamadı!");

            _uow.GetRepository<Blog>().Remove(data);
            await _uow.SaveChanges();

            return new Response(ResponseType.Success);
        }

        public async Task<IResponse> FeatureStatusUpdate(int Id)
        {
            var data = await _uow.GetRepository<Blog>().FindAsync(Id);
            if (data == null)
                return new Response<Blog>(ResponseType.NotFound, $"{Id} ye sahip veri bulunamadı!");

            data.IsFeatured = !data.IsFeatured;
            await _uow.SaveChanges();
            return new Response(ResponseType.Success);
        }

        public async Task<IResponse<BlogUpdateDto>> UpdateAsync(BlogUpdateDto dto, IFormFile? IconFile)
        {
            var result = _updateValidator.Validate(dto);
            if (result.IsValid)
            {
                var unchangedData = await _uow.GetRepository<Blog>().FindAsync(dto.Id);
                if (unchangedData == null)
                    return new Response<BlogUpdateDto>(ResponseType.NotFound, $"{dto.Id} Id değerine sahip sonuç bulunamadı");
                
                var entity = _mapper.Map<Blog>(dto);
                if (IconFile != null)
                {
                    entity.CoverPhoto = await FileHelper.ReplaceFile(unchangedData.CoverPhoto, IconFile);
                }
                else
                    entity.CoverPhoto = unchangedData.CoverPhoto;

                entity.ReadingTime = ReadingTime.Calculate(dto.Content);

                _uow.GetRepository<Blog>().Update(entity, unchangedData);
                await _uow.SaveChanges();
                return new Response<BlogUpdateDto>(ResponseType.Success, dto);
            }

            return new Response<BlogUpdateDto>(ResponseType.ValidationError, dto);
        }

    }
}
