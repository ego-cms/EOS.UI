using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using EOS.UI.iOS.Sandbox.Controls;
using EOS.UI.iOS.Sandbox.Styles;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Interfaces;
using EOS.UI.Shared.Themes.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIKit;

namespace EOS.UI.iOS.Sandbox.Storyboards
{
    public abstract class BaseViewController : UIViewController
    {
        private List<UIView> _children;
        private List<UIView> Children => _children = _children ?? GetChildren();

        public BaseViewController(IntPtr intPtr): base(intPtr)
        {
            NavigationItem.BackBarButtonItem = new UIBarButtonItem { Title = ControlsData.BackTitle };
        }

        public override void ViewWillAppear(bool animated)
		{
            base.ViewWillAppear(animated);
            NavigationController.SetNavigationBarHidden(false, false);
            UpdateApperaence();
		}

        private List<UIView> GetChildren()
        {
            var result = GetAllSubviews(View);
            result.Add(NavigationController.NavigationBar);
            return result;
        }

        private List<UIView> GetAllSubviews(UIView view)
        {
            var result = new List<UIView>() { view };
            if(view.Subviews == null || view.Subviews.Length == 0)
                return result;

            foreach(var subView in view.Subviews)
                result.AddRange(GetAllSubviews(subView));

            return result;
        }

        public void UpdateApperaence()
        {
            View.BackgroundColor = EOSThemeProvider.Instance.GetEOSProperty<UIColor>(EOSConstants.NeutralColor6);
            NavigationController.NavigationBar.BarStyle = EOSThemeProvider.Instance.GetCurrentTheme() is LightEOSTheme ? UIBarStyle.Default : UIBarStyle.Black;

            SetStyle();
            foreach(var view in Children)
                if(view is IEOSThemeControl eOSTheme)
                    eOSTheme.UpdateAppearance();
        }

        private void SetStyle()
        {
            IEOSStyle style = null;

            if(EOSThemeProvider.Instance.GetCurrentTheme() is LightEOSTheme)
                style = new EOSSandboxLightStyle();

            if(EOSThemeProvider.Instance.GetCurrentTheme() is DarkEOSTheme)
                style = new EOSSandboxDarkStyle();

            EOSSandboxStyleProvider.Instance.Style = style;
        }
    }
}
