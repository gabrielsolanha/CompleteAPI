using MediatR;

namespace Completeapi.CsharpModel.Application.Sales.GetSaleById;

public class GetSaleByIdCommand : IRequest<GetSaleByIdResult>
{
    public Guid Id { get; set; }
    public string Token { get; set; }

    public GetSaleByIdCommand(Guid id)
    {
        Id = id;
    }
}
