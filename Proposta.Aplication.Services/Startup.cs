using Microsoft.Extensions.Configuration;

namespace Proposta.Aplication.Services
{
    public class Startup : Framework.Aplication.Services.Startup
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}
