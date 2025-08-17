using AutoMapper;
using Completeapi.CsharpModel.Domain.Entities;

namespace Completeapi.CsharpModel.Application.Sales.GetSaleById;

public class GetSaleByIdProfile : Profile
{
    public GetSaleByIdProfile()
    {
        CreateMap<Sale, GetSaleByIdResult>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount));

        CreateMap<SaleItem, SaleItemResult>()
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));
    }
}
