using System;
using System.Collections.Generic;
using Android.Graphics;
using Android.Support.Animation;
using EOS.UI.Android.Interfaces;

namespace EOS.UI.Android.Components
{
    internal class MenuStateCommutatorSimple : MenuStateCommutator, IMenuStateCommutator
    {
        public MenuStateCommutatorSimple(List<CircleMenuItem> menuItems, 
            List<Indicator> indicators, 
            PointF[] menuItemsPositions, 
            PointF[] indicatorsPositions, 
            Action afterShowAction, 
            Action afterHideAction, 
            UpdateMenuItemsVisibilityRunnable updateRunnable, 
            OpenSpringAnimationEndListener animationEndListener) : 
            base(menuItems, indicators, menuItemsPositions, indicatorsPositions, afterShowAction, afterHideAction, updateRunnable, animationEndListener)
        {
        }

        public void HideMenuItems(int iteration)
        {
            if(iteration == 1)
            {
                _afterHideAction?.Invoke();

                _commutatorIndicators[2].Animate().X(_commutatorIndicatorsPositions[3].X).Y(_commutatorIndicatorsPositions[3].Y).SetDuration(ShowHideAnimateDuration);
                _commutatorIndicators[3].Animate().X(_commutatorIndicatorsPositions[2].X).Y(_commutatorIndicatorsPositions[2].Y).SetDuration(ShowHideAnimateDuration);
                _commutatorIndicators[4].Animate().X(_commutatorIndicatorsPositions[1].X).Y(_commutatorIndicatorsPositions[1].Y).SetDuration(ShowHideAnimateDuration);

                _commutatorMenuItems[2].Animate().X(_commutatorMenuItemsPositions[3].X).Y(_commutatorMenuItemsPositions[3].Y).SetDuration(ShowHideAnimateDuration);
                _commutatorMenuItems[3].Animate().X(_commutatorMenuItemsPositions[2].X).Y(_commutatorMenuItemsPositions[2].Y).SetDuration(ShowHideAnimateDuration);
                _commutatorMenuItems[4].Animate().X(_commutatorMenuItemsPositions[1].X).Y(_commutatorMenuItemsPositions[1].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_commutatorUpdateRunnable);
            }
            if(iteration == 2)
            {
                _commutatorIndicators[2].Animate().X(_commutatorIndicatorsPositions[2].X).Y(_commutatorIndicatorsPositions[2].Y).SetDuration(ShowHideAnimateDuration);
                _commutatorIndicators[3].Animate().X(_commutatorIndicatorsPositions[1].X).Y(_commutatorIndicatorsPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _commutatorIndicators[4].Animate().X(_commutatorIndicatorsPositions[0].X).Y(_commutatorIndicatorsPositions[0].Y).SetDuration(ShowHideAnimateDuration);

                _commutatorMenuItems[2].Animate().X(_commutatorMenuItemsPositions[2].X).Y(_commutatorMenuItemsPositions[2].Y).SetDuration(ShowHideAnimateDuration);
                _commutatorMenuItems[3].Animate().X(_commutatorMenuItemsPositions[1].X).Y(_commutatorMenuItemsPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _commutatorMenuItems[4].Animate().X(_commutatorMenuItemsPositions[0].X).Y(_commutatorMenuItemsPositions[0].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_commutatorUpdateRunnable);
            }
            if(iteration == 3)
            {
                _commutatorIndicators[2].Animate().X(_commutatorIndicatorsPositions[1].X).Y(_commutatorIndicatorsPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _commutatorIndicators[3].Animate().X(_commutatorIndicatorsPositions[0].X).Y(_commutatorIndicatorsPositions[0].Y).SetDuration(ShowHideAnimateDuration);

                _commutatorMenuItems[2].Animate().X(_commutatorMenuItemsPositions[1].X).Y(_commutatorMenuItemsPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _commutatorMenuItems[3].Animate().X(_commutatorMenuItemsPositions[0].X).Y(_commutatorMenuItemsPositions[0].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_commutatorUpdateRunnable);
            }
            if(iteration == 4)
            {
                _commutatorIndicators[2].Animate().X(_commutatorIndicatorsPositions[0].X).Y(_commutatorIndicatorsPositions[0].Y).SetDuration(ShowHideAnimateDuration);

                _commutatorMenuItems[2].Animate().X(_commutatorMenuItemsPositions[0].X).Y(_commutatorMenuItemsPositions[0].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_commutatorUpdateRunnable);
            }
            if(iteration == 5)
            {
                _commutatorMenuItems[1].Animate().XBy(0).YBy(0).SetDuration(ShowHideAnimateDuration).WithEndAction(_commutatorUpdateRunnable);
            }
        }

        public void ShowMenuItems(int iteration)
        {
            if(iteration == 1)
            {
                foreach(var menu in _commutatorMenuItems)
                    menu.StartRotateAnimation();

                _commutatorMenuItems[1].Animate().XBy(0).YBy(0).SetDuration(ShowHideAnimateDuration).WithEndAction(_commutatorUpdateRunnable);
            }
            if(iteration == 2)
            {
                _commutatorIndicators[2].Animate().X(_commutatorIndicatorsPositions[0].X).Y(_commutatorIndicatorsPositions[0].Y).SetDuration(ShowHideAnimateDuration);

                _commutatorMenuItems[2].Animate().X(_commutatorMenuItemsPositions[0].X).Y(_commutatorMenuItemsPositions[0].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_commutatorUpdateRunnable);
            }
            if(iteration == 3)
            {
                _commutatorIndicators[3].Animate().X(_commutatorIndicatorsPositions[0].X).Y(_commutatorIndicatorsPositions[0].Y).SetDuration(ShowHideAnimateDuration);
                _commutatorIndicators[2].Animate().X(_commutatorIndicatorsPositions[1].X).Y(_commutatorIndicatorsPositions[1].Y).SetDuration(ShowHideAnimateDuration);

                _commutatorMenuItems[3].Animate().X(_commutatorMenuItemsPositions[0].X).Y(_commutatorMenuItemsPositions[0].Y).SetDuration(ShowHideAnimateDuration);
                _commutatorMenuItems[2].Animate().X(_commutatorMenuItemsPositions[1].X).Y(_commutatorMenuItemsPositions[1].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_commutatorUpdateRunnable);
            }
            if(iteration == 4)
            {
                _commutatorIndicators[4].Animate().X(_commutatorIndicatorsPositions[0].X).Y(_commutatorIndicatorsPositions[0].Y).SetDuration(ShowHideAnimateDuration);
                _commutatorIndicators[3].Animate().X(_commutatorIndicatorsPositions[1].X).Y(_commutatorIndicatorsPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _commutatorIndicators[2].Animate().X(_commutatorIndicatorsPositions[2].X).Y(_commutatorIndicatorsPositions[2].Y).SetDuration(ShowHideAnimateDuration);

                _commutatorMenuItems[4].Animate().X(_commutatorMenuItemsPositions[0].X).Y(_commutatorMenuItemsPositions[0].Y).SetDuration(ShowHideAnimateDuration);
                _commutatorMenuItems[3].Animate().X(_commutatorMenuItemsPositions[1].X).Y(_commutatorMenuItemsPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _commutatorMenuItems[2].Animate().X(_commutatorMenuItemsPositions[2].X).Y(_commutatorMenuItemsPositions[2].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_commutatorUpdateRunnable);
            }
            if(iteration == 5)
            {
                //last iteration of showing menus should be with spring animation
                for(int i = _commutatorMenuItems.Count - 2; i > 1; i--)
                {
                    var menu = _commutatorMenuItems[i];
                    var point = _commutatorMenuItemsPositions[_commutatorMenuItems.Count - i];

                    var indicator = _commutatorIndicators[i];
                    var pointIndicator = _commutatorIndicatorsPositions[_commutatorMenuItems.Count - i];

                    var springX = new SpringAnimation(menu, DynamicAnimation.X, point.X);
                    var springY = new SpringAnimation(menu, DynamicAnimation.Y, point.Y);

                    var springIndicatorX = new SpringAnimation(indicator, DynamicAnimation.X, pointIndicator.X);
                    var springIndicatorY = new SpringAnimation(indicator, DynamicAnimation.Y, pointIndicator.Y);

                    if(i == 2)
                    {
                        _afterShowAction?.Invoke();
                        springY.AddEndListener(_commutatorAnimationEndListener);
                    }

                    springIndicatorX.Start();
                    springIndicatorY.Start();
                    springX.Start();
                    springY.Start();
                }
            }
        }
    }
}
