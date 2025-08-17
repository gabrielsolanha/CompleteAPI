using AutoMapper;
using Completeapi.CsharpModel.Application.Sales.ListSale;


namespace Completeapi.CsharpModel.WebApi.Features.Sales.ListSale;

public class ListSaleProfile : Profile
{
    public ListSaleProfile()
    {
        CreateMap<ListSaleRequest, ListSaleCommand>();
        CreateMap<ListSaleResult, ListSaleResponse>();
    }
}
