namespace Xibeiwind.JsonViewer
{
    public interface ICustomTextProvider : IJsonViewerPlugin
    {
        string GetText(JsonObject jsonObject);
    }
    public interface IJsonViewerPlugin
    {
        string DisplayName { get; }
        bool CanVisualize(JsonObject jsonObject);
    }
}