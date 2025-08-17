using AutoMapper;

namespace Completeapi.CsharpModel.WebApi.Features.Sales.DeleteSale;

/// <summary>
/// Profile for mapping DeleteSale feature requests to commands
/// </summary>
public class DeleteSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteSale feature
    /// </summary>
    public DeleteSaleProfile()
    {
        CreateMap<DeleteSaleRequest, Application.Sales.DeleteSale.DeleteSaleCommand>()
            .ConstructUsing(x => new Application.Sales.DeleteSale.DeleteSaleCommand(x.Id, x.Token));
    }
}
