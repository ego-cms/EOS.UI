using System;
using System.Collections.Generic;

namespace EOS.UI.iOS.Sandbox.Controls
{
    public class ControlsData
    {
        private static readonly Lazy<ControlsData> _instance = new Lazy<ControlsData>(() => new ControlsData());
        public static ControlsData Instance => _instance.Value;

        public Dictionary<String, String> Identifiers { get; }

        private ControlsData()
        {
            Identifiers = new Dictionary<string, string>() {
                {SimpleLabelView.Identifier, "Simple Label"},
                {BadgeLabelView.Identifier, "Badge Label"},
                {GhostButtonView.Identifier, "Ghost Button"},
                {SimpleButtonView.Identifier, "Simple button"},
                {FabProgressView.Identifier, "Fab Progress"},
                {InputControlView.Identifier, "Input Control"}
            };
        }
    }
}
