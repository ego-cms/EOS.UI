using System;
using System.Collections.Generic;
using System.Linq;
using Android.Graphics;
using EOS.UI.Droid.Interfaces;

namespace EOS.UI.Droid.Components
{
    internal class MenuStateCommutatorFull : MenuStateCommutator, IMenuStateCommutator
    {
        public MenuStateCommutatorFull(List<CircleMenuItem> menuItems, 
            List<Indicator> indicators, 
            Action afterShowAction, 
            Action afterHideAction,
            List<PointF>[][] indicatorsListPositions,
            List<PointF>[][] menusListPositions,
            List<PointF>[][] indicatorsSpringListPositions,
            List<PointF>[][] menusSpringListPositions) : 
            base(menuItems, indicators, afterShowAction, afterHideAction, indicatorsListPositions, menusListPositions, indicatorsSpringListPositions, menusSpringListPositions)
        {
        }

        public void HideMenuItems()
        {
            for(int i = 1; i < _commutatorMenuItems.Count; i++)
            {
                var afterEndAnimation = i == 1 ? new Action(() => _afterHideAction?.Invoke()) : null;

                var menu = _commutatorMenuItems[i];
                var indicator = _commutatorIndicators[i];

                var positions = new List<PointF>();
                var indicatorPositions = new List<PointF>();

                for(int j = i; j < _commutatorMenuItems.Count; j++)
                {
                    positions.AddRange(_menusListPositions[0][j]);
                    indicatorPositions.AddRange(_indicatorsListPositions[0][j]);
                }

                var delay = AnimateDuration / 5 * i;
                StartAnimation(menu,
                    positions.Select(item => item.X).ToArray(),
                    positions.Select(item => item.Y).ToArray(),
                    AnimateDuration - delay,
                    0,
                    null,
                    new Action(() => afterEndAnimation?.Invoke()));

                StartAnimation(indicator,
                    indicatorPositions.Select(item => item.X).ToArray(),
                    indicatorPositions.Select(item => item.Y).ToArray(),
                    AnimateDuration - delay,
                    0);
            }
        }

        public void ShowMenuItems()
        {
            foreach(var menu in _commutatorMenuItems)
                menu.StartRotateAnimation();

            for(int i = 1; i < _commutatorMenuItems.Count; i++)
            {
                var afterEndAnimation = i == _commutatorMenuItems.Count - 1 ? new Action(() => _afterShowAction?.Invoke()) : null;

                var menu = _commutatorMenuItems[i];
                var indicator = _commutatorIndicators[i];

                var positions = new List<PointF>();
                var springPositions = new List<PointF>();
                var indicatorPositions = new List<PointF>();
                var indicatorSpringPositions = new List<PointF>();

                if(i == _commutatorMenuItems.Count - 1)
                {
                    positions = _menusListPositions[1][0];
                    springPositions = _menusSpringListPositions[1][0];
                    indicatorPositions = _indicatorsListPositions[1][0];
                    indicatorSpringPositions = _indicatorsSpringListPositions[1][0];
                }
                else
                {
                    springPositions = _menusSpringListPositions[1][i + 1];
                    indicatorSpringPositions = _indicatorsSpringListPositions[1][i + 1];

                    for(int j = _commutatorMenuItems.Count - 1; j > i; j--)
                    {
                        positions.AddRange(_menusListPositions[1][j]);
                        indicatorPositions.AddRange(_indicatorsListPositions[1][j]);
                    }
                }

                var delay = AnimateDuration / 5 * i;
                StartAnimation(menu,
                    positions.Select(item => item.X).ToArray(),
                    positions.Select(item => item.Y).ToArray(),
                    AnimateDuration - delay,
                    delay,
                    null,
                    new Action(() => StartAnimation(menu,
                        springPositions.Select(item => item.X).ToArray(),
                        springPositions.Select(item => item.Y).ToArray(),
                        SpringAnimateDuration,
                        0,
                        null,
                        afterEndAnimation)));

                StartAnimation(indicator,
                    indicatorPositions.Select(item => item.X).ToArray(),
                    indicatorPositions.Select(item => item.Y).ToArray(),
                    AnimateDuration - delay,
                    delay,
                    null,
                    new Action(() => StartAnimation(indicator,
                        indicatorSpringPositions.Select(item => item.X).ToArray(),
                        indicatorSpringPositions.Select(item => item.Y).ToArray(),
                        SpringAnimateDuration,
                        0)));
            }
        }
    }
}
