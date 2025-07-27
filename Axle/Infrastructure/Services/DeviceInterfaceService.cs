namespace Axle.Infrastructure.Services;

using Axle.Framework.Devices.Base;

public class DeviceInterfaceService : IDeviceInterfaceService
{
    public FrameworkDevice Device { get; }

    public DeviceInterfaceService()
    {
        Device = FrameworkDevice.Detect();
    }
}