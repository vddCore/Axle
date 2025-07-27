namespace Axle.ViewModel.CoreUI;

using Axle.Framework.Platform;
using Axle.Infrastructure.Services;

public class MainViewModel : ViewModelBase
{
    private readonly IDeviceInterfaceService _deviceInterface;
    private readonly IPlatformInfoProvider _platformInfo;

    public string Manufacturer => _platformInfo.SystemManufacturer;
    public string DeviceName => _platformInfo.SystemProductName;
    
    public string FullDeviceName => $"{_platformInfo.SystemManufacturer} {_platformInfo.SystemProductName}";

    public MainViewModel(IDeviceInterfaceService deviceInterface)
    {
        _deviceInterface = deviceInterface;
        _platformInfo = _deviceInterface.Device.PlatformInfo;
    }

    public void RefreshPlatformInfo()
    {
        _deviceInterface.Device.PlatformInfo.Refresh();
        
        OnPropertiesChanged(
            nameof(Manufacturer),
            nameof(DeviceName)
        );
    }
}