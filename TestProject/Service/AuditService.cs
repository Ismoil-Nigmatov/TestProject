using Microsoft.EntityFrameworkCore;
using TestProject.Data;
using TestProject.Entity;

namespace TestProject.Service
{
    public class AuditService
    {
        private readonly AppDbContext _dbContext;

        public AuditService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Audit>> GetAuditLogs()
        {
            var auditLogs = await _dbContext.AuditLogs.ToListAsync();
            return auditLogs;
        }

        public async Task<List<Audit>> Filter(DateTime? startDate, DateTime? endDate)
        {
            startDate = DateTime.SpecifyKind(startDate!.Value, DateTimeKind.Utc);
            endDate = DateTime.SpecifyKind(endDate!.Value, DateTimeKind.Utc);

            if (endDate != DateTime.MaxValue) endDate = endDate?.AddDays(1);

            var filteredAuditLogs = await _dbContext.AuditLogs
                .Where(log => log.DateTime >= startDate && log.DateTime <= endDate)
                .ToListAsync();
            return filteredAuditLogs;
        }
    }
}
