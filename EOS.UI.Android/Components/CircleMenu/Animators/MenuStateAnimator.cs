using System;
using System.Collections.Generic;
using Android.Graphics;

namespace EOS.UI.Android.Components
{
    internal abstract class MenuStateAnimator
    {
        protected const int ShowHideAnimateDuration = 50;

        protected List<CircleMenuItem> _menuItems;
        protected List<Indicator> _indicators;
        protected PointF[] _menuItemsPositions;
        protected PointF[] _indicatorsPositions;
        protected Action _afterShowAction;
        protected Action _afterHideAction;
        protected UpdateMenuItemsVisibilityRunnable _updateRunnable;
        protected OpenSpringAnimationEndListener _animationEndListener;

        public MenuStateAnimator(List<CircleMenuItem> menuItems, 
            List<Indicator> indicators, 
            PointF[] menuItemsPositions,
            PointF[] indicatorsPositions,
            Action afterShowAction,
            Action afterHideAction,
            UpdateMenuItemsVisibilityRunnable updateRunnable,
            OpenSpringAnimationEndListener animationEndListener)
        {
            _menuItems = menuItems;
            _indicators = indicators;
            _menuItemsPositions = menuItemsPositions;
            _indicatorsPositions = indicatorsPositions;
            _afterShowAction = afterShowAction;
            _afterHideAction = afterHideAction;
            _updateRunnable = updateRunnable;
            _animationEndListener = animationEndListener;
        }
    }
}
