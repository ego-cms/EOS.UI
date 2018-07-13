using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Airbnb.Lottie;
using EOS.UI.Android.Interfaces;

namespace EOS.UI.Android.Components
{
    /// <summary>
    /// Main menu custom internal control
    /// On click lmplemented scale animation and lottie animation for image 
    /// </summary>
    internal class MainMenuButton: FrameLayout
    {
        #region fields

        private const float ShadowRadiusValue = 4f;
        private const float CornerRadius = 200f;

        private const string _forwardAnimationPath = "Animations/menu_animation_forward.json";
        private const string _backAnimationPath = "Animations/menu_animation_back.json";
        private LottieAnimationView _lottieView;

        private IIsOpened _mainControl;

        #endregion

        #region properties

        private string AnimationName => _mainControl != null && _mainControl.IsOpened ? _backAnimationPath : _forwardAnimationPath;

        #endregion

        #region .ctors

        public MainMenuButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        public MainMenuButton(Context context) : base(context)
        {
            Initialize();
        }

        public MainMenuButton(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public MainMenuButton(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(attrs);
        }

        public MainMenuButton(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize(attrs);
        }

        #endregion

        #region utility methods

        private void Initialize(IAttributeSet attrs = null)
        {
            var inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            var view = inflater.Inflate(Resource.Layout.MainMenu, this);

            var roundedDrawable = new GradientDrawable();
            roundedDrawable.SetColor(Color.LightBlue);
            roundedDrawable.SetCornerRadius(CornerRadius);
            view.SetBackgroundDrawable(roundedDrawable);
            view.Elevation = ShadowRadiusValue;
            view.TranslationZ = ShadowRadiusValue;

            _lottieView = view.FindViewById<LottieAnimationView>(Resource.Id.lottieView);
            _lottieView.SetAnimation(AnimationName);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if(e.Action == MotionEventActions.Down)
            {
                _lottieView.SetAnimation(AnimationName);
                _lottieView.PlayAnimation();
            }
            return base.OnTouchEvent(e);
        }

        public void SetIIsOpenedItem(IIsOpened isOpened)
        {
            _mainControl = isOpened;
        }

        #endregion
    }
}
