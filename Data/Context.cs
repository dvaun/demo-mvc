using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.DB
{
    public class DataContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberCompany> MemberCompanies { get; set;}

        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            builder.UseSeeding((context, _) =>
            {
                var baseCompany = context.Set<MemberCompany>().FirstOrDefault(c => c.Id == -1);
                if (baseCompany == null)
                {
                    context.Set<MemberCompany>().Add(new MemberCompany {
                        Id = -1,
                        MembershipAcronym = "",
                        Name = "Unknown"
                    });
                    context.SaveChanges();
                }
            }).UseAsyncSeeding(async (context, _, cancellationToken) =>
            {                
                var baseCompany = await context.Set<MemberCompany>().FirstOrDefaultAsync(c => c.Id == -1, cancellationToken);
                if (baseCompany == null)
                {
                    context.Set<MemberCompany>().Add(new MemberCompany {
                        Id = -1,
                        MembershipAcronym = "",
                        Name = "Unknown"
                    });
                    await context.SaveChangesAsync(cancellationToken);
                }
            });
        }
    }
}