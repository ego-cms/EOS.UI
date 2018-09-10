using System;
using System.Collections.Generic;
using System.Linq;
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
        protected List<PointF>[] _menusListPositionsForward = new List<PointF>[6];
        protected List<PointF>[] _menusListPositionsBack = new List<PointF>[6];
        protected List<PointF>[] _indicatorsListPositionsForward = new List<PointF>[6];
        protected List<PointF>[] _indicatorsListPositionsBack = new List<PointF>[6];
        protected List<PointF>[] _menusSpringListPositionsForward = new List<PointF>[6];
        protected List<PointF>[] _menusSpringListPositionsBack = new List<PointF>[6];
        protected List<PointF>[] _indicatorsSpringListPositionsForward = new List<PointF>[6];
        protected List<PointF>[] _indicatorsSpringListPositionsBack = new List<PointF>[6];

        public MenuStateCommutator(List<CircleMenuItem> menuItems, 
            List<Indicator> indicators, 
            Action afterShowAction,
            Action afterHideAction,
            List<PointF>[] menusListPositionsForward,
            List<PointF>[] menusListPositionsBack,
            List<PointF>[] indicatorsListPositionsForward,
            List<PointF>[] indicatorsListPositionsBack,
            List<PointF>[] menusSpringListPositionsForward,
            List<PointF>[] menusSpringListPositionsBack,
            List<PointF>[] indicatorsSpringListPositionsForward,
            List<PointF>[] indicatorsSpringListPositionsBack)
        {
            _commutatorMenuItems = menuItems;
            _commutatorIndicators = indicators;
            _afterShowAction = afterShowAction;
            _afterHideAction = afterHideAction;
            _menusListPositionsForward = menusListPositionsForward;
            _menusListPositionsBack = menusListPositionsBack;
            _indicatorsListPositionsForward = indicatorsListPositionsForward;
            _indicatorsListPositionsBack = indicatorsListPositionsBack;
            _menusSpringListPositionsForward = menusSpringListPositionsForward;
            _menusSpringListPositionsBack = menusSpringListPositionsBack;
            _indicatorsSpringListPositionsForward = indicatorsSpringListPositionsForward;
            _indicatorsSpringListPositionsBack = indicatorsSpringListPositionsBack;
        }

        protected void StartOpenAnimation(CircleMenuItem menu, Indicator indicator, List<PointF> positions, List<PointF> indicatorPositions, 
            List<PointF> springPositions, List<PointF> indicatorSpringPositions, int delay, Action afterEndAnimation)
        {
            //start open main menu animation with spring
            StartAnimation(menu,
                positions,
                AnimateDuration - delay,
                delay,
                null,
                new Action(() => StartAnimation(menu,
                    springPositions,
                    SpringAnimateDuration,
                    0,
                    null,
                    afterEndAnimation)));

            //start open indicators animation with spring
            StartAnimation(indicator,
                indicatorPositions,
                AnimateDuration - delay,
                delay,
                null,
                new Action(() => StartAnimation(indicator,
                    indicatorSpringPositions,
                    SpringAnimateDuration,
                    0)));
        }

        protected void StartCloseAnimation(CircleMenuItem menu, Indicator indicator, List<PointF> positions, 
            List<PointF> indicatorPositions,int delay, Action afterEndAnimation)
        {
            //start hide main menu animation without spring
            StartAnimation(menu, positions, AnimateDuration - delay, 0, null, new Action(() => afterEndAnimation?.Invoke()));
            //start hide indicators animation without spring
            StartAnimation(indicator, indicatorPositions, AnimateDuration - delay, 0);
        }

        protected void StartAnimation(View view, List<PointF> positions, int duration, int startDelay, BaseInterpolator interpolator = null, Action action = null)
        {
            var xPositions = positions.Select(item => item.X).ToArray();
            var yPositions = positions.Select(item => item.Y).ToArray();
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
