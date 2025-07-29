namespace Axle.Infrastructure.Services;

using Axle.Framework.Devices.Base;

public interface IDeviceInterfaceService : IService
{
    bool IsSupportedDevice { get; }
    FrameworkDevice? Device { get; }
}