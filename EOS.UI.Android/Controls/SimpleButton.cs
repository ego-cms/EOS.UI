using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using R = Android.Resource;

namespace EOS.UI.Android.Controls
{
    public class SimpleButton: Button, IEOSThemeControl
    {
        #region fields

        private int[][] _states;

        #endregion

        #region constructors

        public SimpleButton(Context context) : base(context)
        {
            Initialize();
        }

        public SimpleButton(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public SimpleButton(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            Initialize(attrs);
        }

        public SimpleButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        #endregion

        #region customization

        public override Typeface Typeface
        {
            get => base.Typeface;
            set
            {
                base.Typeface = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public override float LetterSpacing
        {
            get => base.LetterSpacing;
            set
            {
                base.LetterSpacing = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public override float TextSize
        {
            get => base.TextSize;
            set
            {
                base.TextSize = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _enabledTextColor;
        public Color EnabledTextColor
        {
            get => _enabledTextColor;
            set
            {
                _enabledTextColor = value;
                var colorSet = new ColorStateList(
                new int[][]
                {
                    new int[] { R.Attribute.StateEnabled },
                    new int[] { -R.Attribute.StateEnabled},
                    new int[] { R.Attribute.StatePressed },
                },
                new int[]
                {
                    _enabledTextColor,
                    DisabledTextColor,
                    PressedStateTextColor
                });
                base.SetTextColor(colorSet);
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _disabledTextColor;
        public Color DisabledTextColor
        {
            get => _disabledTextColor;
            set
            {
                _disabledTextColor = value;
                var colorSet = new ColorStateList(
                new int[][]
                {
                    new int[] { R.Attribute.StateEnabled },
                    new int[] { -R.Attribute.StateEnabled },
                    new int[] { R.Attribute.StatePressed },
                },
                new int[]
                {
                    EnabledTextColor,
                    _disabledTextColor,
                    PressedStateTextColor,
                });
                base.SetTextColor(colorSet);
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _pressedStateTextColor;
        public Color PressedStateTextColor
        {
            get => _pressedStateTextColor;
            set
            {
                _pressedStateTextColor = value;
                var colorSet = new ColorStateList(
                new int[][]
                {
                    new int[] { R.Attribute.StateEnabled },
                    new int[] { -R.Attribute.StateEnabled},
                    new int[] { R.Attribute.StatePressed },
                },
                new int[]
                {
                    EnabledTextColor,
                    DisabledTextColor,
                    _pressedStateTextColor,
                });
                base.SetTextColor(colorSet);
                IsEOSCustomizationIgnored = true;
            }
        }

        #endregion

        #region utility methods

        private void Initialize(IAttributeSet attrs = null)
        {
            SetMaxLines(1);
            if(attrs != null)
                InitializeAttributes(attrs);
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
            if(!IsEOSCustomizationIgnored)
            {
                base.SetTypeface(Typeface.CreateFromAsset(Context.Assets, GetThemeProvider().GetEOSProperty<string>(this, EOSConstants.Font)), TypefaceStyle.Normal);
                base.LetterSpacing = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.LetterSpacing);
                base.SetTextColor(GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.PrimaryColor));
                base.TextSize = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.TextSize);
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
            var provider = GetThemeProvider();
            base.SetTypeface(Typeface.CreateFromAsset(Context.Assets, provider.GetEOSProperty<string>(this, EOSConstants.Font)), TypefaceStyle.Normal);
            base.LetterSpacing = provider.GetEOSProperty<float>(this, EOSConstants.LetterSpacing);
            EnabledTextColor = provider.GetEOSProperty<Color>(this, EOSConstants.PrimaryColor);
            DisabledTextColor = provider.GetEOSProperty<Color>(this, EOSConstants.DisabledTextColor);
            PressedStateTextColor = provider.GetEOSProperty<Color>(this, EOSConstants.PressedStateTextColor);
            base.TextSize = provider.GetEOSProperty<float>(this, EOSConstants.TextSize);
            IsEOSCustomizationIgnored = false;
        }

        #endregion


    }
}