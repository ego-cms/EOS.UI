using System;
using System.Collections.Generic;
using EOS.UI.iOS.Sandbox.Storyboards;
using SharedControls = UIFrameworks.Shared.Themes.Helpers.ControlNames;

namespace EOS.UI.iOS.Sandbox.Controls
{
    public class ControlsData
    {
        private static readonly Lazy<ControlsData> _instance = new Lazy<ControlsData>(() => new ControlsData());
        public static ControlsData Instance => _instance.Value;

        public Dictionary<String, String> Names { get; }

        private ControlsData()
        {
            Names = new Dictionary<string, string>() {
                {SharedControls.BadgeLabel, BadgeLabelView.Identifier},
                {SharedControls.SimpleLabel, SimpleLabelView.Identifier},
                {SharedControls.GhostButton, GhostButtonView.Identifier},
                {SharedControls.SimpleButton, SimpleButtonView.Identifier},
                {SharedControls.FabProgress, FabProgressView.Identifier},
                {SharedControls.Input, InputControlView.Identifier},
                {SharedControls.CircleProgress, CircleProgressView.Identifier},
                {SharedControls.Section, SectionComponentView.Identifier },
                {SharedControls.CTAButton, CTAButtonView.Identifier},
                {SharedControls.WorkTimeCalendar, WorkTimeView.Identifier}
            };
        }
    }
}
