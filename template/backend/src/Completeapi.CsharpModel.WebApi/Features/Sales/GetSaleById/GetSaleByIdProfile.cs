using AutoMapper;
using Completeapi.CsharpModel.Application.Sales.GetSaleById;

namespace Completeapi.CsharpModel.WebApi.Features.Sales.GetSaleById;

public class GetSaleByIdProfile : Profile
{
    public GetSaleByIdProfile()
    {
        CreateMap<GetSaleByIdRequest, GetSaleByIdCommand>();
        CreateMap<GetSaleByIdResult, GetSaleByIdResponse>();
        CreateMap<SaleItemResult, SaleItemResponse>();
    }
}
