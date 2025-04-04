using Data.Models;
using Data.DB;

namespace MVC.Services
{
    public interface IMemberService
    {
        IEnumerable<Member> GetMembers();
        void AddMember(Member member);
    }

    public class MemberService : IMemberService
    {
        private ILogger<MemberService> _logger;
        private readonly DataContext _context;

        public MemberService(DataContext context, ILogger<MemberService> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        public IEnumerable<Member> GetMembers()
        {
            return _context.Members.AsEnumerable();
        }

        public void AddMember(Member member)
        {
            _context.Members.Add(member);
            _context.SaveChanges();
        }
    }
}