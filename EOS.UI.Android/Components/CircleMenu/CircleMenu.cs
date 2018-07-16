using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Support.Animation;
using Android.Util;
using Android.Views;
using Android.Widget;
using EOS.UI.Android.Interfaces;
using Java.Lang;

namespace EOS.UI.Android.Components
{
    public class CircleMenu: FrameLayout, View.IOnTouchListener, IRunnable, DynamicAnimation.IOnAnimationEndListener, IIsOpened
    {
        #region fields

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

        private float _deltaNormalizePositions;

        private bool _forward;
        private bool _normalize;
        private bool _isScrolling;
        private int _showMenuItemsIteration;

        private MainMenuButton _mainMenu;
        private RelativeLayout _container;
        private float _bufferX;
        private float _bufferY;
        private bool _isMovedRight;
        private bool _isMovedLeft;

        private List<CircleMenuItem> _menuItems = new List<CircleMenuItem>();

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
            _deltaClosePositions[0] = new float[] { -Diameter * 0.25f * denisty, 0f };
            _deltaClosePositions[1] = new float[] { -Delta4 * denisty, Delta1 * denisty };
            _deltaClosePositions[2] = new float[] { -Delta3 * denisty, Delta2 * denisty };
            _deltaClosePositions[3] = new float[] { -Delta2 * denisty, Delta3 * denisty };
            _deltaClosePositions[4] = new float[] { -Delta1 * denisty, Delta4 * denisty };
            _deltaClosePositions[5] = new float[] { 0f, Diameter * 0.25f * denisty };

            _deltaOpenPositions[0] = new float[] { 0f, -Diameter * 0.25f * denisty };
            _deltaOpenPositions[5] = new float[] { Delta1 * denisty, -Delta4 * denisty };
            _deltaOpenPositions[4] = new float[] { Delta2 * denisty, -Delta3 * denisty };
            _deltaOpenPositions[3] = new float[] { Delta3 * denisty, -Delta2 * denisty };
            _deltaOpenPositions[2] = new float[] { Delta4 * denisty, -Delta1 * denisty };
            _deltaOpenPositions[1] = new float[] { Diameter * 0.25f * denisty, 0f };

            _deltaForwardPositions[0] = new float[] { 0f, -Diameter * 0.25f * denisty };
            _deltaForwardPositions[1] = new float[] { Delta1 * denisty, -Delta4 * denisty };
            _deltaForwardPositions[2] = new float[] { Delta2 * denisty, -Delta3 * denisty };
            _deltaForwardPositions[3] = new float[] { Delta3 * denisty, -Delta2 * denisty };
            _deltaForwardPositions[4] = new float[] { Delta4 * denisty, -Delta1 * denisty };
            _deltaForwardPositions[5] = new float[] { Diameter * 0.25f * denisty, 0f };

            _deltaBackPositions[0] = new float[] { -Diameter * 0.25f * denisty, 0f };
            _deltaBackPositions[5] = new float[] { -Delta4 * denisty, Delta1 * denisty };
            _deltaBackPositions[4] = new float[] { -Delta3 * denisty, Delta2 * denisty };
            _deltaBackPositions[3] = new float[] { -Delta2 * denisty, Delta3 * denisty };
            _deltaBackPositions[2] = new float[] { -Delta1 * denisty, Delta4 * denisty };
            _deltaBackPositions[1] = new float[] { 0f, Diameter * 0.25f * denisty };
        }

        private void MainMenuClick(object sender, EventArgs e)
        {
            if(_showMenuItemsIteration == 0)
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
                    _menuItems[0].Animate().WithEndAction(this).XBy(-_deltaNormalizePositions).SetDuration(1);
                else
                    _menuItems[0].Animate().WithEndAction(this).YBy(-_deltaNormalizePositions).SetDuration(1);
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

        #endregion

        #region IOnTouchListener implementation

        public bool OnTouch(View v, MotionEvent e)
        {
            if(!_isScrolling)
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
                    Toast.MakeText(Context, _isMovedRight ? "swipe right" : "swipe left", ToastLength.Short).Show();
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

                _isScrolling = false;
            }
            else
            {
                //after end of open/hide animation setup internal values to default
                _showMenuItemsIteration = 0;
                IsOpened = !IsOpened;
            }
        }

        #endregion 
    }
}
