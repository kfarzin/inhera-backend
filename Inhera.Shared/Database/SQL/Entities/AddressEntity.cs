using Inhera.Shared.Enums;
using Inhera.Shared.Models.Database.SQL.Entities;
using Inhera.Shared.Util.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inhera.Shared.Database.SQL.Entities
{
    [Table("Addresses")]
    public class AddressEntity : SqlEntity
    {
        public string? Title { set; get; }
        public required string FirstName { set; get; }
        public required string LastName { set; get; }
        public string? PhoneNumber { set; get; }
        public string? MobileNumber { set; get; }
        public string? Email { set; get; }
        public bool? IsDefault { set; get; } = false;

        [EnumStringValue(typeof(AddressTypes))]
        public string? AddressType { set; get; }

        public string? Street { set; get; }
        public string? HouseNo { set; get; }
        public string? Additional1 { set; get; }
        public string? Additional2 { set; get; }
        public string? ZipCode { set; get; }
        public string? City { set; get; }
        public string? Country { set; get; }

        public UserProfileEntity? Profile { set; get; }
        public Guid? ProfileId { set; get; }

    }
}
