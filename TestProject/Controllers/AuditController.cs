using Microsoft.AspNetCore.Mvc;
using TestProject.Service;

namespace TestProject.Controllers
{
    public class AuditController : Controller
    {
        private readonly AuditService _auditService;

        public AuditController(AuditService auditService)
        {
            _auditService = auditService;
        }

        public async Task<ViewResult> Index()
        {
            var auditLogs = await _auditService.GetAuditLogs();
            return View("Audit", auditLogs);
        }

        [HttpPost]
        public async Task<ViewResult> FilterAuditLog(DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.MinValue;
            endDate ??= DateTime.MaxValue;

            var filteredAuditLogs = await _auditService.Filter(startDate, endDate);

            return View("Audit", filteredAuditLogs);
        }
    }
}
