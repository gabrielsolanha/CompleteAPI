using AutoMapper;
using Completeapi.CsharpModel.Domain.Entities;

namespace Completeapi.CsharpModel.Application.Sales.UpdateSale;

public class UpdateSaleProfile : Profile
{
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleCommand, Sale>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<UpdateSaleItemDto, SaleItem>();

        CreateMap<Sale, UpdateSaleResult>();
    }
}
