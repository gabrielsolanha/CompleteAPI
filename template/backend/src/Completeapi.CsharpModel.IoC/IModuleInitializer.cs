using Microsoft.AspNetCore.Builder;

namespace Completeapi.CsharpModel.IoC;

public interface IModuleInitializer
{
    void Initialize(WebApplicationBuilder builder);
}
