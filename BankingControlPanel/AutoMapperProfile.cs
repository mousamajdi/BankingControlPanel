using AutoMapper;
using BankingControlPanel.DTOs;
using BankingControlPanel.Entities;
using BankingControlPanel.Models;

namespace BankingControlPanel
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Existing mappings
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<Account, AccountDTO>().ReverseMap();

            // Mapping for PagedResult
            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>)).ConvertUsing(typeof(PagedResultConverter<,>));
        }
    }

    // Custom converter to handle PagedResult<T> mapping
    public class PagedResultConverter<TSource, TDestination> : ITypeConverter<PagedResult<TSource>, PagedResult<TDestination>>
    {
        private readonly IMapper _mapper;

        public PagedResultConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public PagedResult<TDestination> Convert(PagedResult<TSource> source, PagedResult<TDestination> destination, ResolutionContext context)
        {
            return new PagedResult<TDestination>
            {
                TotalCount = source.TotalCount,
                TotalPages = source.TotalPages,
                CurrentPage = source.CurrentPage,
                PageSize = source.PageSize,
                Data = _mapper.Map<IEnumerable<TDestination>>(source.Data)
            };
        }
    }
}
