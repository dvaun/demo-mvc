using Data.Models;
using Data.DB;

namespace MVC.Services
{
    public interface IMemberCompanyService
    {
        IEnumerable<MemberCompany> GetMemberCompanies();
        void AddMemberCompany(MemberCompany company);
    }

    public class MemberCompanyService : IMemberCompanyService
    {
        private ILogger<MemberCompanyService> _logger;
        private readonly DataContext _context;

        public MemberCompanyService(DataContext context, ILogger<MemberCompanyService> logger) {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<MemberCompany> GetMemberCompanies()
        {
            return _context.MemberCompanies.AsEnumerable();
        }

        public void AddMemberCompany(MemberCompany company)
        {
            _context.MemberCompanies.Add(company);
            _context.SaveChanges();
        }
    }
}