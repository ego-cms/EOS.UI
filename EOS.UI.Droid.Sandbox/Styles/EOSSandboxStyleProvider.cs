using System;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;

namespace EOS.UI.Droid.Sandbox.Styles
{
    public class EOSSandboxStyleProvider
    {
        private EOSSandboxStyleProvider()
        {
        }

        static Lazy<EOSSandboxStyleProvider> _instance = new Lazy<EOSSandboxStyleProvider>(() => new EOSSandboxStyleProvider());

        public static EOSSandboxStyleProvider Instance => _instance.Value;

        public IEOSStyle Style { get; set; } = new EOSSandboxLightStyle();

    }
}