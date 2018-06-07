using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Util;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.Android.Sandbox.Controls
{
    public class EOSSandboxToolbar : Toolbar, IEOSThemeControl
    {
        #region constructors

        public EOSSandboxToolbar(Context context) : base(context)
        {
            Initialize(context);
        }
        public EOSSandboxToolbar(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(context);
        }

        public EOSSandboxToolbar(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(context);
        }

        #endregion

        #region utility methods

        private void Initialize(Context context)
        {
            var activity = GetActivity(context);
            NavigationClick += (s, e) => activity.Finish();
            Title = activity.Title;
        }

        private Activity GetActivity(Context context)
        {
            while(context is ContextWrapper)
            {
                if(context is Activity)
                    return (Activity)context;

                context = ((ContextWrapper)context).BaseContext;
            }
            return null;
        }

        #endregion

        #region IEOSThemeControl implementation

        public bool IsEOSCustomizationIgnored { get; private set; }

        public IEOSThemeProvider GetThemeProvider()
        {
            return EOSThemeProvider.Instance;
        }

        public void UpdateAppearance()
        {
            if(!IsEOSCustomizationIgnored)
            {
                SetTitleTextColor(GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor1));
                SetBackgroundColor(GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor6));
                if(NavigationIcon != null)
                    NavigationIcon.SetColorFilter(GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor), PorterDuff.Mode.SrcAtop);
            }
        }

        public void ResetCustomization()
        {
            IsEOSCustomizationIgnored = false;
            UpdateAppearance();
        }

        public IEOSStyle GetCurrentEOSStyle()
        {
            return null;
        }

        public void SetEOSStyle(EOSStyleEnumeration style)
        {

        }

        #endregion
    }
}