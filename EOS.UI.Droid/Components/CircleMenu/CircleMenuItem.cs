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

        private string BlackoutTag = "Blackout";
        private const float StartAngle = -180f;
        private const float EndAngle = 0f;
        private const float PivotScale = 0.5f;
        private const int RotateDuration = 380;
        private const float ShadowRadiusValue = 8f;

        #endregion

        #region fields

        private ImageView _icon;

        private ICircleMenuClickable _circleMenu;

        private bool _isSubMenu;
        private bool _isOpened;
        private bool _hasChildren;
        private bool _forward;

        private CircleMenuScrollListener _scrollListener = new CircleMenuScrollListener();

        #endregion

        #region properties

        public int CircleMenuModelId { get; set; } = -1;

        public int HasSubMenus { get; set; }

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;

                var view = FindViewWithTag(BlackoutTag);
                (view.Background as GradientDrawable).SetColor(value ? Color.Transparent : BlackoutColor);
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

        public Color BlackoutColor { get; set; }

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
            AddView(CreateBlackoutView());
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

        private View CreateBlackoutView()
        {
            var view = new View(Context);
            var layoutParameters = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, RelativeLayout.LayoutParams.MatchParent);
            layoutParameters.AddRule(LayoutRules.CenterInParent);
            view.LayoutParameters = layoutParameters;
            var roundedDrawable = new GradientDrawable();
            roundedDrawable.SetColor(Color.Transparent);
            roundedDrawable.SetShape(ShapeType.Oval);
            view.SetBackgroundDrawable(roundedDrawable);
            view.Tag = BlackoutTag;
            return view;
        }

        #endregion

        #region overrides

        public override bool OnTouchEvent(MotionEvent e)
        {
            var isSpinned = _scrollListener.IsSpinRound(ref _forward, e);
            if((e.Action == MotionEventActions.Up || e.Action == MotionEventActions.Cancel) && Enabled && !_circleMenu.Locked && CircleMenuModelId != -1)
            {
                if(isSpinned)
                {
                    _circleMenu.PerformSwipe(_forward);
                }
                else
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
            }
            return true;
        }

        #endregion
    }
}
