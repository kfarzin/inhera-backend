using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.VMs.Address.Vm;

namespace Inhera.Shared.ModelMappers
{
    public static class AddressExtensions
    {
        public static AddressVm ToAddressVm(this AddressEntity entry)
        {
            var result = new AddressVm
            {
                Title = entry.Title,
                PhoneNumber = entry.PhoneNumber,
                Street = entry.Street,
                HouseNo = entry.HouseNo,
                Additional1 = entry.Additional1,
                Additional2 = entry.Additional2,
                ZipCode = entry.ZipCode,
                City = entry.City,
                Country = entry.Country,
            };

            return result;
        }
    }
}
