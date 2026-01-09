using System;
using UniverseLib.UI;
using UniverseLib.UI.Panels;

namespace UnityExplorer.UI
{
    /// <summary>
    /// VR variant of ExplorerUIBase which uses the UniverseLib VR world-space UI features.
    /// </summary>
    internal class ExplorerVRUIBase : VRUIBase
    {
        public ExplorerVRUIBase(string id, Action updateMethod) : base(id, updateMethod) { }

        protected override PanelManager CreatePanelManager()
        {
            return new UEPanelManager(this);
        }
    }
}
