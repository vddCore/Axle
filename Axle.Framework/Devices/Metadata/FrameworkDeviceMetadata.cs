namespace Axle.Framework.Devices.Metadata;

using System.Text.Json.Serialization;

internal record FrameworkDeviceMetadata
{
    [JsonPropertyName("Type"), JsonConverter(typeof(JsonStringEnumConverter))]
    public FrameworkDeviceType Type { get; set; } =  FrameworkDeviceType.Unknown;
    
    [JsonPropertyName("Comment")]
    public string Comment { get; set; } = null!;
}