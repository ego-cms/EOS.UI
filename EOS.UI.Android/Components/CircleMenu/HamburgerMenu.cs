using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Airbnb.Lottie;

namespace EOS.UI.Android.Components
{
    internal class HamburgerMenu: FrameLayout
    {
        #region fields

        private LottieAnimationView _lottieView;

        #endregion

        #region .ctors

        public HamburgerMenu(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        public HamburgerMenu(Context context) : base(context)
        {
            Initialize();
        }

        public HamburgerMenu(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public HamburgerMenu(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(attrs);
        }

        public HamburgerMenu(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize(attrs);
        }

        #endregion

        #region utility methods

        private void Initialize(IAttributeSet attrs = null)
        {
            var inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            var view = inflater.Inflate(Resource.Layout.HamburgerMenu, this);

            var roundedDrawable = new GradientDrawable();
            roundedDrawable.SetColor(Color.LightBlue);
            roundedDrawable.SetCornerRadius(150f);
            view.SetBackgroundDrawable(roundedDrawable);
            view.Elevation = 4;
            view.TranslationZ = 4;

            _lottieView = view.FindViewById<LottieAnimationView>(Resource.Id.lottieView);
            _lottieView.SetAnimation("Animations/menuButton1.json");

            Click += HamburgerMenuClick;
        }

        private void HamburgerMenuClick(object sender, EventArgs e)
        {
            _lottieView.PlayAnimation();
        }

        #endregion
    }
}
