using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using EOS.UI.Android.Interfaces;
using EOS.UI.Shared.Themes.DataModels;

namespace EOS.UI.Android.Components
{
    /// <summary>
    /// Menu custom internal control
    /// On scroll lmplemented rotation animation 
    /// </summary>
    internal class CircleMenuItem : FrameLayout
    {
        #region constants

        private const float StartAngle = -180f;
        private const float EndAngle = 0f;
        private const float PivotScale = 0.5f;
        private const int RotateDuration = 380;
        private const float ShadowRadiusValue = 8f;
        private const float EnabledAlpha = 1f;
        private const float DisabledAlpha = 0.6f;

        #endregion

        #region fields

        private ImageView _icon;

        private ICircleMenuClicable _circleMenu;

        private bool _isSubMenu;
        private bool _isOpened;

        #endregion

        #region properties

        public int CircleMenuModelId { get; set; }

        public int HasSubMenus { get; set; }

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;

                var alpha = Enabled ? EnabledAlpha : DisabledAlpha;
                _icon.Alpha = alpha;
                Alpha = alpha;
            }
        }

        public Color MainColor { get; set; }

        public Color FocusedMainColor { get; set; }

        public Color FocusedButtonMainColor { get; set; }

        public Color UnfocusedButtonMainColor { get; set; }

        #endregion

        #region .ctors

        public CircleMenuItem(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        public CircleMenuItem(Context context) : base(context)
        {
            Initialize();
        }

        public CircleMenuItem(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public CircleMenuItem(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(attrs);
        }

        public CircleMenuItem(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize(attrs);
        }

        #endregion

        #region utility methods

        private void Initialize(IAttributeSet attrs = null)
        {
            var inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            var view = inflater.Inflate(Resource.Layout.CircleMenuItem, this);
            _icon = view.FindViewById<ImageView>(Resource.Id.icon);
            var roundedDrawable = new GradientDrawable();
            roundedDrawable.SetColor(Color.White);
            roundedDrawable.SetShape(ShapeType.Oval);
            view.SetBackgroundDrawable(roundedDrawable);
            view.Elevation = ShadowRadiusValue;
        }

        public void StartRotateAnimation()
        {
            var scaleInAnimation = new RotateAnimation(StartAngle, EndAngle, Dimension.RelativeToSelf, PivotScale, Dimension.RelativeToSelf, PivotScale);
            scaleInAnimation.Duration = RotateDuration;
            _icon.StartAnimation(scaleInAnimation);
        }

        public void SetICircleMenuClicable(ICircleMenuClicable circleMenu)
        {
            _circleMenu = circleMenu;
        }

        public void SetDataFromModel(Drawable drawable, int id, bool isSubmenu = false)
        {
            _icon.SetImageDrawable(drawable);
            CircleMenuModelId = id;
            _isSubMenu = isSubmenu;
        }

        public void ResetDataFromModel()
        {
            _icon.SetImageDrawable(new ColorDrawable(Color.Transparent));
            CircleMenuModelId = -1;
        }

        #endregion

        #region overrides

        public override bool OnTouchEvent(MotionEvent e)
        {
            if(e.Action == MotionEventActions.Down && Enabled && !_circleMenu.Locked)
            {
                _circleMenu.PerformClick(CircleMenuModelId, _isSubMenu, _isOpened);

                if(!_isSubMenu)
                    _isOpened = !_isOpened;
            }

            return base.OnTouchEvent(e);
        }

        #endregion
    }
}
