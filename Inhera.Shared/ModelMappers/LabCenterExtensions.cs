using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.VMs.LabCenter.Vm;

namespace Inhera.Shared.ModelMappers
{
    public static class LabCenterExtensions
    {
        public static LabCenterVm ToLabCenterVm(this LabCenterEntity entry)
        {
            var result = new LabCenterVm
            {
                Name = entry.Name,
                Description = entry.Description,
                Address = entry.Address?.ToAddressVm(),
                Latitude = entry.Latitude,
                Longitude = entry.Longitude,
                OperationHours = entry.OperationHours,
                OperationDays = entry.OperationDays
            };

            return result;
        }

        public static List<LabCenterVm> ToLabCenterVmList(this List<LabCenterEntity> entries)
        {
            var result = new List<LabCenterVm>();
            foreach (var entry in entries)
            {
                result.Add(entry.ToLabCenterVm());
            }
            return result;
        }
    }
}
