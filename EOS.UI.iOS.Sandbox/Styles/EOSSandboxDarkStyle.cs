using System.Collections.Generic;
using EOS.UI.Shared.Themes.Extensions;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Shared.Themes.Helpers;

namespace EOS.UI.iOS.Sandbox.Styles
{
    public class EOSSandboxDarkStyle : IEOSStyle
    {
        private const string neutralColor6 = "#272727";
        public Dictionary<string, object> ThemeValues => new Dictionary<string, object>()
        {
            { EOSConstants.NeutralColor6, ColorExtension.FromHex(neutralColor6)},
        };
    }
}