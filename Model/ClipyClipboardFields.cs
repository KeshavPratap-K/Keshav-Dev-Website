using MongoDB.Bson.Serialization.Attributes;

namespace Keshav_Dev.Model;

public class ClipyClipboardFields
{
    [BsonId]
    public string? IdShared { get; set; }
    public List<string>? ClipyHistory { get; set; }
}

public class ClipyClipboardData
{
    public List<string> clipboardData { get; set; }
}
