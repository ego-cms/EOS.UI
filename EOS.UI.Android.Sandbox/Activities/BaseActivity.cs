using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using EOS.UI.Shared.Themes.Themes;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using R = Android.Resource;
using A = Android;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity]
    public class BaseActivity : Activity
    {
        private List<View> _children;
        private List<View> Children => _children = _children ?? GetAllChildren(Window.DecorView);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            RequestedOrientation = ScreenOrientation.Portrait;
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
            ActionBar.SetBackgroundDrawable(new ColorDrawable(EOSThemeProvider.Instance.GetEOSProperty<Color>(null, EOSConstants.NeutralColor4)));

            foreach(var view in Children)
                if(view is IEOSThemeControl eOSTheme)
                    eOSTheme.UpdateAppearance();
        }

        public void ResetCustomization()
        {
            foreach(var view in Children)
                if(view is IEOSThemeControl eOSTheme)
                    eOSTheme.ResetCustomization();
        }

        private List<View> GetAllChildren(View view)
        {
            if(!(view is ViewGroup)) 
                return new List<View> { view };

            var result = new List<View>() { view };

            var viewGroup = (ViewGroup)view;
            for(int i = 0; i < viewGroup.ChildCount; i++)
                result.AddRange(GetAllChildren(viewGroup.GetChildAt(i)));

            return result;
        }
    }
}