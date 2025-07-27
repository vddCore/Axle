namespace Axle.Infrastructure.Services;

using Axle.Framework;
using Axle.Framework.Devices.Base;

public interface IDeviceInterfaceService : IService
{
    FrameworkDevice Device { get; }
}