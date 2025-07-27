namespace Axle.Framework.Platform;

using System.Runtime.Versioning;

[SupportedOSPlatform("linux")]
public class LinuxPlatformInfoProvider :  IPlatformInfoProvider
{
    public string BiosVendor => throw new NotImplementedException();
    public string BiosReleaseDate => throw new NotImplementedException();
    public string BiosVersion => throw new NotImplementedException();
    public string BoardManufacturer => throw new NotImplementedException();
    public string BoardProductName => throw new NotImplementedException();
    public string BoardRevision => throw new NotImplementedException();
    public string SystemManufacturer => throw new NotImplementedException();
    public string SystemProductName => throw new NotImplementedException();
    public string SystemSKU => throw new NotImplementedException();
    
    public void Refresh()
    {
        throw new NotImplementedException();
    }
}