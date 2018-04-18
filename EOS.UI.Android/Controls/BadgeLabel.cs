using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.Android.Controls
{
    public class BadgeLabel: TextView, IEOSThemeControl
    {
        #region fields


        #endregion

        #region constructors

        public BadgeLabel(Context context) : base(context)
        {
            Initialize();
        }

        public BadgeLabel(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public BadgeLabel(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            Initialize(attrs);
        }

        public BadgeLabel(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        #endregion

        #region public Api

        public void SetCustomBackgroundColor(Color color)
        {
            (Background as GradientDrawable).SetColor(color);
        }

        public void SetCustomFont(string fontPath)
        {
            var font = Typeface.CreateFromAsset(Context.Assets, fontPath);
            SetTypeface(font, TypefaceStyle.Normal);
        }

        public void SetCustomLetterSpacing(float spacing)
        {
            LetterSpacing = spacing;
        }

        public void SetCustomTextColor(Color color)
        {
            SetTextColor(color);
        }

        public void SetCustomTextSize(float size)
        {
            TextSize = size;
        }

        public void SetCornerRadius(float radius)
        {
            (Background as GradientDrawable).SetCornerRadius(radius);
        }

        #endregion

        #region utility methods

        private void Initialize(IAttributeSet attrs = null)
        {
            Background = CreateDefaultDrawable();
            if(attrs != null)
                InitializeAttributes(attrs);
        }

        private GradientDrawable CreateDefaultDrawable()
        {
            var drawable = new GradientDrawable();
            drawable.SetShape(ShapeType.Rectangle);
            return drawable;
        }

        private void InitializeAttributes(IAttributeSet attrs)
        {
            //TODO: Implement set attrs logic
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
            
        }

        public void ResetCustomization()
        {
            
        }

        public IEOSStyle GetCurrentEOSStyle()
        {
            throw new NotImplementedException();
        }

        public IEOSStyle SetEOSStyle(EOSStyleEnumeration style)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}