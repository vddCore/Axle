namespace Axle.Infrastructure.Services;

using Axle.Framework.Devices.Base;

public class DeviceInterfaceService : IDeviceInterfaceService
{
    public bool IsSupportedDevice { get; }
    public FrameworkDevice? Device { get; }

    public DeviceInterfaceService()
    {
        try
        {
            Device = FrameworkDevice.Detect();
            IsSupportedDevice = true;
        }
        catch
        {
            IsSupportedDevice = false;
        }
    }
}