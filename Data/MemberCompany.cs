using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("MemberCompanies")]
    public class MemberCompany
    {
        [Key]
        public int Id { get; set;}
        public required string MembershipAcronym { get; set; }
        public required string Name {get;set;}
    }
}