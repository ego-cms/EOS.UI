﻿#if __ANDROID__
using System.Collections.Generic;
using EOS.UI.Droid.Themes;
using EOS.UI.Shared.Helpers;
        private static ShadowConfig ShadowConfiguration => (ShadowConfig) EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.FabShadow];

        public static Dictionary<string, Color> BackgroundColors =>
#endif