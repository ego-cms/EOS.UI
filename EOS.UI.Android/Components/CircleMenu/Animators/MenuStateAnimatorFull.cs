using System;
using System.Collections.Generic;
using Android.Graphics;
using Android.Support.Animation;
using EOS.UI.Android.Interfaces;

namespace EOS.UI.Android.Components
{
    internal class MenuStateAnimatorFull : MenuStateAnimator, IMenuStateAnimator
    {
        public MenuStateAnimatorFull(List<CircleMenuItem> menuItems, 
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

                _indicators[1].Animate().X(_indicatorsPositions[4].X).Y(_indicatorsPositions[4].Y).SetDuration(ShowHideAnimateDuration);
                _indicators[2].Animate().X(_indicatorsPositions[3].X).Y(_indicatorsPositions[3].Y).SetDuration(ShowHideAnimateDuration);
                _indicators[3].Animate().X(_indicatorsPositions[2].X).Y(_indicatorsPositions[2].Y).SetDuration(ShowHideAnimateDuration);
                _indicators[4].Animate().X(_indicatorsPositions[1].X).Y(_indicatorsPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _indicators[5].Animate().X(_indicatorsPositions[0].X).Y(_indicatorsPositions[0].Y).SetDuration(ShowHideAnimateDuration);

                _menuItems[1].Animate().X(_menuItemsPositions[4].X).Y(_menuItemsPositions[4].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[2].Animate().X(_menuItemsPositions[3].X).Y(_menuItemsPositions[3].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[3].Animate().X(_menuItemsPositions[2].X).Y(_menuItemsPositions[2].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[4].Animate().X(_menuItemsPositions[1].X).Y(_menuItemsPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[5].Animate().X(_menuItemsPositions[0].X).Y(_menuItemsPositions[0].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_updateRunnable);
            }
            if(iteration == 2)
            {
                _indicators[1].Animate().X(_indicatorsPositions[3].X).Y(_indicatorsPositions[3].Y).SetDuration(ShowHideAnimateDuration);
                _indicators[2].Animate().X(_indicatorsPositions[2].X).Y(_indicatorsPositions[2].Y).SetDuration(ShowHideAnimateDuration);
                _indicators[3].Animate().X(_indicatorsPositions[1].X).Y(_indicatorsPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _indicators[4].Animate().X(_indicatorsPositions[0].X).Y(_indicatorsPositions[0].Y).SetDuration(ShowHideAnimateDuration);

                _menuItems[1].Animate().X(_menuItemsPositions[3].X).Y(_menuItemsPositions[3].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[2].Animate().X(_menuItemsPositions[2].X).Y(_menuItemsPositions[2].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[3].Animate().X(_menuItemsPositions[1].X).Y(_menuItemsPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[4].Animate().X(_menuItemsPositions[0].X).Y(_menuItemsPositions[0].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_updateRunnable);
            }
            if(iteration == 3)
            {
                _indicators[1].Animate().X(_indicatorsPositions[2].X).Y(_indicatorsPositions[2].Y).SetDuration(ShowHideAnimateDuration);
                _indicators[2].Animate().X(_indicatorsPositions[1].X).Y(_indicatorsPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _indicators[3].Animate().X(_indicatorsPositions[0].X).Y(_indicatorsPositions[0].Y).SetDuration(ShowHideAnimateDuration);

                _menuItems[1].Animate().X(_menuItemsPositions[2].X).Y(_menuItemsPositions[2].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[2].Animate().X(_menuItemsPositions[1].X).Y(_menuItemsPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[3].Animate().X(_menuItemsPositions[0].X).Y(_menuItemsPositions[0].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_updateRunnable);
            }
            if(iteration == 4)
            {
                _indicators[1].Animate().X(_indicatorsPositions[1].X).Y(_indicatorsPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _indicators[2].Animate().X(_indicatorsPositions[0].X).Y(_indicatorsPositions[0].Y).SetDuration(ShowHideAnimateDuration);

                _menuItems[1].Animate().X(_menuItemsPositions[1].X).Y(_menuItemsPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[2].Animate().X(_menuItemsPositions[0].X).Y(_menuItemsPositions[0].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_updateRunnable);
            }
            if(iteration == 5)
            {
                _indicators[1].Animate().X(_indicatorsPositions[0].X).Y(_indicatorsPositions[0].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[1].Animate().X(_menuItemsPositions[0].X).Y(_menuItemsPositions[0].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_updateRunnable);
            }
        }

        public void ShowMenuItems(int iteration)
        {
            if(iteration == 1)
            {
                foreach(var menu in _menuItems)
                    menu.StartRotateAnimation();

                _indicators[1].Animate().X(_indicatorsPositions[0].X).Y(_indicatorsPositions[0].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[1].Animate().X(_menuItemsPositions[0].X).Y(_menuItemsPositions[0].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_updateRunnable);
            }
            if(iteration == 2)
            {
                _indicators[2].Animate().X(_indicatorsPositions[0].X).Y(_indicatorsPositions[0].Y).SetDuration(ShowHideAnimateDuration);
                _indicators[1].Animate().X(_indicatorsPositions[1].X).Y(_indicatorsPositions[1].Y).SetDuration(ShowHideAnimateDuration);

                _menuItems[2].Animate().X(_menuItemsPositions[0].X).Y(_menuItemsPositions[0].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[1].Animate().X(_menuItemsPositions[1].X).Y(_menuItemsPositions[1].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_updateRunnable);
            }
            if(iteration == 3)
            {
                _indicators[3].Animate().X(_indicatorsPositions[0].X).Y(_indicatorsPositions[0].Y).SetDuration(ShowHideAnimateDuration);
                _indicators[2].Animate().X(_indicatorsPositions[1].X).Y(_indicatorsPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _indicators[1].Animate().X(_indicatorsPositions[2].X).Y(_indicatorsPositions[2].Y).SetDuration(ShowHideAnimateDuration);

                _menuItems[3].Animate().X(_menuItemsPositions[0].X).Y(_menuItemsPositions[0].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[2].Animate().X(_menuItemsPositions[1].X).Y(_menuItemsPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[1].Animate().X(_menuItemsPositions[2].X).Y(_menuItemsPositions[2].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_updateRunnable);
            }
            if(iteration == 4)
            {
                _indicators[4].Animate().X(_indicatorsPositions[0].X).Y(_indicatorsPositions[0].Y).SetDuration(ShowHideAnimateDuration);
                _indicators[3].Animate().X(_indicatorsPositions[1].X).Y(_indicatorsPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _indicators[2].Animate().X(_indicatorsPositions[2].X).Y(_indicatorsPositions[2].Y).SetDuration(ShowHideAnimateDuration);
                _indicators[1].Animate().X(_indicatorsPositions[3].X).Y(_indicatorsPositions[3].Y).SetDuration(ShowHideAnimateDuration);

                _menuItems[4].Animate().X(_menuItemsPositions[0].X).Y(_menuItemsPositions[0].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[3].Animate().X(_menuItemsPositions[1].X).Y(_menuItemsPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[2].Animate().X(_menuItemsPositions[2].X).Y(_menuItemsPositions[2].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[1].Animate().X(_menuItemsPositions[3].X).Y(_menuItemsPositions[3].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_updateRunnable);
            }
            if(iteration == 5)
            {
                //last iteration of showing menus should be with spring animation
                for(int i = _menuItems.Count - 1; i > 0; i--)
                {
                    var menu = _menuItems[i];
                    var point = _menuItemsPositions[_menuItems.Count - i];

                    var indicator = _indicators[i];
                    var pointIndicator = _indicatorsPositions[_menuItems.Count - i];

                    var springX = new SpringAnimation(menu, DynamicAnimation.X, point.X);
                    var springY = new SpringAnimation(menu, DynamicAnimation.Y, point.Y);

                    var springIndicatorX = new SpringAnimation(indicator, DynamicAnimation.X, pointIndicator.X);
                    var springIndicatorY = new SpringAnimation(indicator, DynamicAnimation.Y, pointIndicator.Y);

                    if(i == 1)
                    {
                        _afterShowAction?.Invoke();
                        springY.AddEndListener(_animationEndListener);
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
