using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("Members")]
    public class Member
    {
        [Key]
        public required string MembershipID {get; set;}
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public LanguagePreference? LanguagePreference { get; set; }

        public required DateTime DOB { get; set; }

        /// <summary>
        /// Returns a string describing the gift this member would receive based upon their birthday
        /// </summary>
        public string GetMemberGift()
        {
            /*
            Some brief research led me to learn that seasonal-changes aren't necessarily
            standardized. Rather, "seasons" roughly correspond with shifts between
            solstice and equinox events. Rough date estimates are used here to avoid
            implementing any difficult, and unnecessary, logic to pinpoint minor differences
            between years
            */

            // I could probably add an enum and simplify this - going to leave it as is
            return (Season.GetSeason(this.DOB)) switch
            {
                "spring" => "Chocolate Bunny",
                "summer" => "Popsicle",
                "fall" => "Pumpkin",
                "winter" => "Hot Chocolate",
                _ => "n/a"
            };
        }
    }

    public enum LanguagePreference
    {
        English = 1,
        Spanish = 2
    }
}