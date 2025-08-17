using AutoMapper;
using Completeapi.CsharpModel.Domain.Entities;

namespace Completeapi.CsharpModel.Application.Sales.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Sale>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<CreateSaleItemDto, SaleItem>();

        CreateMap<Sale, CreateSaleResult>();
    }
}
