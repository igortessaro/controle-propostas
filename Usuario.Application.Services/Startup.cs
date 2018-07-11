using Microsoft.Extensions.Configuration;

namespace Usuario.Application.Services
{
    public class Startup : Framework.Aplication.Services.Startup
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}
