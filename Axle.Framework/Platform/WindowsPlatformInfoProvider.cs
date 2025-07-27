namespace Axle.Framework.Platform;

using System.Management;
using System.Runtime.Versioning;

[SupportedOSPlatform("windows")]
public class WindowsPlatformInfoProvider : IPlatformInfoProvider
{
    public string BiosVendor { get; private set; } = null!;
    public string BiosReleaseDate { get; private set; } = null!;
    public string BiosVersion { get; private set; } = null!;
    
    public string BoardManufacturer { get; private set; } = null!;
    public string BoardProductName { get; private set; } = null!;
    public string BoardRevision { get; private set; } = null!;
    
    public string SystemManufacturer { get; private set; } = null!;
    public string SystemProductName { get; private set; } = null!;
    public string SystemSKU { get; private set; } = null!;

    internal WindowsPlatformInfoProvider()
        => Refresh();
    
    public void Refresh()
    {
        BiosVendor = QueryWmiClass("ROOT\\WMI", "MS_SystemInformation", "BIOSVendor");
        BiosReleaseDate = QueryWmiClass("ROOT\\WMI", "MS_SystemInformation", "BIOSReleaseDate");
        BiosVersion = QueryWmiClass("ROOT\\WMI", "MS_SystemInformation", "BIOSVersion");
        
        BoardManufacturer = QueryWmiClass("ROOT\\WMI", "MS_SystemInformation", "BaseBoardManufacturer");
        BoardProductName = QueryWmiClass("ROOT\\WMI", "MS_SystemInformation", "BaseBoardProduct");
        BoardRevision = QueryWmiClass("ROOT\\WMI", "MS_SystemInformation", "BaseBoardVersion");
        
        SystemManufacturer = QueryWmiClass("ROOT\\WMI", "MS_SystemInformation", "SystemManufacturer");
        SystemProductName = QueryWmiClass("ROOT\\WMI", "MS_SystemInformation", "SystemProductName");
        SystemSKU = QueryWmiClass("ROOT\\WMI", "MS_SystemInformation", "SystemSKU");
    }
    
    private static string QueryWmiClass(string scope, string wmiClass, string propertyName)
    {
        try
        {
            using var searcher = new ManagementObjectSearcher(
                new ManagementScope(scope),
                new ObjectQuery($"SELECT {propertyName} FROM {wmiClass}")
            );
            
            foreach (var obj in searcher.Get())
            {
                return obj[propertyName]?.ToString() ?? "N/A";
            }
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
        
        return $"{wmiClass}/{propertyName}: not found.";
    }
}