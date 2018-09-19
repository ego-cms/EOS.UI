using System;
using System.Collections.Generic;
using System.Linq;
using Android.Graphics;
using EOS.UI.Droid.Interfaces;

namespace EOS.UI.Droid.Components
{
    internal class MenuStateCommutatorSimple : MenuStateCommutator, IMenuStateCommutator
    {
        public MenuStateCommutatorSimple(List<CircleMenuItem> menuItems,
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
            List<PointF>[] indicatorsSpringListPositionsBack) :
            base(menuItems,
                indicators,
                afterShowAction,
                afterHideAction,
                menusListPositionsForward,
                menusListPositionsBack,
                indicatorsListPositionsForward,
                indicatorsListPositionsBack,
                menusSpringListPositionsForward,
                menusSpringListPositionsBack,
                indicatorsSpringListPositionsForward,
                indicatorsSpringListPositionsBack)
        {
        }

        public void HideMenuItems()
        {
            for(int i = 2; i < _commutatorMenuItems.Count - 1; i++)
            {
                var afterEndAnimation = i == 2 ? new Action(() => _afterHideAction?.Invoke()) : null;

                var menu = _commutatorMenuItems[i];
                var indicator = _commutatorIndicators[i];

                var positions = new List<PointF>();
                var indicatorPositions = new List<PointF>();

                for(int j = i; j < _commutatorMenuItems.Count; j++)
                {
                    positions.AddRange(_menusListPositionsForward[j]);
                    indicatorPositions.AddRange(_indicatorsListPositionsForward[j]);
                }

                var delay = AnimateDuration / 5 * i;
                StartCloseAnimation(menu, indicator, positions, indicatorPositions, delay, afterEndAnimation);
            }
        }

        public void ShowMenuItems()
        {
            foreach(var menu in _commutatorMenuItems)
                menu.StartRotateAnimation();

            for(int i = 2; i < _commutatorMenuItems.Count - 1; i++)
            {
                var afterEndAnimation = i == _commutatorMenuItems.Count - 2 ? new Action(() => _afterShowAction?.Invoke()) : null;

                var menu = _commutatorMenuItems[i];
                var indicator = _commutatorIndicators[i];

                var positions = new List<PointF>();
                var springPositions = new List<PointF>();
                var indicatorPositions = new List<PointF>();
                var indicatorSpringPositions = new List<PointF>();

                if(i == _commutatorMenuItems.Count - 1)
                {
                    positions = _menusListPositionsBack[0];
                    springPositions = _menusSpringListPositionsBack[0];
                    indicatorPositions = _indicatorsListPositionsBack[0];
                    indicatorSpringPositions = _indicatorsSpringListPositionsBack[0];
                }
                else
                {
                    springPositions = _menusSpringListPositionsBack[i + 1];
                    indicatorSpringPositions = _indicatorsSpringListPositionsBack[i + 1];

                    for(int j = _commutatorMenuItems.Count - 1; j > i; j--)
                    {
                        positions.AddRange(_menusListPositionsBack[j]);
                        indicatorPositions.AddRange(_indicatorsListPositionsBack[j]);
                    }
                }

                var delay = AnimateDuration / 5 * i;
                StartOpenAnimation(menu, indicator, positions, indicatorPositions, springPositions, indicatorSpringPositions, delay, afterEndAnimation);
            }
        }
    }
}
