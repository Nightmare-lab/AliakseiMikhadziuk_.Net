using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class ReportServices
    {
        private ILogger<ReportServices> _logger;
        private ReportCreatorServices _reportCreatorServices;
        private IRepository<Students> _repository;

        public ReportServices(ILogger<ReportServices> logger,
            ReportCreatorServices reportCreatorServices,
            IRepository<Students> repository)
        {
            _logger = logger;
            _reportCreatorServices = reportCreatorServices;
            _repository = repository;
        }


    }
}
