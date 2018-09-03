using System;
using System.Collections.Generic;
using Android.Graphics;

namespace EOS.UI.Droid.Components
{
    internal abstract class MenuStateCommutator
    {
        //spring animation constants
        protected const float Stiffness = 1000f;
        protected const float DampingRatio = 0.37f;
        protected const int ShowHideAnimateDuration = 100;

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
