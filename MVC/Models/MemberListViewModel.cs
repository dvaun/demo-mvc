using Data.Models;
namespace MVC.Models
{
    public class MemberListViewModel
    {
        public required List<Member> Members { get; set; }
        public bool HasData => Members.Any();
    }
}