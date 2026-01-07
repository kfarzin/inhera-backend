using Inhera.Shared.VMs.Address.Vm;

namespace Inhera.Shared.VMs.LabCenter.Vm
{
    public class LabCenterVm
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public AddressVm? Address { set; get; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string? OperationHours { get; set; }
        public string? OperationDays { get; set; }
    }
}
