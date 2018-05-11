using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.Android.Controls
{
    public class FabProgress : ImageButton, IEOSThemeControl, View.IOnTouchListener
    {
        private const int _animationDuration = 300;
        private const int _padding = 21;
        private bool _isOpen;
        private Animation _openAnimation;
        private Animation _closeAnimation;

        public bool IsEOSCustomizationIgnored { get; private set; }

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                (Background as GradientDrawable).SetColor(value ? BackgroundColor : DisabledBackgroundColor);
            }
        }

        private Color _backgroundColor;
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                if (Enabled)
                    (Background as GradientDrawable).SetColor(value);
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _disabledBackgroundColor;
        public Color DisabledBackgroundColor
        {
            get => _disabledBackgroundColor;
            set
            {
                _disabledBackgroundColor = value;
                if (!Enabled)
                    (Background as GradientDrawable).SetColor(DisabledBackgroundColor);
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _pressedBackgroundColor;
        public Color PressedBackgroundColor
        {
            get => _pressedBackgroundColor;
            set
            {
                _pressedBackgroundColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private int _buttonSize;
        public int ButtonSize
        {
            get => _buttonSize;
            set
            {
                _buttonSize = value;
                LayoutParameters.Width = value;
                LayoutParameters.Height = value;
                SetScaleType(ScaleType.FitCenter);
                RequestLayout();
                SetPadding(_padding, _padding, _padding, _padding);
                IsEOSCustomizationIgnored = true;
            }
        }

        private Drawable _image;
        public Drawable Image
        {
            get => _image;
            set
            {
                _image = value;
                SetImageDrawable(value);
                IsEOSCustomizationIgnored = true;
            }
        }

        private Drawable _preloaderImage;
        public Drawable PreloaderImage
        {
            get => _preloaderImage;
            set
            {
                _preloaderImage = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public FabProgress(Context context) : base(context)
        {
            Initialize();
        }

        public FabProgress(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize();
        }

        public FabProgress(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize();
        }

        public FabProgress(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize();
        }

        protected FabProgress(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        private void Initialize()
        {
            _openAnimation = AnimationUtils.LoadAnimation(Application.Context, Resource.Animation.FabOpenAnimation);
            _openAnimation.AnimationEnd += (sender, e) =>
              {
                  _isOpen = true;
              };
            _closeAnimation = AnimationUtils.LoadAnimation(Application.Context, Resource.Animation.FabCloseAnimation);
            _closeAnimation.AnimationEnd += (sender, e) =>
              {
                  SetImageDrawable(Image);
                  _isOpen = false;
              };
            Click += OnClick;
            SetOnTouchListener(this);
            UpdateAppearance();
            Elevation = 10f;
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
            LayoutParameters.Width = ViewGroup.LayoutParams.WrapContent;
            LayoutParameters.Height = ViewGroup.LayoutParams.WrapContent;
            RequestLayout();
        }

        public void SetEOSStyle(EOSStyleEnumeration style)
        {
        }

        public void UpdateAppearance()
        {
            if (!IsEOSCustomizationIgnored)
            {
                var provider = GetThemeProvider();
                Image = Resources.GetDrawable(provider.GetEOSProperty<int>(this, EOSConstants.CalendarImage));
                PreloaderImage = Resources.GetDrawable(provider.GetEOSProperty<int>(this, EOSConstants.FabProgressPreloaderImage));
                var roundedDrawable = (GradientDrawable)Resources.GetDrawable(Resource.Drawable.FabButton);
                SetBackgroundDrawable(roundedDrawable);
                BackgroundColor = provider.GetEOSProperty<Color>(this, EOSConstants.FabProgressPrimaryColor);
                DisabledBackgroundColor = provider.GetEOSProperty<Color>(this, EOSConstants.FabProgressDisabledColor);
                PressedBackgroundColor = provider.GetEOSProperty<Color>(this, EOSConstants.FabProgressPressedColor);
                IsEOSCustomizationIgnored = false;
            }
        }

        async void OnClick(object sender, EventArgs e)
        {
            if (_isOpen)
            {
                StartAnimation(_closeAnimation);
            }
            else
            {
                SetImageDrawable(PreloaderImage);
                StartAnimation(_openAnimation);
            }
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            if (Enabled)
            {
                if (e.Action == MotionEventActions.Down)
                    (Background as GradientDrawable).SetColor(PressedBackgroundColor);
                if (e.Action == MotionEventActions.Up || e.Action == MotionEventActions.Cancel)
                    (Background as GradientDrawable).SetColor(BackgroundColor);
            }
            return false;
        }
    }
}
