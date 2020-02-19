using Microsoft.Extensions.Configuration;

namespace BusinessLogic.Services
{
    public class ReportCreatorServices
    {
        private string _json;
        private string _excel;
        private readonly IConfiguration _configuration;

        public ReportCreatorServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string JsonConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_json))
                {
                    _json = _configuration.GetConnectionString("JSON");
                }

                return _json;
            }
        }
    }
}