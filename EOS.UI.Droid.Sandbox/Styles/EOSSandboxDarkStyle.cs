using System.Collections.Generic;
using Android.Graphics;
using EOS.UI.Shared.Themes.Interfaces;
using EOS.UI.Shared.Themes.Helpers;

namespace EOS.UI.Droid.Sandbox.Styles
{
    public class EOSSandboxDarkStyle: IEOSStyle
    {
        private const string neutralColor6 = "#343334";
        public Dictionary<string, object> ThemeValues => new Dictionary<string, object>()
        {
            { EOSConstants.NeutralColor6, Color.ParseColor(neutralColor6)},
        };

    }
}