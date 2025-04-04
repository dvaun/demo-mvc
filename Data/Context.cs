using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.DB
{
    public class DataContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberCompany> MemberCompanies { get; set;}

        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
    }
}