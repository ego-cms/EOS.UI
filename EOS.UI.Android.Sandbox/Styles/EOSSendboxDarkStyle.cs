using System.Collections.Generic;
using Android.Graphics;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Shared.Themes.Helpers;

namespace EOS.UI.Android.Sandbox.Styles
{
    public class EOSSendboxDarkStyle: IEOSStyle
    {
        private const string neutralColor6 = "#343334";
        public Dictionary<string, object> ThemeValues => new Dictionary<string, object>()
        {
            { EOSConstants.NeutralColor6, Color.ParseColor(neutralColor6)},
        };

    }
}