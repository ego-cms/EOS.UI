using System;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;

namespace EOS.UI.Android.Sandbox.Styles
{
    public class EOSSendboxStyleProvider
    {
        private EOSSendboxStyleProvider()
        {
        }

        static Lazy<EOSSendboxStyleProvider> _instance = new Lazy<EOSSendboxStyleProvider>(() => new EOSSendboxStyleProvider());

        public static EOSSendboxStyleProvider Instance => _instance.Value;

        public IEOSStyle Style { get; set; } = new EOSSendboxLightStyle();

        public void SetEOSStyle(EOSStyleEnumeration style)
        {
            switch(style)
            {
                case EOSStyleEnumeration.SendboxLight:
                    Style = new EOSSendboxLightStyle();
                    break;
                case EOSStyleEnumeration.SendboxDark:
                    Style = new EOSSendboxDarkStyle();
                    break;
            }
        }
    }
}