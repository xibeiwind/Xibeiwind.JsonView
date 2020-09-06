using System.Collections.Generic;

namespace Xibeiwind.JsonViewer
{
    public class PluginsManager
    {
        public List<ICustomTextProvider> TextVisualizers { get; } = new List<ICustomTextProvider>();

        public List<IJsonVisualizer> Visualizers { get; } = new List<IJsonVisualizer>();

        public IJsonVisualizer DefaultVisualizer { get; private set; }

        public void Initialize()
        {
            InitDefaults();
        }
        private void InitDefaults()
        {
            if (DefaultVisualizer == null)
            {
                AddPlugin(new JsonObjectVisualizer());
                AddPlugin(new AjaxNetDateTime());
                AddPlugin(new CustomDate());

                AddPlugin(new GridVisualizer());
                AddPlugin(new SimpleGridVisualizer());
            }
        }

        private void AddPlugin(IJsonViewerPlugin plugin)
        {
            //_plugins.Add(plugin);
            if (plugin is ICustomTextProvider provider)
                TextVisualizers.Add(provider);
            if (plugin is IJsonVisualizer visualizer)
            {
                if (DefaultVisualizer == null)
                    DefaultVisualizer = visualizer;
                Visualizers.Add(visualizer);
            }
        }
    }
}
