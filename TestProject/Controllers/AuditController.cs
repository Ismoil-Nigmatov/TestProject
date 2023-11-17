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
    }
}
