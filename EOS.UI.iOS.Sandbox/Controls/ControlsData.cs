using System;
using System.Collections.Generic;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Sandbox.Helpers;

namespace EOS.UI.iOS.Sandbox.Controls
{
    public class ControlsData
    {
        private static readonly Lazy<ControlsData> _instance = new Lazy<ControlsData>(() => new ControlsData());
        public static ControlsData Instance => _instance.Value;

        public Dictionary<String, String> Names { get; }

        public const string Title = ControlNames.MainTitle;
        public const string BackTitle = "Back";

        private ControlsData()
        {
            Names = new Dictionary<string, string>()
            {
                {ControlNames.BadgeLabel, BadgeLabelView.Identifier},
                {ControlNames.SimpleLabel, SimpleLabelView.Identifier},
                {ControlNames.GhostButton, GhostButtonView.Identifier},
                {ControlNames.SimpleButton, SimpleButtonView.Identifier},
                {ControlNames.FabProgress, FabProgressView.Identifier},
                {ControlNames.Input, InputControlView.Identifier},
                {ControlNames.CircleProgress, CircleProgressView.Identifier},
                {ControlNames.Section, SectionComponentView.Identifier },
                {ControlNames.CTAButton, CTAButtonView.Identifier},
                {ControlNames.WorkTimeCalendar, WorkTimeView.Identifier}
            };
        }
    }
}
