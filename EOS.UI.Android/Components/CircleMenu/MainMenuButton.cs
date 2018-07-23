using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Com.Airbnb.Lottie;
using Com.Airbnb.Lottie.Model;
using Com.Airbnb.Lottie.Value;
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

        private const float StartScale = 1f;
        private const float EndScale = 0.9f;
        private const float PivotScale = 0.5f;
        private const int ScaleDimention = 150;

        private const float ShadowRadiusValue = 8f;

        private const string _forwardAnimationPath = "Animations/menu_animation_forward.json";
        private const string _backAnimationPath = "Animations/menu_animation_back.json";
        private LottieAnimationView _lottieView;

        private IIsOpened _mainControl;

        #endregion

        #region properties

        private string AnimationName => _mainControl != null && _mainControl.IsOpened ? _backAnimationPath : _forwardAnimationPath;

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                Alpha = Enabled ? 1f : 0.6f;
            }
        }

        public Color MainColor
        {
            set => (Background as GradientDrawable).SetColor(value);
        }

        private Color _unfocusedButtonColor;
        public Color UnfocusedButtonColor
        {
            get => _unfocusedButtonColor;
            set
            {
                _unfocusedButtonColor = value;
                _lottieView.AddValueCallback(new KeyPath("**"),
                   LottieProperty.ColorFilter,
                   new LottieValueCallback(new SimpleColorFilter(value)));
            }
        }

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
            roundedDrawable.SetColor(Color.White);
            roundedDrawable.SetShape(ShapeType.Oval);
            SetBackgroundDrawable(roundedDrawable);
            Elevation = ShadowRadiusValue;

            _lottieView = view.FindViewById<LottieAnimationView>(Resource.Id.lottieView);
            _lottieView.SetAnimation(AnimationName);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if(e.Action == MotionEventActions.Down && Enabled)
            {
                _lottieView.SetAnimation(AnimationName);
                SetCustomColorToLottieView();
                _lottieView.PlayAnimation();
                StartTouchAnimation();
            }
            return base.OnTouchEvent(e);
        }

        public void SetIIsOpenedItem(IIsOpened isOpened)
        {
            _mainControl = isOpened;
        }

        private void StartTouchAnimation()
        {
            var scaleInAnimation = new ScaleAnimation(StartScale, EndScale, StartScale, EndScale, Dimension.RelativeToSelf, PivotScale, Dimension.RelativeToSelf, PivotScale);
            scaleInAnimation.Duration = ScaleDimention;
            StartAnimation(scaleInAnimation);
        }

        private void SetCustomColorToLottieView()
        {
            _lottieView.AddValueCallback(new KeyPath("**"),
                   LottieProperty.ColorFilter,
                   new LottieValueCallback(new SimpleColorFilter(UnfocusedButtonColor)));
        }

        #endregion
    }
}
