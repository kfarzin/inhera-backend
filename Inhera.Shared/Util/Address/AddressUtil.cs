using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.Enums;

namespace Inhera.Shared.Util.Address
{
    public static class AddressUtil
    {
        public static AddressEntity CreateAnEmptyAddressWithType(AddressTypes type)
        {
            var address = new AddressEntity
            {
                Title = "",
                FirstName = "",
                LastName = "",
                PhoneNumber = "",
                MobileNumber = "",
                Email = "",
                IsDefault = false,
                AddressType = type.ToString(),
                Street = "",
                HouseNo = "",
                Additional1 = "",
                Additional2 = "",
                ZipCode = "",
                City = "",
                Country = "",
            };

            return address;
        }
    }
}
