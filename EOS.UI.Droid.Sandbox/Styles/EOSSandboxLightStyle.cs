using System.Collections.Generic;
using Android.Graphics;
using EOS.UI.Shared.Themes.Interfaces;
using EOS.UI.Shared.Themes.Helpers;

namespace EOS.UI.Droid.Sandbox.Styles
{
    public class EOSSandboxLightStyle : IEOSStyle
    {
        private const string neutralColor6 = "#F6F6F6";
        public Dictionary<string, object> ThemeValues => new Dictionary<string, object>()
        {
            { EOSConstants.NeutralColor6, Color.ParseColor(neutralColor6)},
        };
    }
}