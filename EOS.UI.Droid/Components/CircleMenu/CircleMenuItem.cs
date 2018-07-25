using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using EOS.UI.Droid.Interfaces;
using EOS.UI.Shared.Themes.DataModels;

namespace EOS.UI.Droid.Components
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

        private ICircleMenuClickable _circleMenu;

        private bool _isSubMenu;
        private bool _isOpened;
        private bool _hasChildren;

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

        private Color _unfocusedBackgroundColor;
        public Color UnfocusedBackgroundColor
        {
            get => _unfocusedBackgroundColor;
            set
            {
                _unfocusedBackgroundColor = value;
                if(!_hasChildren || (_hasChildren && !_isOpened))
                    (Background as GradientDrawable).SetColor(value);
            }
        }

        private Color _focusedBackgroundColor;
        public Color FocusedBackgroundColor
        {
            get => _focusedBackgroundColor;
            set
            {
                _focusedBackgroundColor = value;
                if(_hasChildren && _isOpened)
                    (Background as GradientDrawable).SetColor(value);
            }
        }

        private Color _focusedIconColor;
        public Color FocusedIconColor
        {
            get => _focusedIconColor;
            set
            {
                _focusedIconColor = value;
                if(_hasChildren && _isOpened)
                    _icon?.Drawable?.SetColorFilter(value, PorterDuff.Mode.SrcIn);
            }
        }

        private Color _unfocusedIconColor;
        public Color UnfocusedIconColor
        {
            get => _unfocusedIconColor;
            set
            {
                _unfocusedIconColor = value;
                if(!_hasChildren || (_hasChildren && !_isOpened))
                    _icon?.Drawable?.SetColorFilter(value, PorterDuff.Mode.SrcIn);
            }
        }

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
            SetBackgroundDrawable(roundedDrawable);
            Elevation = ShadowRadiusValue;
        }

        public void StartRotateAnimation()
        {
            var scaleInAnimation = new RotateAnimation(StartAngle, EndAngle, Dimension.RelativeToSelf, PivotScale, Dimension.RelativeToSelf, PivotScale);
            scaleInAnimation.Duration = RotateDuration;
            _icon.StartAnimation(scaleInAnimation);
        }

        public void SetICircleMenuClicable(ICircleMenuClickable circleMenu)
        {
            _circleMenu = circleMenu;
        }

        public void SetDataFromModel(CircleMenuItemModel model, bool isSubmenu = false)
        {
            _icon.SetImageDrawable(model.ImageSource);
            UpdateIconColor();
            CircleMenuModelId = model.Id;
            _isSubMenu = isSubmenu;
            _hasChildren = model.HasChildren;
        }

        public void ResetDataFromModel()
        {
            _icon.SetImageDrawable(new ColorDrawable(Color.Transparent));
            CircleMenuModelId = -1;
        }

        private void UpdateIconColor()
        {
            _icon.Drawable.ClearColorFilter();
            if(_hasChildren)
                _icon.Drawable.SetColorFilter(_isOpened ? FocusedIconColor : UnfocusedIconColor, PorterDuff.Mode.SrcIn);
            else
                _icon.Drawable.SetColorFilter(UnfocusedIconColor, PorterDuff.Mode.SrcIn);
        }

        #endregion

        #region overrides

        public override bool OnTouchEvent(MotionEvent e)
        {
            if(e.Action == MotionEventActions.Down && Enabled && !_circleMenu.Locked)
            {
                _circleMenu.PerformClick(CircleMenuModelId, _isSubMenu, _isOpened);

                if(!_isSubMenu)
                {
                    if(_hasChildren)
                    {
                        _isOpened = !_isOpened;
                        (Background as GradientDrawable).SetColor(_isOpened ? FocusedBackgroundColor : UnfocusedBackgroundColor);
                        UpdateIconColor();
                    }
                }
            }
            return base.OnTouchEvent(e);
        }

        #endregion
    }
}
