namespace Axle.Framework.Devices.Base;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Axle.Framework.Devices;
using Axle.Framework.Devices.Metadata;
using Axle.Framework.Platform;

public abstract class FrameworkDevice
{
    private const string BoardRegistryFileName = "BoardRegistry.json";

    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    private static readonly Dictionary<string, FrameworkDeviceMetadata>? _knownBoardRegistry;
    
    public IPlatformInfoProvider PlatformInfo { get; }
    public abstract FrameworkDeviceType Type { get; }

    static FrameworkDevice()
    {
        if (!TryLoadUserBoardRegistry(out _knownBoardRegistry))
        {
            TryLoadBuiltInBoardRegistry(out _knownBoardRegistry);
        }
    }

    protected FrameworkDevice()
    {
        PlatformInfo = CreatePlatformInfoProvider();
    }
    
    public static FrameworkDeviceType Recognize()
    {
        var provider = CreatePlatformInfoProvider();

        if (_knownBoardRegistry == null)
        {
            throw new AxleFrameworkException("Unable to initialize known board registry.");
        }

        return _knownBoardRegistry.TryGetValue(provider.BoardProductName, out var meta) 
            ? meta.Type 
            : FrameworkDeviceType.Unknown;
    }

    public static FrameworkDevice Detect()
    {
        return Recognize() switch
        {
            FrameworkDeviceType.Laptop12 => throw new NotImplementedException(),
            FrameworkDeviceType.Laptop13 => throw new NotImplementedException(),
            FrameworkDeviceType.Laptop16 => new FrameworkLaptop16Device(),
            FrameworkDeviceType.Desktop => throw new NotImplementedException(),
            _ => throw new AxleFrameworkException("Unable to detect your Framework device.")
        };
    }

    private static bool TryLoadUserBoardRegistry(
        [MaybeNullWhen(false)] out Dictionary<string, FrameworkDeviceMetadata> boardRegistry)
    {
        boardRegistry = null;
        
        var userPath = Path.Combine(AppContext.BaseDirectory, BoardRegistryFileName);

        if (!File.Exists(userPath))
            return false;

        using (var fileStream = File.OpenRead(userPath))
        {
            boardRegistry = JsonSerializer.Deserialize<Dictionary<string, FrameworkDeviceMetadata>>(
                fileStream,  
                _serializerOptions
            );
        }
        
        return boardRegistry != null;
    }

    private static bool TryLoadBuiltInBoardRegistry(
        [MaybeNullWhen(false)] out Dictionary<string, FrameworkDeviceMetadata> boardRegistry)
    {
        boardRegistry = null;
        
        using var resourceStream = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream($"Axle.Framework.Resources.{BoardRegistryFileName}");
        
        if (resourceStream == null)
            return false;
        
        boardRegistry = JsonSerializer.Deserialize<Dictionary<string, FrameworkDeviceMetadata>>(
            resourceStream,  
            _serializerOptions
        );
        
        return boardRegistry != null;
    }
    
    private static IPlatformInfoProvider CreatePlatformInfoProvider()
    {
        if (OperatingSystem.IsWindows())
        {
            return new WindowsPlatformInfoProvider();
        }

        if (OperatingSystem.IsLinux())
        {
            return new LinuxPlatformInfoProvider();
        }

        throw new NotSupportedException("Your platform is not supported.");
    }
}