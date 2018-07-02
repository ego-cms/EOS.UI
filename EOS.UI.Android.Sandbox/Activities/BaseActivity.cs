using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Interfaces;
using R = Android.Resource;
using Android.Support.V7.App;
using EOS.UI.Android.Sandbox.Styles;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Themes;
using EOS.UI.Shared.Themes.Interfaces;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Theme = "@style/Sandbox.Main")]
    public class BaseActivity : AppCompatActivity
    {
        private List<IEOSThemeControl> _children;
        private List<IEOSThemeControl> Children => _children = _children ?? GetAllChildren(Window.DecorView);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestedOrientation = ScreenOrientation.Portrait;
        }

        protected override void OnStart()
        {
            base.OnStart();
            UpdateApperaence();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case R.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public void UpdateApperaence()
        {
            SetStyle();
            foreach(var view in Children)
                view.UpdateAppearance();
        }

        public void ResetCustomization()
        {
            SetStyle();
            foreach(var view in Children)
                view.ResetCustomization();
        }

        private List<IEOSThemeControl> GetAllChildren(View view)
        {
            if(!(view is ViewGroup))
                if(view is IEOSThemeControl eOSTheme)
                    return new List<IEOSThemeControl> { eOSTheme };
                else
                    return new List<IEOSThemeControl>();

            var result = new List<IEOSThemeControl>();
            if(view is IEOSThemeControl eos)
                result.Add(eos);

            var viewGroup = (ViewGroup)view;
            for(int i = 0; i < viewGroup.ChildCount; i++)
                result.AddRange(GetAllChildren(viewGroup.GetChildAt(i)));

            return result;
        }

        private void SetStyle()
        {
            IEOSStyle style = default;

            if(EOSThemeProvider.Instance.GetCurrentTheme() is LightEOSTheme)
                style = new EOSSandboxLightStyle();

            if(EOSThemeProvider.Instance.GetCurrentTheme() is DarkEOSTheme)
                style = new EOSSandboxDarkStyle();

            EOSSandboxStyleProvider.Instance.Style = style;
        }
    }
}
