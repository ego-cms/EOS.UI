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
using UIKit;
using EOS.UI.Shared.Themes.Extensions;

namespace EOS.UI.iOS.Sandbox.Storyboards
{
    public abstract class BaseViewController : UIViewController
    {
        private List<UIView> _children;
        private List<UIView> Children => _children = _children ?? GetChildren();
        private UIImage _backgroundImage;

        public BaseViewController(IntPtr intPtr) : base(intPtr)
        {
            NavigationItem.BackBarButtonItem = new UIBarButtonItem { Title = ControlsData.BackTitle };
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _backgroundImage = NavigationController.NavigationBar.GetBackgroundImage(UIBarMetrics.Default);
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
            if (view.Subviews == null || view.Subviews.Length == 0)
                return result;

            foreach (var subView in view.Subviews)
                result.AddRange(GetAllSubviews(subView));

            return result;
        }

        public void UpdateApperaence()
        {
            View.BackgroundColor = EOSThemeProvider.Instance.GetEOSProperty<UIColor>(EOSConstants.NeutralColor6);
            NavigationController.NavigationBar.BarStyle = EOSThemeProvider.Instance.GetCurrentTheme() is LightEOSTheme ? UIBarStyle.Default : UIBarStyle.Black;

            SetStyle();
            foreach (var view in Children)
                if (view is IEOSThemeControl eOSTheme)
                    eOSTheme.UpdateAppearance();
        }

        private void SetStyle()
        {
            IEOSStyle style = null;

            if (EOSThemeProvider.Instance.GetCurrentTheme() is LightEOSTheme)
                style = new EOSSandboxLightStyle();

            if (EOSThemeProvider.Instance.GetCurrentTheme() is DarkEOSTheme)
                style = new EOSSandboxDarkStyle();

            EOSSandboxStyleProvider.Instance.Style = style;
        }

        protected void ToggleNavigationBar(bool enabled)
        {
            if (NavigationController == null)
                return;

            if (enabled)
            {
                NavigationController.NavigationBar.BackgroundColor = UIColor.White;
                NavigationController.NavigationBar.SetBackgroundImage(_backgroundImage, UIBarMetrics.Default);
                NavigationController.NavigationBar.BackgroundColor = UIColor.Clear;
                NavigationController.NavigationBar.Items[0].BackBarButtonItem.Enabled = true; 
                NavigationController.NavigationBar.TintColor = ColorExtension.FromHex("3C6DF0");
                NavigationController.InteractivePopGestureRecognizer.Enabled = true;
            }
            else
            {
                NavigationController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
                NavigationController.NavigationBar.BackgroundColor = UIColor.Clear;
                NavigationController.NavigationBar.Items[0].BackBarButtonItem.Enabled = false; 
                NavigationController.NavigationBar.TintColor = UIColor.LightGray;
                NavigationController.InteractivePopGestureRecognizer.Enabled = false;
            }
        }
    }
}
