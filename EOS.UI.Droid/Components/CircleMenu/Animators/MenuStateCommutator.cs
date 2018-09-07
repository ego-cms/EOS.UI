using System;
using System.Collections.Generic;
using Android.Animation;
using Android.Graphics;
using Android.Views;
using Android.Views.Animations;

namespace EOS.UI.Droid.Components
{
    internal abstract class MenuStateCommutator
    {
        protected const int AnimateDuration = 600;
        protected const int SpringAnimateDuration = 600;

        protected List<CircleMenuItem> _commutatorMenuItems;
        protected List<Indicator> _commutatorIndicators;
        protected Action _afterShowAction;
        protected Action _afterHideAction;
        protected List<PointF>[][] _indicatorsListPositions = new List<PointF>[2][];
        protected List<PointF>[][] _menusListPositions = new List<PointF>[2][];
        protected List<PointF>[][] _indicatorsSpringListPositions = new List<PointF>[2][];
        protected List<PointF>[][] _menusSpringListPositions = new List<PointF>[2][];

        public MenuStateCommutator(List<CircleMenuItem> menuItems, 
            List<Indicator> indicators, 
            Action afterShowAction,
            Action afterHideAction,
            List<PointF>[][] indicatorsListPositions,
            List<PointF>[][] menusListPositions,
            List<PointF>[][] indicatorsSpringListPositions,
            List<PointF>[][] menusSpringListPositions)
        {
            _commutatorMenuItems = menuItems;
            _commutatorIndicators = indicators;
            _afterShowAction = afterShowAction;
            _afterHideAction = afterHideAction;
            _indicatorsListPositions = indicatorsListPositions;
            _menusListPositions = menusListPositions;
            _indicatorsSpringListPositions = indicatorsSpringListPositions;
            _menusSpringListPositions = menusSpringListPositions;
        }

        protected void StartAnimation(View view, float[] xPositions, float[] yPositions, int duration, int startDelay, BaseInterpolator interpolator = null, Action action = null)
        {
            var xProp = PropertyValuesHolder.OfFloat("X", xPositions);
            var yProp = PropertyValuesHolder.OfFloat("Y", yPositions);
            var moveAnimation = ObjectAnimator.OfPropertyValuesHolder(view, xProp, yProp);
            moveAnimation.SetDuration(duration);
            moveAnimation.StartDelay = startDelay;
            moveAnimation.SetInterpolator(new LinearInterpolator());

            if(interpolator != null)
                moveAnimation.SetInterpolator(interpolator);

            if(action != null)
                moveAnimation.AnimationEnd += (s, e) => action.Invoke();

            moveAnimation.Start();
        }
    }
}
