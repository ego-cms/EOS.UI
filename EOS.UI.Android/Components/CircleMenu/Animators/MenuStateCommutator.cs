using System;
using System.Collections.Generic;
using Android.Graphics;

namespace EOS.UI.Android.Components
{
    internal abstract class MenuStateCommutator
    {
        protected const int ShowHideAnimateDuration = 50;

        protected List<CircleMenuItem> _commutatorMenuItems;
        protected List<Indicator> _commutatorIndicators;
        protected PointF[] _commutatorMenuItemsPositions;
        protected PointF[] _commutatorIndicatorsPositions;
        protected Action _afterShowAction;
        protected Action _afterHideAction;
        protected UpdateMenuItemsVisibilityRunnable _commutatorUpdateRunnable;
        protected OpenSpringAnimationEndListener _commutatorAnimationEndListener;

        public MenuStateCommutator(List<CircleMenuItem> menuItems, 
            List<Indicator> indicators, 
            PointF[] menuItemsPositions,
            PointF[] indicatorsPositions,
            Action afterShowAction,
            Action afterHideAction,
            UpdateMenuItemsVisibilityRunnable updateRunnable,
            OpenSpringAnimationEndListener animationEndListener)
        {
            _commutatorMenuItems = menuItems;
            _commutatorIndicators = indicators;
            _commutatorMenuItemsPositions = menuItemsPositions;
            _commutatorIndicatorsPositions = indicatorsPositions;
            _afterShowAction = afterShowAction;
            _afterHideAction = afterHideAction;
            _commutatorUpdateRunnable = updateRunnable;
            _commutatorAnimationEndListener = animationEndListener;
        }
    }
}
