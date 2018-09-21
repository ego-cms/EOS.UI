using System;
using System.Collections.Generic;
using System.Linq;
using Android.Animation;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using EOS.UI.Droid.Interfaces;
using EOS.UI.Droid.Themes;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using static EOS.UI.Droid.Helpers.Enums;

namespace EOS.UI.Droid.Components
{
    public class CircleMenu: FrameLayout, View.IOnTouchListener, IIsOpened, IEOSThemeControl, ICircleMenuClickable
    {
        #region constants

        private const int HintElevationValue = 2;
        private const int HintAnimationSmoothDuration = 200;
        private const int HintAnimationFirstDuration = 300;
        private const int HintAnimationSecondDuration = 400;
        private const int HintAnimationDeltaY = 10;
        private const int SubMenuMargin = 11;

        //MenuItems margins from bottom and right side of screen
        private const float MenuDiameter = 52f;
        private const float MenuMargin1 = 94f;
        private const float MenuMargin3 = 25f;
        private const float MenuMargin5 = 111f;
        private const float MenuMargin6 = -22f;
        private const float MenuMargin7 = 76f;

        //Indicator margins from bottom and right side of screen
        private const float IndicatorDiameter = 6f;
        private const float IndicatorMargin1 = 155f;

        //SubMenu margins for 3 position
        private const int BottomMargin1 = 15;
        private const int BottomMargin2 = 136;
        private const int BottomMargin3 = 161;
        private const int LeftMargin1 = 174;
        private const int LeftMargin2 = 86;
        private const int LeftMargin3 = 26;

        //Tags for submenus and indicator
        private const string Child = "Child";
        private const string Indicator = "Indicator";

        private const int SubMenuAnimateDuration = 150;

        private const float ShadowRadiusValue = 6f;
        //intermediate alpha values when should show or hide shadow
        private const float AlphaHidingValue = 0.8f;
        private const float AlphaShowingValue = 0.5f;

        private const int MenuPositionsRadius = 96;
        private const int CirclePointsCount = 360;
        private const int InitialIndex = 105;
        private const int PointsOnIteration = 40;
        private const int MarginToCenter = 16;

        private const int MainSwipeDuration = 50;
        private const int MainSpringSwipeDuration = 250;

        #endregion

        #region fields 

        private MainMenuButton _mainMenu;
        private RelativeLayout _container;
        private List<CircleMenuItem> _menuItems = new List<CircleMenuItem>();
        private List<Indicator> _indicators = new List<Indicator>();
        private List<PointF> _menuIndicatorsPositions = new List<PointF>();
        private List<PointF> _menuPositions = new List<PointF>();

        private List<PointF>[] _menusListPositionsForward = new List<PointF>[6];
        private List<PointF>[] _menusListPositionsBack = new List<PointF>[6];

        private List<PointF>[] _indicatorsListPositionsForward = new List<PointF>[6];
        private List<PointF>[] _indicatorsListPositionsBack = new List<PointF>[6];

        private List<PointF>[] _menusSpringListPositionsForward = new List<PointF>[6];
        private List<PointF>[] _menusSpringListPositionsBack = new List<PointF>[6];

        private List<PointF>[] _indicatorsSpringListPositionsForward = new List<PointF>[6];
        private List<PointF>[] _indicatorsSpringListPositionsBack = new List<PointF>[6];


        private bool _forward;
        private bool _canSwipe = true;
        private float _deltaNormalizePositions;

        private CircleMenuScrollListener _scrollListener = new CircleMenuScrollListener();
        private NormalizationEndRunnable _normalizationEndListener;

        private IMenuStateCommutator _menuStateCommutator;

        #endregion

        #region properties

        private bool IsBusy { get; set; }
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

        private Color _unfocusedBackgroundColor;
        public Color UnfocusedBackgroundColor
        {
            get => _unfocusedBackgroundColor;
            set
            {
                _unfocusedBackgroundColor = value;
                foreach(var menu in _menuItems)
                    menu.UnfocusedBackgroundColor = value;
                _mainMenu.UnfocusedBackgroundColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _focusedBackrgoundColor;
        public Color FocusedBackgroundColor
        {
            get => _focusedBackrgoundColor;
            set
            {
                _focusedBackrgoundColor = value;
                foreach(var menu in _menuItems)
                    menu.FocusedBackgroundColor = value;
                foreach(var indicator in _indicators)
                    (indicator.Background as GradientDrawable).SetColor(value);
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _focusedIconColor;
        public Color FocusedIconColor
        {
            get => _focusedIconColor;
            set
            {
                _focusedIconColor = value;
                foreach(var menu in _menuItems)
                    menu.FocusedIconColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _unfocusedIconColor;
        public Color UnfocusedIconColor
        {
            get => _unfocusedIconColor;
            set
            {
                _unfocusedIconColor = value;
                foreach(var menu in _menuItems)
                    menu.UnfocusedIconColor = value;
                _mainMenu.UnfocusedIconColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _blackoutColor;
        private Color BlackoutColor
        {
            get => _blackoutColor;
            set
            {
                _blackoutColor = value;
                foreach(var menu in _menuItems)
                    menu.BlackoutColor = value;
                foreach(var indicator in _indicators)
                    indicator.BlackoutColor = value;
                _mainMenu.BlackoutColor = value;
            }
        }

        private List<CircleMenuItemModel> _circleMenuItems;
        public List<CircleMenuItemModel> CircleMenuItems
        {
            get => _circleMenuItems;
            set
            {
                if(value?.Count > 9 || value?.Count < 3)
                    throw new ArgumentOutOfRangeException("Items should be more then 3 and less then 9");

                var menuState = MenuState.Full;
                _canSwipe = true;

                //if source contains 3 item we should off swipe and set simple open/show algorithm
                if(value.Count == 3)
                {
                    menuState = MenuState.Simple;
                    _canSwipe = false;
                }

                _menuStateCommutator = GetMenuStateCommutator(menuState);

                _circleMenuItems = value;
                InitialDataModelSetup();
                IsEOSCustomizationIgnored = true;
            }
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

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            FillMenuItemsPositions();
        }

        private void FillMenuItemsPositions()
        {
            var mainMenuCenterX = Width - (MenuDiameter + MarginToCenter) * Resources.DisplayMetrics.Density;
            var mainMenuCenterY = Height - (MenuDiameter + MarginToCenter) * Resources.DisplayMetrics.Density;
            var indicatorCenterX = Width - (MenuDiameter / 2 + IndicatorDiameter / 2 + MarginToCenter) * Resources.DisplayMetrics.Density;
            var indicatorCenterY = Height - (MenuDiameter / 2 + IndicatorDiameter / 2 + MarginToCenter) * Resources.DisplayMetrics.Density;

            var mainMenuPositionsRadius = MenuPositionsRadius * Resources.DisplayMetrics.Density;
            var indicatorPositionsRadius = mainMenuPositionsRadius + 35 * Resources.DisplayMetrics.Density;

            for(int i = 0; i < CirclePointsCount; i++)
            {
                _menuPositions.Add(new PointF(
                    (float)(Math.Cos(2 * Math.PI * i / CirclePointsCount) * mainMenuPositionsRadius + mainMenuCenterX), 
                    (float)(Math.Sin(2 * Math.PI * i / CirclePointsCount) * mainMenuPositionsRadius + mainMenuCenterY)));

                _menuIndicatorsPositions.Add(new PointF(
                    (float)(Math.Cos(2 * Math.PI * i / CirclePointsCount) * indicatorPositionsRadius + indicatorCenterX), 
                    (float)(Math.Sin(2 * Math.PI * i / CirclePointsCount) * indicatorPositionsRadius + indicatorCenterY)));
            }

            GeneratePositions();
        }

        private void Initialize(IAttributeSet attrs = null)
        {
            var inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            var view = inflater.Inflate(Resource.Layout.CircleMenu, this);
            _container = view.FindViewById<RelativeLayout>(Resource.Id.container);

            _mainMenu = view.FindViewById<MainMenuButton>(Resource.Id.hamburgerMenu);
            _mainMenu.SetIIsOpenedItem(this);
            _mainMenu.SetICircleMenuClickable(this);
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

            _normalizationEndListener = new NormalizationEndRunnable(Context, this);

            var denisty = Context.Resources.DisplayMetrics.Density;
            _indicators.Add(CreateIndicatorView((int)(-IndicatorDiameter * denisty), (int)(-IndicatorDiameter * denisty)));
            _indicators.Add(CreateIndicatorView((int)(IndicatorMargin1 * denisty), (int)(-IndicatorDiameter * denisty)));
            _indicators.Add(CreateIndicatorView((int)(IndicatorMargin1 * denisty), (int)(-IndicatorDiameter * denisty)));
            _indicators.Add(CreateIndicatorView((int)(IndicatorMargin1 * denisty), (int)(-IndicatorDiameter * denisty)));
            _indicators.Add(CreateIndicatorView((int)(IndicatorMargin1 * denisty), (int)(-IndicatorDiameter * denisty)));
            _indicators.Add(CreateIndicatorView((int)(IndicatorMargin1 * denisty), (int)(-IndicatorDiameter * denisty)));

            foreach(var indicator in _indicators)
                _container.AddView(indicator);

            _deltaNormalizePositions = (MenuDiameter + MenuMargin1) * Context.Resources.DisplayMetrics.Density;

            UpdateAppearance();
        }

        private void MainMenuClick(object sender, EventArgs e)
        {
            if(!IsBusy && !Locked)
            {
                if(IsOpened)
                {
                    //on hiding animation should be visible icon on last item
                    var model = FindNextVisibleModel(true);
                    _menuItems[1].SetDataFromModel(model);
                    _indicators[1].Visibility = model.HasChildren ? ViewStates.Visible : ViewStates.Gone;
                }

                Locked = true;
                _mainMenu.AnimateClick();
                UpdateMenuItemsVisiblility();
            }
        }

        /// <summary>
        /// Method which spin round items by swipe action.
        /// Animation animated X and Y with spring interpolator (implemented with native SpringAnimation)
        /// Spin round can be forward (left to right) and back (right to left)
        /// </summary>
        private void MoveMenuItemsAnimation()
        {
            for(int i = 0; i < _menuItems.Count; i++)
            {
                var afterEndAnimation = i == _menuItems.Count - 1 ? new Action(() => HandleOnScrollSpringAnimationEnd()) : null;

                var menu = _menuItems[i];
                var indicator = _indicators[i];

                var positions = _forward ? _menusListPositionsForward[i] : _menusListPositionsBack[i];
                var springPositions = _forward ? _menusSpringListPositionsForward[i] : _menusSpringListPositionsBack[i];
                var indicatorPositions = _forward ? _indicatorsListPositionsForward[i] : _indicatorsListPositionsBack[i];
                var indicatorSpringPositions = _forward ? _indicatorsSpringListPositionsForward[i] : _indicatorsSpringListPositionsBack[i];

                StartAnimations(menu, indicator, positions, indicatorPositions, springPositions, indicatorSpringPositions, afterEndAnimation);
            }
        }

        /// <summary>
        /// Before scroll right/left we should set invisible item to start position
        /// Start position depends on scroll direction
        /// After normalize starts scroll animation
        /// </summary>
        private void NormalizeHiddenMenuItem(CircleMenuItemModel model)
        {
            var denisty = Context.Resources.DisplayMetrics.Density;
            if(_forward)
            {
                _menuItems[1].SetDataFromModel(model);
                _menuItems[0].Animate().X(Width - MenuMargin7 * denisty).Y(Height - MenuMargin6 * denisty).SetDuration(1).WithEndAction(_normalizationEndListener);
            }
            else
            {
                _menuItems[5].SetDataFromModel(model);
                _menuItems[0].Animate().X(Width - MenuMargin6 * denisty).Y(Height - MenuMargin7 * denisty).SetDuration(1).WithEndAction(_normalizationEndListener);
            }
        }

        private void ToggleIndicatorVisibility(CircleMenuItemModel model)
        {
            _indicators[_forward ? 1 : 5].Visibility = model.HasChildren ? ViewStates.Visible : ViewStates.Invisible;
        }

        private void PreScrollingSetup()
        {
            var model = FindNextVisibleModel(_forward);
            ToggleIndicatorVisibility(model);
            NormalizeHiddenMenuItem(model);
        }

        /// <summary>
        /// Method find next visible model after scrolling
        /// </summary>
        /// <param name="scrollForward">Sets direction of scrolling</param>
        /// <returns>Returns finded model on CircleMenuItems list</returns>
        private CircleMenuItemModel FindNextVisibleModel(bool scrollForward)
        {
            var index = 0;
            if(scrollForward)
            {
                index = CircleMenuItems.IndexOf(CircleMenuItems.FirstOrDefault(item => item.Id == _menuItems[2].CircleMenuModelId)) - 1;
                if(index < 0)
                    index = CircleMenuItems.Count - 1;
            }
            else
            {
                index = CircleMenuItems.IndexOf(CircleMenuItems.FirstOrDefault(item => item.Id == _menuItems[4].CircleMenuModelId)) + 1;
                if(index > CircleMenuItems.Count - 1)
                    index =  0;
            }
            return CircleMenuItems[index];
        }

        /// <summary>
        /// Method which spin round items by swipe action.
        /// Animation has 5 iterations. 
        /// For hiding all iterations animate X and Y without interpolator. 
        /// For showing first 4 iterations animate X and Y without interpolator, 
        /// and last has spring interpolator (implemented with native SpringAnimation)
        /// </summary>
        private void UpdateMenuItemsVisiblility()
        {
            Locked = true;

            if(IsOpened)
            {
                _menuStateCommutator.HideMenuItems();
            }
            else
            {
                _container.SetBackgroundColor(BlackoutColor);
                _menuStateCommutator.ShowMenuItems();
            }
        }

        /// <summary>
        /// Generation of positions for both directions are by start and end position on layout
        /// Full lenth of points in base list are 360 points (each point according 1 degree) 
        /// and length from each to another position are 40 degrees and its according 40 points
        /// For move any menu from one to next position we should set to animation 40 poits
        /// </summary>
        private void GeneratePositions()
        {
            //for forward direction we should just find first and last point for moving menu
            GeneratePointsFoward();
            //for back direction we should find first and last point for moving menuand revert positions back
            //beecause in base list positions located in forward direction
            GeneratePointsBack();
        }

        private void GeneratePointsFoward()
        {
            for(int i = 0; i < _menuItems.Count; i++)
            {
                _menusListPositionsForward[i] = _menuPositions.GetRange(GetStartInpexOfPositions(i, true), PointsOnIteration);
                _indicatorsListPositionsForward[i] = _menuIndicatorsPositions.GetRange(GetStartInpexOfPositions(i, true), PointsOnIteration);

                _menusSpringListPositionsForward[i] = new List<PointF>();
                _indicatorsSpringListPositionsForward[i] = new List<PointF>();

                GenerateSpringAnimationPositionsForward(i);
            }
        }

        private void GenerateSpringAnimationPositionsForward(int index)
        {
            var baseIndex = GetBaseIndexForSpringAnimation(index, true);

            _menusSpringListPositionsForward[index].Add(_menuPositions[baseIndex + 4]);
            //_menusSpringListPositionsForward[index].Add(_menuPositions[baseIndex - 2]);
            _menusSpringListPositionsForward[index].Add(_menuPositions[baseIndex - 2]);
            _menusSpringListPositionsForward[index].Add(_menuPositions[baseIndex]);

            _indicatorsSpringListPositionsForward[index].Add(_menuIndicatorsPositions[baseIndex + 4]);
            //_indicatorsSpringListPositionsForward[index].Add(_menuIndicatorsPositions[baseIndex - 2]);
            _indicatorsSpringListPositionsForward[index].Add(_menuIndicatorsPositions[baseIndex - 2]);
            _indicatorsSpringListPositionsForward[index].Add(_menuIndicatorsPositions[baseIndex]);
        }

        private void GeneratePointsBack()
        {
            for(int i = 0; i < _menuItems.Count; i++)
            {
                _menusListPositionsBack[i] = new List<PointF>();
                _indicatorsListPositionsBack[i] = new List<PointF>();

                _menusListPositionsBack[i] = _menuPositions.GetRange(GetStartInpexOfPositions(i, false), PointsOnIteration);
                _indicatorsListPositionsBack[i] = _menuIndicatorsPositions.GetRange(GetStartInpexOfPositions(i, false), PointsOnIteration);

                var tempList = new List<PointF>();
                for(int z = _menusListPositionsBack[i].Count - 1; z >= 0; z--)
                    tempList.Add(_menusListPositionsBack[i][z]);
                _menusListPositionsBack[i] = tempList;

                var tempIndicatorsList = new List<PointF>();
                for(int z = _indicatorsListPositionsBack[i].Count - 1; z >= 0; z--)
                    tempIndicatorsList.Add(_indicatorsListPositionsBack[i][z]);
                _indicatorsListPositionsBack[i] = tempIndicatorsList;

                _menusSpringListPositionsBack[i] = new List<PointF>();
                _indicatorsSpringListPositionsBack[i] = new List<PointF>();

                GenerateSpringAnimationPositionsBack(i);
            }
        }

        private void GenerateSpringAnimationPositionsBack(int index)
        {
            var baseIndex = GetBaseIndexForSpringAnimation(index, false);

            _menusSpringListPositionsBack[index].Add(_menuPositions[baseIndex - 4]);
            //_menusSpringListPositionsBack[index].Add(_menuPositions[baseIndex + 2]);
            _menusSpringListPositionsBack[index].Add(_menuPositions[baseIndex + 2]);
            _menusSpringListPositionsBack[index].Add(_menuPositions[baseIndex]);

            _indicatorsSpringListPositionsBack[index].Add(_menuIndicatorsPositions[baseIndex - 4]);
            //_indicatorsSpringListPositionsBack[index].Add(_menuIndicatorsPositions[baseIndex + 2]);
            _indicatorsSpringListPositionsBack[index].Add(_menuIndicatorsPositions[baseIndex + 2]);
            _indicatorsSpringListPositionsBack[index].Add(_menuIndicatorsPositions[baseIndex]);
        }

        private int GetStartInpexOfPositions(int index, bool forward)
        {
            if(forward)
                return InitialIndex + index * PointsOnIteration;
            else
                //first position for back animation in the end of list
                return index == 0 ? InitialIndex + 5 * PointsOnIteration : InitialIndex + (index - 1) * PointsOnIteration;
        }

        private int GetBaseIndexForSpringAnimation(int index, bool forward)
        {
            if(forward)
                return InitialIndex + (index + 1) * PointsOnIteration;
            else
                //first position for back animation in the end of list
                return index == 0 ? InitialIndex + 5 * PointsOnIteration : InitialIndex + (index - 1) * PointsOnIteration;
        }

        private void StartAnimations(CircleMenuItem menu, Indicator indicator, List<PointF> positions, List<PointF> indicatorPositions,
            List<PointF> springPositions, List<PointF> indicatorSpringPositions, Action afterEndAnimation)
        {
            //start swipe main menu animation with spring
            StartAnimation(menu,
                positions,
                MainSwipeDuration,
                null,
                new Action(() => StartAnimation(menu,
                    springPositions,
                    MainSpringSwipeDuration,
                    null,
                    afterEndAnimation)));

            //start swipe indicators animation with spring
            StartAnimation(indicator,
                indicatorPositions,
                MainSwipeDuration,
                null,
                new Action(() => StartAnimation(indicator,
                    indicatorSpringPositions,
                    MainSpringSwipeDuration,
                    null,
                    null)));
        }

        private void StartAnimation(View view, List<PointF> positions, int duration, BaseInterpolator interpolator = null, Action action = null)
        {
            var xPositions = positions.Select(item => item.X).ToArray();
            var yPositions = positions.Select(item => item.Y).ToArray();

            var xProp = PropertyValuesHolder.OfFloat("X", xPositions);
            var yProp = PropertyValuesHolder.OfFloat("Y", yPositions);
            var moveAnimation = ObjectAnimator.OfPropertyValuesHolder(view, xProp, yProp);
            moveAnimation.SetDuration(duration);

            if(interpolator != null)
                moveAnimation.SetInterpolator(interpolator);

            if(action != null)
                moveAnimation.AnimationEnd += (s, e) => action.Invoke();

            moveAnimation.Start();
        }

        /// <summary>
        /// Method which responsible for open/show algorithm implementation
        /// </summary>
        /// <param name="menuState">identificator of algorithm</param>
        /// <returns>open/show algorithm implementation</returns>
        private IMenuStateCommutator GetMenuStateCommutator(MenuState menuState)
        {
            var afterHideAnimation = new Action(() =>
            {
                _container.SetBackgroundColor(Color.Transparent);
                _indicators.ForEach(indicator => indicator.Visibility = ViewStates.Gone);
                InitialDataModelSetup();
                IsOpened = !IsOpened;
                Locked = false;
            });

            var afterShowAnimation = new Action(() =>
            {
                //reset data from model for part visible and not clickable menus
                _menuItems[1].ResetDataFromModel();

                //after end of open/hide animation setup internal values to default
                var action = new Action(() =>
                {
                    IsOpened = !IsOpened;
                    Locked = false;
                });

                if(ShowHintAnimation)
                    StartHintAnimation(action);
                else
                    action.Invoke();
            });

            switch(menuState)
            {
                case MenuState.Full:
                    return new MenuStateCommutatorFull(_menuItems, 
                        _indicators, 
                        afterShowAnimation, 
                        afterHideAnimation,
                        _menusListPositionsForward,
                        _menusListPositionsBack,
                        _indicatorsListPositionsForward,
                        _indicatorsListPositionsBack,
                        _menusSpringListPositionsForward,
                        _menusSpringListPositionsBack,
                        _indicatorsSpringListPositionsForward,
                        _indicatorsSpringListPositionsBack);
                case MenuState.Simple:
                    return new MenuStateCommutatorSimple(_menuItems,
                        _indicators,
                        afterShowAnimation,
                        afterHideAnimation,
                        _menusListPositionsForward,
                        _menusListPositionsBack,
                        _indicatorsListPositionsForward,
                        _indicatorsListPositionsBack,
                        _menusSpringListPositionsForward,
                        _menusSpringListPositionsBack,
                        _indicatorsSpringListPositionsForward,
                        _indicatorsSpringListPositionsBack);
                default:
                    return new MenuStateCommutatorFull(_menuItems,
                        _indicators,
                        afterShowAnimation,
                        afterHideAnimation,
                        _menusListPositionsForward,
                        _menusListPositionsBack,
                        _indicatorsListPositionsForward,
                        _indicatorsListPositionsBack,
                        _menusSpringListPositionsForward,
                        _menusSpringListPositionsBack,
                        _indicatorsSpringListPositionsForward,
                        _indicatorsSpringListPositionsBack);
            }
        }

        /// <summary>
        /// Hint animation shows one time on start of application
        /// </summary>
        /// <param name="action">After animation complete action</param>
        private void StartHintAnimation(Action action)
        {
            ShowHintAnimation = false;

            if(!_menuItems[4].HasSubMenus)
            {
                action?.Invoke();
                return;
            }

            Locked = true;

            var lastView = CreateHintView();
            var middleView = CreateHintView();

            var delta = HintAnimationDeltaY * Context.Resources.DisplayMetrics.Density;

            var lastItemTranslateDown = CreateHintAnimation(delta, HintAnimationFirstDuration);
            lastItemTranslateDown.AnimationEnd += delegate
            {
                var lastItemTranslateUp = CreateHintAnimation(delta, HintAnimationSecondDuration, false);
                lastItemTranslateUp.AnimationEnd += delegate
                {
                    _container.RemoveViewAt(0);
                    _container.RemoveViewAt(0);

                    lastView?.Dispose();
                    middleView?.Dispose();

                    action?.Invoke();
                };
                lastView.StartAnimation(lastItemTranslateUp);
            };
            var firstItemTranslateUp = CreateHintAnimation(-delta, HintAnimationFirstDuration);
            firstItemTranslateUp.AnimationEnd += delegate
            {
                var firstItemTranslateDown = CreateHintAnimation(-delta, HintAnimationSecondDuration, false);
                _menuItems[4].StartAnimation(firstItemTranslateDown);
                if(_indicators[4].Visibility == ViewStates.Visible)
                {
                    firstItemTranslateDown.AnimationEnd += delegate
                    {
                        //after translate animation view can't change visibilitythat's why we should clear animation after complete
                        //thats why we shold clear animation after complete
                        _indicators[4].ClearAnimation();
                    };
                    _indicators[4].StartAnimation(firstItemTranslateDown);
                }
            };

            lastView.Elevation = HintElevationValue * Resources.DisplayMetrics.Density;
            middleView.Elevation = HintElevationValue * 2 * Resources.DisplayMetrics.Density;

            _container.AddView(middleView, 0);
            _container.AddView(lastView, 0);

            lastView.StartAnimation(lastItemTranslateDown);
            _menuItems[4].StartAnimation(firstItemTranslateUp);
            if(_indicators[4].Visibility == ViewStates.Visible)
                _indicators[4].StartAnimation(firstItemTranslateUp);
        }

        private void InitialDataModelSetup()
        {
            //if _canSwipe flag is true you can see 4 item on open/hide animation
            //else you can see 3
            if(_canSwipe)
            {
                for(int i = !IsOpened ? 0 : 1; i < 4; i++)
                {
                    var model = _circleMenuItems[i];
                    _menuItems[i + 1].SetDataFromModel(model);
                    _indicators[i + 1].Visibility = model.HasChildren ? ViewStates.Visible : ViewStates.Gone;
                }
            }
            else
            {
                for(int i = 0; i < 3; i++)
                {
                    var model = _circleMenuItems[i];
                    _menuItems[i + 2].SetDataFromModel(model);
                    _indicators[i + 2].Visibility = model.HasChildren ? ViewStates.Visible : ViewStates.Gone;
                }
            }
        }

        /// <summary>
        /// method of implementation of hint animation
        /// </summary>
        /// <param name="delta">animated delta Y position</param>
        /// <param name="duration">animation time</param>
        /// <param name="isIn">flag for detecting animation direction out or in  </param>
        /// <returns></returns>
        private TranslateAnimation CreateHintAnimation(float delta, long duration, bool isIn = true)
        {
            var translateAnimation = isIn ? new TranslateAnimation(0, 0, 0, delta) : new TranslateAnimation(0, 0, delta, 0);
            translateAnimation.StartOffset = isIn? 0 : HintAnimationSmoothDuration;
            translateAnimation.Interpolator = new DecelerateInterpolator();
            translateAnimation.FillAfter = true;
            translateAnimation.Duration = duration;
            return translateAnimation;
        }

        private ObjectAnimator CreateAlphaAnimation(View view, int duration, int startDelay, bool isShow = true)
        {
            var alfaAnimation = isShow? ObjectAnimator.OfFloat(view, "Alpha", 1f) : ObjectAnimator.OfFloat(view, "Alpha", 0f);
            alfaAnimation.SetDuration(duration);
            alfaAnimation.StartDelay = startDelay;
            return alfaAnimation;
        }

        private View CreateHintView()
        {
            var hintView = new View(Context);
            var layoutParameters = new RelativeLayout.LayoutParams(
                (int)(MenuDiameter * Context.Resources.DisplayMetrics.Density),
                (int)(MenuDiameter * Context.Resources.DisplayMetrics.Density));

            layoutParameters.RightMargin = (int)(MenuMargin3 * Context.Resources.DisplayMetrics.Density);
            layoutParameters.BottomMargin = (int)(MenuMargin5 * Context.Resources.DisplayMetrics.Density);

            layoutParameters.AddRule(LayoutRules.AlignParentBottom);
            layoutParameters.AddRule(LayoutRules.AlignParentRight);

            hintView.LayoutParameters = layoutParameters;

            var roundedDrawable = new GradientDrawable();
            roundedDrawable.SetColor(UnfocusedBackgroundColor);
            roundedDrawable.SetShape(ShapeType.Oval);

            hintView.SetBackgroundDrawable(roundedDrawable);

            return hintView;
        }

        private CircleMenuItem CreateSubMenu(int buttonMargin, int rightMargin)
        {
            var subMenu = new CircleMenuItem(Context);
            var layoutParameters = new RelativeLayout.LayoutParams(
                (int)(MenuDiameter * Context.Resources.DisplayMetrics.Density),
                (int)(MenuDiameter * Context.Resources.DisplayMetrics.Density));

            layoutParameters.RightMargin = (int)(rightMargin * Context.Resources.DisplayMetrics.Density);
            layoutParameters.BottomMargin = (int)(buttonMargin * Context.Resources.DisplayMetrics.Density);

            layoutParameters.AddRule(LayoutRules.AlignParentBottom);
            layoutParameters.AddRule(LayoutRules.AlignParentRight);

            subMenu.LayoutParameters = layoutParameters;

            var roundedDrawable = new GradientDrawable();
            roundedDrawable.SetColor(UnfocusedBackgroundColor);
            roundedDrawable.SetShape(ShapeType.Oval);

            subMenu.SetBackgroundDrawable(roundedDrawable);

            subMenu.Alpha = 0f;
            subMenu.Elevation = 0f;

            subMenu.UnfocusedBackgroundColor = UnfocusedBackgroundColor;
            subMenu.FocusedBackgroundColor = FocusedBackgroundColor;
            subMenu.FocusedIconColor = FocusedIconColor;
            subMenu.UnfocusedIconColor = UnfocusedIconColor;

            return subMenu;
        }

        private Indicator CreateIndicatorView(int buttonMargin, int rightMargin)
        {
            var indicatorView = new Indicator(Context);
            var layoutParameters = new RelativeLayout.LayoutParams(
                (int)(IndicatorDiameter * Context.Resources.DisplayMetrics.Density),
                (int)(IndicatorDiameter * Context.Resources.DisplayMetrics.Density));

            layoutParameters.RightMargin = (int)(rightMargin * Context.Resources.DisplayMetrics.Density);
            layoutParameters.BottomMargin = (int)(buttonMargin * Context.Resources.DisplayMetrics.Density);

            layoutParameters.AddRule(LayoutRules.AlignParentBottom);
            layoutParameters.AddRule(LayoutRules.AlignParentRight);

            indicatorView.LayoutParameters = layoutParameters;

            var roundedDrawable = new GradientDrawable();
            roundedDrawable.SetColor(FocusedBackgroundColor);
            roundedDrawable.SetShape(ShapeType.Oval);

            indicatorView.SetBackgroundDrawable(roundedDrawable);

            return indicatorView;
        }

        private void UpdateEnabledState(int index)
        {
            for(int i = 1; i < _menuItems.Count; i++)
            {
                if(i != index)
                {
                    _menuItems[i].Enabled = !_menuItems[i].Enabled;
                    if(_indicators[i].Visibility == ViewStates.Visible)
                        _indicators[i].Enabled = !_indicators[i].Enabled;
                }
            }
            _mainMenu.Enabled = !_mainMenu.Enabled;
        }

        private void GetSubMenuMarginsByPosition(int position, out int bottom, out int right)
        {
            switch(position)
            {
                case 2:
                    bottom = BottomMargin1;
                    right = LeftMargin1;
                    break;
                case 3:
                    bottom = BottomMargin2;
                    right = LeftMargin2;
                    break;
                case 4:
                    bottom = BottomMargin3;
                    right = LeftMargin3;
                    break;
                default:
                    bottom = 0;
                    right = 0;
                    break;
            }
        }

        private void ShowSubMenus(int index, CircleMenuItemModel menuItemModel)
        {
            _indicators[index].Visibility = ViewStates.Invisible;

            GetSubMenuMarginsByPosition(index, out int bottom, out int right);

            for(int i = 0; i < menuItemModel.Children.Count; i++)
            {
                var subMenu = CreateSubMenu(bottom + SubMenuMargin + ((int)MenuDiameter + SubMenuMargin) * i, right);
                subMenu.Tag = $"{Child}{i}";
                subMenu.SetICircleMenuClicable(this);
                subMenu.SetDataFromModel(menuItemModel.Children[i], true);
                _container.AddView(subMenu, 0);

                var alfaAnimation = CreateAlphaAnimation(subMenu, SubMenuAnimateDuration, SubMenuAnimateDuration * i);

                alfaAnimation.Update += delegate
                {
                    //when alpha is intermediate value it's necessary to add shadow
                    if(subMenu.Alpha >= AlphaShowingValue)
                        subMenu.Elevation = ShadowRadiusValue * Resources.DisplayMetrics.Density;
                };

                if(i == menuItemModel.Children.Count() - 1)
                {
                    //after showing submenus shows indicator
                    alfaAnimation.AnimationEnd += (s, e) =>
                    {
                        var indicator = CreateIndicatorView(
                            bottom + SubMenuMargin + ((int)MenuDiameter + SubMenuMargin) * i - (int)(IndicatorDiameter / 2),
                            right + (int)(MenuDiameter / 2) - 2);

                        indicator.Tag = Indicator;
                        _container.AddView(indicator);
                        var alfaIndicatorAnimation = CreateAlphaAnimation(indicator, SubMenuAnimateDuration, 0);
                        alfaIndicatorAnimation.Start();
                        alfaIndicatorAnimation.AnimationEnd += delegate 
                        {
                            Locked = false;
                        };
                    };
                }

                alfaAnimation.Start();
            }
        }

        private void HideSubMenus(int index, CircleMenuItemModel menuItemModel)
        {
            //first hides indicator
            var indicator = _container.FindViewWithTag(Indicator) as Indicator;
            var alfaIndicatorAnimation = CreateAlphaAnimation(indicator, SubMenuAnimateDuration, 0, false);
            alfaIndicatorAnimation.Start();
            alfaIndicatorAnimation.AnimationEnd += delegate
            {
                _container.RemoveView(indicator);
            };

            for(int i = menuItemModel.Children.Count - 1; i >= 0; i--)
            {
                var subMenu = _container.FindViewWithTag($"{Child}{i}") as CircleMenuItem;
                var alfaAnimation = CreateAlphaAnimation(subMenu, SubMenuAnimateDuration, SubMenuAnimateDuration * (menuItemModel.Children.Count - i), false);
                alfaAnimation.Start();

                alfaAnimation.Update += delegate
                {
                    //when alpha is intermediate value it's necessary to remove shadow
                    if(subMenu.Alpha < AlphaHidingValue)
                        subMenu.Elevation = 0;
                };

                alfaAnimation.AnimationEnd += delegate
                {
                    if(subMenu.Tag.ToString() == $"{Child}{0}")
                    {
                        _indicators[index].Visibility = ViewStates.Visible;
                        IsBusy = false;
                        Locked = false;
                    }
                    _container.RemoveView(subMenu);
                };
            }
        }

        internal void HandleOnScrollSpringAnimationEnd()
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

                var indicator = _indicators.Last();
                _indicators.RemoveAt(_indicators.Count - 1);
                _indicators.Insert(0, indicator);
            }
            else
            {
                var menuItem = _menuItems.First();
                _menuItems.RemoveAt(0);
                _menuItems.Add(menuItem);

                var indicator = _indicators.First();
                _indicators.RemoveAt(0);
                _indicators.Add(indicator);
            }

            //Set to zero position
            _menuItems.First().Animate().X(Width).Y(Height).SetDuration(1);
            _indicators.First().Animate().X(Width).Y(Height).SetDuration(1);

            //reset data from model for part visible and not clickable menus
            _menuItems[1].ResetDataFromModel();
            _menuItems[5].ResetDataFromModel();

            Locked = false;
        }

        internal void HandleNormalizationEnd()
        {
            MoveMenuItemsAnimation();
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
                UnfocusedBackgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor6S);
                FocusedBackgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor);
                FocusedIconColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor6S);
                UnfocusedIconColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor1S);
                BlackoutColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.Blackout);
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

        #region IOnTouchListener implementation

        public bool OnTouch(View v, MotionEvent e)
        {
            if(!IsBusy && !Locked && _canSwipe)
            {
                if(_scrollListener.IsScrolled(ref _forward, e))
                {
                    Locked = true;
                    PreScrollingSetup();
                }
            }
            return IsOpened || Locked || IsBusy;
        }

        #endregion

        #region ICircleMenuClicable implementation

        public bool Locked { get; private set; }

        public void PerformClick(int id, bool isSubMenu, bool isOpened)
        {
            if(isSubMenu)
            {
                Clicked?.Invoke(this, id);
            }
            else
            {
                if(!Locked)
                {
                    Locked = true;

                    var menuItemModel = CircleMenuItems?.FirstOrDefault(item => item.Id == id);

                    if(menuItemModel == null)
                        return;

                    if(menuItemModel.HasChildren)
                    {
                        if(!isOpened)
                            IsBusy = true;

                        var index = _menuItems.IndexOf(_menuItems.FirstOrDefault(item => item.CircleMenuModelId == menuItemModel.Id));

                        UpdateEnabledState(index);

                        if(!isOpened)
                            ShowSubMenus(index, menuItemModel);
                        else
                            HideSubMenus(index, menuItemModel);
                    }
                    else
                    {
                        Clicked?.Invoke(this, id);
                        Locked = false;
                    }
                }
            }
        }

        public void PerformSwipe(bool isForward)
        {
            if(!IsBusy && !Locked &&_canSwipe)
            {
                _forward = isForward;
                Locked = true;
                PreScrollingSetup();
            }
        }

        #endregion
    }
}
