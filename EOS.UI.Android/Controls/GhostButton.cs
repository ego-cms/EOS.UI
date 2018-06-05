using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using R = Android.Resource;
using TextUtils = Android.Text.TextUtils;


namespace EOS.UI.Android.Controls
{
    public class GhostButton : Button, IEOSThemeControl
    {
        public bool IsEOSCustomizationIgnored { get; private set; }

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
                SetTextColorSet(PressedStateTextColor, _enabledTextColor, DisabledTextColor);
                IsEOSCustomizationIgnored = true;
            }
        }

        private void SetTextColorSet(Color pressedColor, Color enabledColor, Color disabledColor)
        {
            var colorSet = new ColorStateList(
            new int[][]
            {
                new int[] { R.Attribute.StatePressed },
                new int[] { R.Attribute.StateEnabled },
                new int[] { -R.Attribute.StateEnabled},
            },
            new int[]
            {
                pressedColor,
                enabledColor,
                disabledColor
            });
            base.SetTextColor(colorSet);
        }

        private Color _disabledTextColor;
        public Color DisabledTextColor
        {
            get => _disabledTextColor;
            set
            {
                _disabledTextColor = value;
                SetTextColorSet(PressedStateTextColor, EnabledTextColor, _disabledTextColor);
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
                SetTextColorSet(_pressedStateTextColor, EnabledTextColor, DisabledTextColor);
                IsEOSCustomizationIgnored = true;
            }
        }


        public GhostButton(Context context) : base(context)
        {
            Initialize();
        }

        public GhostButton(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public GhostButton(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(attrs);
        }

        public GhostButton(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize(attrs);
        }

        protected GhostButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        private void Initialize(IAttributeSet attributeSet = null)
        {
            Background = CreateRippleDrawable();
            UpdateAppearance();
            SetLines(1);
            Ellipsize = TextUtils.TruncateAt.End;
        }

        private void UpdateEnabledState(bool enabled)
        {
            base.SetTextColor(enabled ? EnabledTextColor : DisabledTextColor);
        }

        private Drawable CreateRippleDrawable()
        {
            return new RippleDrawable(new ColorStateList(
                new int[][]
                {
                    new int[] { },
                },
                new int[]
                {
                    _pressedStateTextColor
                }), 
                new ColorDrawable(Color.Transparent), new ColorDrawable(Color.White));
        }

        public override void SetTypeface(Typeface tf, [GeneratedEnum] TypefaceStyle style)
        {
            base.SetTypeface(tf, style);
            IsEOSCustomizationIgnored = true;
        }

        public IEOSStyle GetCurrentEOSStyle()
        {
            return null;
        }

        public IEOSThemeProvider GetThemeProvider()
        {
            return EOSThemeProvider.Instance;
        }

        public void ResetCustomization()
        {
            IsEOSCustomizationIgnored = false;
            UpdateAppearance();
        }

        public void SetEOSStyle(EOSStyleEnumeration style)
        {

        }

        public void UpdateAppearance()
        {
            if (!IsEOSCustomizationIgnored)
            {
                var provider = GetThemeProvider();
                base.SetTypeface(Typeface.CreateFromAsset(Context.Assets, provider.GetEOSProperty<string>(this, EOSConstants.Font)), TypefaceStyle.Normal);
                base.LetterSpacing = provider.GetEOSProperty<float>(this, EOSConstants.LetterSpacing);
                EnabledTextColor = provider.GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor);
                DisabledTextColor = provider.GetEOSProperty<Color>(this, EOSConstants.NeutralColor3);
                PressedStateTextColor = provider.GetEOSProperty<Color>(this, EOSConstants.NeutralColor6);
                base.TextSize = provider.GetEOSProperty<float>(this, EOSConstants.TextSize);
                IsEOSCustomizationIgnored = false;
            }
        }
    }
}
