using System.Collections.Generic;
using EOS.UI.Shared.Themes.Extensions;
using EOS.UI.Shared.Themes.Interfaces;
using EOS.UI.Shared.Themes.Helpers;

namespace EOS.UI.iOS.Sandbox.Styles
{
    public class EOSSandboxLightStyle : IEOSStyle
    {
        private const string neutralColor6 = "#F6F6F6";
        public Dictionary<string, object> ThemeValues => new Dictionary<string, object>()
        {
            { EOSConstants.NeutralColor6, ColorExtension.FromHex(neutralColor6)},
        };
    }
}