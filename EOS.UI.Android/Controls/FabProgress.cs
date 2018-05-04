using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Views.Animations;
using Android.Widget;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.Android.Controls
{
    public class FabProgress : ImageButton, IEOSThemeControl
    {
        private const int _animationDuration = 200;
        private bool _isOpen;
        private Animation _openAnimation;
        private Animation _closeAnimation;
        private Drawable _normalImage;

        public bool IsEOSCustomizationIgnored { get; private set; }

        private Color _backgroundColor;
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                (Background as GradientDrawable).SetColor(value);
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
            _closeAnimation = AnimationUtils.LoadAnimation(Application.Context, Resource.Animation.FabCloseAnimation);

            Click += OnClick;

            UpdateAppearance();
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
                Image = Resources.GetDrawable(provider.GetEOSProperty<int>(this, EOSConstants.CalendarImage));
                PreloaderImage = Resources.GetDrawable(provider.GetEOSProperty<int>(this, EOSConstants.FabProgressPreloaderImage));

                var roundedDrawable = (GradientDrawable) Resources.GetDrawable(Resource.Drawable.FabButton);
                SetBackgroundDrawable(roundedDrawable);

                BackgroundColor = provider.GetEOSProperty<Color>(this, EOSConstants.FabProgressPrimaryColor);
                IsEOSCustomizationIgnored = false;
            }
        }

        async void OnClick(object sender, EventArgs e)
        {
            if (_isOpen)
            {
                StartAnimation(_closeAnimation);
                await Task.Delay(_animationDuration);
                SetImageDrawable(Image);
                _isOpen = false;
            }
            else
            {
                SetImageDrawable(PreloaderImage);
                StartAnimation(_openAnimation);
                _isOpen = true;
            }
        }
	}
}
