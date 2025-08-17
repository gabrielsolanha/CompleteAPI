using AutoMapper;
using Completeapi.CsharpModel.Domain.Entities;

namespace Completeapi.CsharpModel.Application.Sales.ListSale;

public class ListSaleProfile : Profile
{
    public ListSaleProfile()
    {
        CreateMap<Sale, ListSaleResult>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount));
    }
}
