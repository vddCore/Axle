namespace Axle.Framework.Platform;

public interface IPlatformInfoProvider
{
    string BiosVendor { get; }
    string BiosReleaseDate { get; }
    string BiosVersion { get; }
    
    string BoardManufacturer { get; }
    string BoardProductName { get; }
    string BoardRevision { get; }
    
    string SystemManufacturer { get; }
    string SystemProductName { get; }
    string SystemSKU { get; }

    void Refresh();
}