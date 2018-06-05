using System;
using System.Collections.Generic;
using UIFrameworks.Shared.Themes.Interfaces;
using UIKit;

namespace EOS.UI.iOS.Sandbox.Storyboards
{
    public abstract class BaseViewController: UIViewController
    {
        private List<UIView> _children;
        private List<UIView> Children => _children = _children ?? GetAllChildren(View);

        public BaseViewController(IntPtr intPtr): base(intPtr) { }

		public override void ViewWillAppear(bool animated)
		{
            base.ViewWillAppear(animated);
            NavigationController.SetNavigationBarHidden(false, false);
		}

        private List<UIView> GetAllChildren(UIView view)
        {
            var result = new List<UIView>() { view };
            if(view.Subviews == null || view.Subviews.Length == 0)
                return result;

            foreach(var subView in view.Subviews)
                result.AddRange(GetAllChildren(subView));

            return result;
        }

        public void UpdateApperaence()
        {
            SetStyle();
            foreach(var view in Children)
                if(view is IEOSThemeControl eOSTheme)
                    eOSTheme.UpdateAppearance();
        }

        private void SetStyle()
        {
            //EOSStyleEnumeration style = default;

            //if(EOSThemeProvider.Instance.GetCurrentTheme() is LightEOSTheme)
            //    style = EOSStyleEnumeration.SendboxLight;

            //if(EOSThemeProvider.Instance.GetCurrentTheme() is DarkEOSTheme)
            //    style = EOSStyleEnumeration.SendboxDark;

            //EOSSendboxStyleProvider.Instance.SetEOSStyle(style);
        }
    }
}
