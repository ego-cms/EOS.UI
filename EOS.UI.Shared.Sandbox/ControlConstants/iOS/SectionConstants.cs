#if __IOS__
using System;
using System.Collections.Generic;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Helpers;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Extensions;
using EOS.UI.Shared.Themes.Helpers;
using UIKit;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.Shared.Sandbox.ControlConstants.iOS
{
    public static class SectionConstants
    {
            public static Dictionary<string, int> Paddings =>  Sizes.PaddingsCollection;
    }
}

#endif