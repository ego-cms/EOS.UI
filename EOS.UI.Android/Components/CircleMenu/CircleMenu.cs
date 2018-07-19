using System;
using System.Collections.Generic;
using System.Linq;
using Android.Animation;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Support.Animation;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using EOS.UI.Android.Interfaces;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Java.Lang;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.Android.Components
{
    public class CircleMenu: FrameLayout, View.IOnTouchListener, IRunnable, DynamicAnimation.IOnAnimationEndListener, IIsOpened, IEOSThemeControl, ICircleMenuClicable
    {
        #region fields and properties

        private const int RightMargin = 29;
        private const int BottomMargin = 110;
        private const int HintElevationValue = 2;
        private const int HintAnimationDuration = 300;
        private const int HintAnimationDeltaY = 10;
        private const int SubMenuMargin = 11;

        private const float Diameter = 52f;
        private const float StartDelta = 94f;
        private const float Delta1 = -16f;
        private const float Delta2 = 26f;
        private const float Delta3 = 60f;
        private const float Delta4 = 64f;

        private const float MinSwipeWidth = 50;
        private const int SwipeAnimateDuration = 300;
        private const int ShowHideAnimateDuration = 50;

        private float[][] _deltaClosePositions = new float[6][];
        private float[][] _deltaOpenPositions = new float[6][];
        private float[][] _deltaForwardPositions = new float[6][];
        private float[][] _deltaBackPositions = new float[6][];

        private int _startMenuItemsPosition = 1;
        private float _deltaNormalizePositions;

        private bool _forward;
        private bool _normalize;
        private bool _isScrolling;
        private int _showMenuItemsIteration;
        private bool _isSubMenuOpened;

        private MainMenuButton _mainMenu;
        private RelativeLayout _container;
        private float _bufferX;
        private float _bufferY;
        private bool _isMovedRight;
        private bool _isMovedLeft;
        private List<CircleMenuItem> _menuItems = new List<CircleMenuItem>();

        private bool IsBusy => _isScrolling || _showMenuItemsIteration > 0 || _isSubMenuOpened;
        public bool ShowHintAnimation { get; set; } = true;

        #endregion

        #region events

        public event EventHandler<int> Clicked;

        #endregion

        #region IIsOpened implementation

        public bool IsOpened { get; private set; }

        #endregion

        #region .ctors

        public CircleMenu(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        public CircleMenu(Context context) : base(context)
        {
            Initialize();
        }

        public CircleMenu(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public CircleMenu(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(attrs);
        }

        public CircleMenu(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize(attrs);
        }

        #endregion

        #region customization

        private Color _mainColor;
        public Color MainColor
        {
            get => _mainColor;
            set
            {
                _mainColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _focusedMainColor;
        public Color FocusedMainColor
        {
            get => _focusedMainColor;
            set
            {
                _focusedMainColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _focusedButtonMainColor;
        public Color FocusedButtonMainColor
        {
            get => _focusedButtonMainColor;
            set
            {
                _focusedButtonMainColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _unfocusedButtonMainColor;
        public Color UnfocusedButtonMainColor
        {
            get => _unfocusedButtonMainColor;
            set
            {
                _unfocusedButtonMainColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private List<CircleMenuItemModel> _circleMenuItems;
        public List<CircleMenuItemModel> CircleMenuItems
        {
            get => _circleMenuItems;
            set
            {
                if(value?.Count > 9 || value?.Count < 4)
                    throw new ArgumentOutOfRangeException("Items should be more then 4 and less then 9");

                _circleMenuItems = value;

                _startMenuItemsPosition = 1;

                if(!IsOpened)
                    _menuItems[1].SetDataFromModel(_circleMenuItems[0].ImageSource, _circleMenuItems[0].Id);

                _menuItems[2].SetDataFromModel(_circleMenuItems[1].ImageSource, _circleMenuItems[1].Id);
                _menuItems[3].SetDataFromModel(_circleMenuItems[2].ImageSource, _circleMenuItems[2].Id);
                _menuItems[4].SetDataFromModel(_circleMenuItems[3].ImageSource, _circleMenuItems[3].Id);

                IsEOSCustomizationIgnored = true;
            }
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
                IsEOSCustomizationIgnored = false;
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

        }

        #endregion

        #region public API

        public void Attach(ViewGroup viewGroup)
        {
            var parameters = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, RelativeLayout.LayoutParams.MatchParent);
            viewGroup.AddView(this, parameters);
        }

        #endregion

        #region utility methods

        private void Initialize(IAttributeSet attrs = null)
        {
            var inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            var view = inflater.Inflate(Resource.Layout.CircleMenu, this);
            _container = view.FindViewById<RelativeLayout>(Resource.Id.container);

            _mainMenu = view.FindViewById<MainMenuButton>(Resource.Id.hamburgerMenu);
            _mainMenu.SetIIsOpenedItem(this);
            _mainMenu.Click += MainMenuClick;

            _container.SetOnTouchListener(this);

            _menuItems.AddRange(new List<CircleMenuItem>
            {
                FindViewById<CircleMenuItem>(Resource.Id.menu0),
                FindViewById<CircleMenuItem>(Resource.Id.menu1),
                FindViewById<CircleMenuItem>(Resource.Id.menu2),
                FindViewById<CircleMenuItem>(Resource.Id.menu3),
                FindViewById<CircleMenuItem>(Resource.Id.menu4),
                FindViewById<CircleMenuItem>(Resource.Id.menu5),
            });

            foreach(var menu in _menuItems)
                menu.SetICircleMenuClicable(this);

            InitDeltaArrays();

            _deltaNormalizePositions = (Diameter + StartDelta) * Context.Resources.DisplayMetrics.Density;
        }

        /// <summary>
        /// Delta arrays should be filled for two actions: close/open action and scroll action
        /// Each action sho?ld contains deltas for forward (left to right) and back (right to left) translation
        /// Delta values are constants, which calculated from design like 
        /// </summary>
        private void InitDeltaArrays()
        {
            var denisty = Context.Resources.DisplayMetrics.Density;
            _deltaClosePositions[0] = new float[] { Delta1 * denisty, 0f };
            _deltaClosePositions[1] = new float[] { -Delta4 * denisty, Delta1 * denisty };
            _deltaClosePositions[2] = new float[] { -Delta3 * denisty, Delta2 * denisty };
            _deltaClosePositions[3] = new float[] { -Delta2 * denisty, Delta3 * denisty };
            _deltaClosePositions[4] = new float[] { -Delta1 * denisty, Delta4 * denisty };
            _deltaClosePositions[5] = new float[] { 0f, -Delta1 * denisty };

            _deltaOpenPositions[0] = new float[] { 0f, Delta1 * denisty };
            _deltaOpenPositions[5] = new float[] { Delta1 * denisty, -Delta4 * denisty };
            _deltaOpenPositions[4] = new float[] { Delta2 * denisty, -Delta3 * denisty };
            _deltaOpenPositions[3] = new float[] { Delta3 * denisty, -Delta2 * denisty };
            _deltaOpenPositions[2] = new float[] { Delta4 * denisty, -Delta1 * denisty };
            _deltaOpenPositions[1] = new float[] { -Delta1 * denisty, 0f };

            _deltaForwardPositions[0] = new float[] { 0f, Delta1 * denisty };
            _deltaForwardPositions[1] = new float[] { Delta1 * denisty, -Delta4 * denisty };
            _deltaForwardPositions[2] = new float[] { Delta2 * denisty, -Delta3 * denisty };
            _deltaForwardPositions[3] = new float[] { Delta3 * denisty, -Delta2 * denisty };
            _deltaForwardPositions[4] = new float[] { Delta4 * denisty, -Delta1 * denisty };
            _deltaForwardPositions[5] = new float[] { -Delta1 * denisty, 0f };

            _deltaBackPositions[0] = new float[] { Delta1 * denisty, 0f };
            _deltaBackPositions[5] = new float[] { -Delta4 * denisty, Delta1 * denisty };
            _deltaBackPositions[4] = new float[] { -Delta3 * denisty, Delta2 * denisty };
            _deltaBackPositions[3] = new float[] { -Delta2 * denisty, Delta3 * denisty };
            _deltaBackPositions[2] = new float[] { -Delta1 * denisty, Delta4 * denisty };
            _deltaBackPositions[1] = new float[] { 0f, -Delta1 * denisty };
        }

        private void MainMenuClick(object sender, EventArgs e)
        {
            if(_showMenuItemsIteration == 0 && !_isSubMenuOpened)
                ShowMenuItemsAnimation();
        }

        /// <summary>
        /// Method which spin round items by swipe action.
        /// If we should normlize position of invisible item in method transmit bool flag.
        /// Animation animated X and Y with spring interpolator (implemented with native SpringAnimation)
        /// Spin round can be forward (left to right) and back (right to left)
        /// </summary>
        /// <param name="normalize">flag for normalize position of invisible item</param>
        private void MoveMenuItemsAnimation(bool normalize = true)
        {
            if(normalize)
            {
                _normalize = true;
                if(_forward)
                {
                    _menuItems[0].Animate().WithEndAction(this).X(Width - _deltaNormalizePositions).Y(Height).SetDuration(1);

                    if(_startMenuItemsPosition > 0)
                        --_startMenuItemsPosition;
                    else
                        _startMenuItemsPosition = _circleMenuItems.Count - 1;

                    _menuItems[1].SetDataFromModel(CircleMenuItems[_startMenuItemsPosition].ImageSource, CircleMenuItems[_startMenuItemsPosition].Id);
                }
                else
                {
                    _menuItems[0].Animate().WithEndAction(this).X(Width).Y(Height - _deltaNormalizePositions).SetDuration(1);

                    if(_startMenuItemsPosition == _circleMenuItems.Count - 1)
                    {
                        _startMenuItemsPosition = 0;
                        _menuItems[5].SetDataFromModel(CircleMenuItems[_startMenuItemsPosition + 2].ImageSource, CircleMenuItems[_startMenuItemsPosition + 2].Id);
                    }
                    else
                    {
                        ++_startMenuItemsPosition;
                        if(_startMenuItemsPosition == _circleMenuItems.Count - 1)
                            _menuItems[5].SetDataFromModel(CircleMenuItems[0].ImageSource, CircleMenuItems[0].Id);
                        else
                            _menuItems[5].SetDataFromModel(CircleMenuItems[_startMenuItemsPosition].ImageSource, CircleMenuItems[_startMenuItemsPosition].Id);
                    }
                }
            }
            else
            {
                for(int i = 0; i < _menuItems.Count; i++)
                {
                    var x =  _forward ? _deltaForwardPositions[i][0] : _deltaBackPositions[i][0];
                    var y = _forward ? _deltaForwardPositions[i][1] : _deltaBackPositions[i][1];
                    var denisty = Context.Resources.DisplayMetrics.Density;

                    var menu = _menuItems[i];
                    var springX = new SpringAnimation(menu, DynamicAnimation.TranslationX, menu.TranslationX + x);
                    var springY = new SpringAnimation(menu, DynamicAnimation.TranslationY, menu.TranslationY + y);

                    if(i == _menuItems.Count - 1)
                        springY.AddEndListener(this);

                    springX.Start();
                    springY.Start();
                }
            }
        }

        /// <summary>
        /// Method which spin round items by swipe action.
        /// If we should normlize position of invisible item in method transmit bool flag.
        /// Animation has 5 iterations. First 4 iterations animated X and Y without interpolator, and last fith spring interpolator (implemented with native SpringAnimation)
        /// Spin round can be forward (left to right) and back (right to left)
        /// </summary>
        private void ShowMenuItemsAnimation()
        {
            ++_showMenuItemsIteration;
            if(IsOpened)
            {
                if(_startMenuItemsPosition > 0)
                    --_startMenuItemsPosition;
                else
                    _startMenuItemsPosition = _circleMenuItems.Count - 1;

                _menuItems[1].SetDataFromModel(CircleMenuItems[_startMenuItemsPosition].ImageSource, CircleMenuItems[_startMenuItemsPosition].Id);

                for(int i = 0; i < _menuItems.Count - _showMenuItemsIteration; i++)
                {
                    var deltaX = _deltaOpenPositions[_menuItems.Count - i - _showMenuItemsIteration][0];
                    var deltaY = _deltaOpenPositions[_menuItems.Count - i - _showMenuItemsIteration][1];

                    if(i == _menuItems.Count - _showMenuItemsIteration - 1)
                        _menuItems[i + 1].Animate().XBy(deltaX).YBy(deltaY).SetDuration(ShowHideAnimateDuration).WithEndAction(this);
                    else
                        _menuItems[i + 1].Animate().XBy(deltaX).YBy(deltaY).SetDuration(ShowHideAnimateDuration);
                }
            }
            else
            {
                if(_showMenuItemsIteration == 1)
                    foreach(var menu in _menuItems)
                        menu.StartRotateAnimation();

                for(int i = 0; i < _showMenuItemsIteration; i++)
                {
                    var deltaX = _deltaClosePositions[i][0];
                    var deltaY = _deltaClosePositions[i][1];
                    var menu = _menuItems[_showMenuItemsIteration - i];

                    if(_showMenuItemsIteration != _menuItems.Count - 1)
                    {
                        if(i == _showMenuItemsIteration - 1)
                            menu.Animate().XBy(deltaX).YBy(deltaY).SetDuration(ShowHideAnimateDuration).WithEndAction(this);
                        else
                            menu.Animate().XBy(deltaX).YBy(deltaY).SetDuration(ShowHideAnimateDuration);
                    }
                    else
                    {
                        var springX = new SpringAnimation(menu, DynamicAnimation.TranslationX, menu.TranslationX + deltaX);
                        var springY = new SpringAnimation(menu, DynamicAnimation.TranslationY, menu.TranslationY + deltaY);

                        if(i == _showMenuItemsIteration - 1)
                        {
                            _container.SetBackgroundColor(!IsOpened ? Color.Argb(50, 0, 0, 0) : Color.Transparent);
                            springY.AddEndListener(this);
                        }

                        springX.Start();
                        springY.Start();
                    }
                }
            }
        }

        /// <summary>
        /// Hint animation shows one time on start of application
        /// </summary>
        /// <param name="action">After animation complete action</param>
        private void StartHintAnimation(Action action)
        {
            ShowHintAnimation = false;

            var lastView = CreateHintView();
            var middleView = CreateHintView();

            var translateDown = CreateHintAnimation(false);

            translateDown.AnimationEnd += (s, e) =>
            {
                _container.RemoveViewAt(0);
                _container.RemoveViewAt(0);

                lastView?.Dispose();
                middleView?.Dispose();

                action?.Invoke();
            };

            var translateUp = CreateHintAnimation();

            lastView.Elevation = HintElevationValue;
            middleView.Elevation = HintElevationValue * 2;

            _container.AddView(middleView, 0);
            _container.AddView(lastView, 0);

            lastView.StartAnimation(translateDown);
            _menuItems[4].StartAnimation(translateUp);
        }

        private TranslateAnimation CreateHintAnimation(bool isUp = true)
        {
            var delta = (isUp ? -HintAnimationDeltaY : HintAnimationDeltaY) * Context.Resources.DisplayMetrics.Density;
            var translateAnimation = new TranslateAnimation(0, 0, 0, delta);
            translateAnimation.Interpolator = new DecelerateInterpolator();
            translateAnimation.FillAfter = false;
            translateAnimation.Duration = HintAnimationDuration;
            return translateAnimation;
        }

        private ObjectAnimator CreateAlphaAnimation(CircleMenuItem menu, int duration, int startDelay, bool isShow = true)
        {
            var alfaAnimation = isShow? ObjectAnimator.OfFloat(menu, "Alpha", 1f) : ObjectAnimator.OfFloat(menu, "Alpha", 0f);
            alfaAnimation.SetDuration(duration);
            alfaAnimation.StartDelay = startDelay;
            return alfaAnimation;
        }

        private View CreateHintView()
        {
            var hintView = new View(Context);
            var layoutParameters = new RelativeLayout.LayoutParams(
                (int)(Diameter * Context.Resources.DisplayMetrics.Density),
                (int)(Diameter * Context.Resources.DisplayMetrics.Density));

            layoutParameters.RightMargin = (int)(RightMargin * Context.Resources.DisplayMetrics.Density);
            layoutParameters.BottomMargin = (int)(BottomMargin * Context.Resources.DisplayMetrics.Density);

            layoutParameters.AddRule(LayoutRules.AlignParentBottom);
            layoutParameters.AddRule(LayoutRules.AlignParentRight);

            hintView.LayoutParameters = layoutParameters;

            var roundedDrawable = new GradientDrawable();
            roundedDrawable.SetColor(Color.White);
            roundedDrawable.SetShape(ShapeType.Oval);

            hintView.SetBackgroundDrawable(roundedDrawable);

            return hintView;
        }

        private CircleMenuItem CreateSubMenu(int buttonMargin, int rightMargin)
        {
            var subMenu = new CircleMenuItem(Context);
            var layoutParameters = new RelativeLayout.LayoutParams(
                (int)(Diameter * Context.Resources.DisplayMetrics.Density),
                (int)(Diameter * Context.Resources.DisplayMetrics.Density));

            layoutParameters.RightMargin = (int)(rightMargin * Context.Resources.DisplayMetrics.Density);
            layoutParameters.BottomMargin = (int)(buttonMargin * Context.Resources.DisplayMetrics.Density);

            layoutParameters.AddRule(LayoutRules.AlignParentBottom);
            layoutParameters.AddRule(LayoutRules.AlignParentRight);

            subMenu.LayoutParameters = layoutParameters;

            var roundedDrawable = new GradientDrawable();
            roundedDrawable.SetColor(Color.White);
            roundedDrawable.SetShape(ShapeType.Oval);

            subMenu.SetBackgroundDrawable(roundedDrawable);

            subMenu.Alpha = 0f;

            return subMenu;
        }

        #endregion

        #region IOnTouchListener implementation

        public bool OnTouch(View v, MotionEvent e)
        {
            if(!IsBusy)
            {
                if(e.Action == MotionEventActions.Down)
                {
                    _bufferX = e.RawX;
                    _bufferY = e.RawY;
                }
                else if(e.Action == MotionEventActions.Move && _bufferX != e.RawX)
                {
                    //checking if swipe was horizontal and not vertical
                    if(_bufferX - e.RawX > MinSwipeWidth && System.Math.Abs(_bufferX - e.RawX) > System.Math.Abs(_bufferY - e.RawY))
                        _isMovedLeft = true;
                    if(_bufferX - e.RawX < -MinSwipeWidth && System.Math.Abs(_bufferX - e.RawX) > System.Math.Abs(_bufferY - e.RawY))
                        _isMovedRight = true;
                }
                if(e.Action == MotionEventActions.Up && !_isMovedRight && !_isMovedLeft)
                {
                }
                if(e.Action == MotionEventActions.Up && (_isMovedRight || _isMovedLeft))
                {
                    _forward = _isMovedRight;
                    _isScrolling = true;
                    MoveMenuItemsAnimation();
                    _bufferX = 0f;
                    _bufferY = 0f;
                    _isMovedRight = false;
                    _isMovedLeft = false;
                }
            }
            return IsOpened;
        }

        #endregion

        #region IRunnable implementation

        public void Run()
        {
            //checked if need normalize position of invisible item
            if(_normalize)
            {
                _normalize = false;
                MoveMenuItemsAnimation(false);
            }
            else if(_showMenuItemsIteration > 0)
            {
                if(_showMenuItemsIteration != _menuItems.Count - 1)
                {
                    //Invoke animation until all items not on theirs positions
                    ShowMenuItemsAnimation();
                }
                else if(IsOpened)
                {
                    //after end of animation setup internal values to default and change background color
                    _showMenuItemsIteration = 0;
                    IsOpened = !IsOpened;
                    _container.SetBackgroundColor(IsOpened ? Color.Argb(50, 0, 0, 0) : Color.Transparent);
                }
            }
        }

        #endregion

        #region IOnAnimationEndListener implementation

        public void OnAnimationEnd(DynamicAnimation animation, bool canceled, float value, float velocity)
        {
            if(_isScrolling)
            {
                //After end of swipe animation we should change indexes to default for items collection
                //and set invisible item to default position

                //HACK: if ImageView was out of the bounds it is invisible
                _menuItems[0].Visibility = ViewStates.Invisible;
                _menuItems[0].Visibility = ViewStates.Visible;

                if(_forward)
                {
                    var menuItem = _menuItems.Last();
                    _menuItems.RemoveAt(_menuItems.Count - 1);
                    _menuItems.Insert(0, menuItem);

                    //Set to zero position
                    _menuItems.First().Animate().YBy(_deltaNormalizePositions).SetDuration(1);
                }
                else
                {
                    var menuItem = _menuItems.First();
                    _menuItems.RemoveAt(0);
                    _menuItems.Add(menuItem);

                    //Set to zero position
                    _menuItems.First().Animate().XBy(_deltaNormalizePositions).SetDuration(1);
                }

                //reset data from model for part visible and not clickable menus
                _menuItems[1].ResetDataFromModel();
                _menuItems[5].ResetDataFromModel();

                _isScrolling = false;
            }
            else
            {
                //reset data from model for part visible and not clickable menus
                _menuItems[1].ResetDataFromModel();

                //after end of open/hide animation setup internal values to default
                var action = new Action(() =>
                {
                    _showMenuItemsIteration = 0;
                    IsOpened = !IsOpened;
                });

                if(ShowHintAnimation)
                    StartHintAnimation(action);
                else
                    action.Invoke();
            }
        }

        #endregion

        #region ICircleMenuClicable implementation

        public void PerformClick(int id, bool isSubMenu, bool isOpened)
        {
            if(isSubMenu)
            {
                Clicked?.Invoke(this, id);
            }
            else
            {
                var menuItemModel = default(CircleMenuItemModel); //CircleMenuItems?.FirstOrDefault(item => item.Id == id);

                if(menuItemModel == null)
                    return;

                if(menuItemModel.Children.Count() > 0)
                {
                    var index = _menuItems.IndexOf(_menuItems.FirstOrDefault(item => item.CircleMenuModelId == menuItemModel.Id));

                    for(int i = 1; i < _menuItems.Count; i++)
                        if(i != index)
                            _menuItems[i].Enabled = !_menuItems[i].Enabled;

                    _mainMenu.Enabled = !_mainMenu.Enabled;

                    _isSubMenuOpened = !isOpened;

                    if(!isOpened)
                    {
                        var initBottomMargin = 0;
                        var initRightMargin = 0;

                        switch(index)
                        {
                            case 2:
                                break;
                            case 3:
                                initBottomMargin = 136;
                                initRightMargin = 90;
                                break;
                            case 4:
                                break;
                        }

                        for(int i = 0; i < menuItemModel.Children.Count; i++)
                        {
                            var subMenu = CreateSubMenu(initBottomMargin + SubMenuMargin + ((int)Diameter + SubMenuMargin) * i, initRightMargin);
                            subMenu.Tag = $"Child{i}";
                            subMenu.SetICircleMenuClicable(this);
                            menuItemModel.Children[i].ImageSource.SetColorFilter(Color.Black, PorterDuff.Mode.SrcIn);
                            subMenu.SetDataFromModel(menuItemModel.Children[i].ImageSource, menuItemModel.Children[i].Id, true);
                            _container.AddView(subMenu, 0);

                            var alfaAnimation = CreateAlphaAnimation(subMenu, 100, 100 * i);
                            alfaAnimation.Start();

                            if(i == menuItemModel.Children.Count() - 1)
                            {
                                alfaAnimation.AnimationEnd += (s, e) =>
                                {
                                    //TODO: After animation actions
                                };
                            }
                        }
                    }
                    else
                    {
                        for(int i = 0; i < menuItemModel.Children.Count; i++)
                        {
                            var subMenu = _container.FindViewWithTag($"Child{i}") as CircleMenuItem;
                            var alfaAnimation = CreateAlphaAnimation(subMenu, 100, 100 * i, false);
                            alfaAnimation.Start();

                            alfaAnimation.AnimationEnd += (s, e) =>
                            {
                                _container.RemoveViewAt(0);
                                //TODO: After animation actions
                            };
                        }
                    }
                }
                else
                {
                    Clicked?.Invoke(this, id);
                }
            }
        }

        #endregion
    }
}
