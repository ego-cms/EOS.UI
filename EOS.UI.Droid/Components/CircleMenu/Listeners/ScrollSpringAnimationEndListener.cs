using Android.Content;
using Android.Support.Animation;
using Android.Views;
using static Android.Support.Animation.DynamicAnimation;

namespace EOS.UI.Droid.Components
{
    internal class ScrollSpringAnimationEndListener: View, IOnAnimationEndListener
    {
        private const int CountAnimations = 4;
        private CircleMenu _menu;
        private int _countAnimationsEnd;

        internal ScrollSpringAnimationEndListener(Context context, CircleMenu menu): base(context)
        {
            _menu = menu;
        }

        #region IOnAnimationEndListener implementation

        public void OnAnimationEnd(DynamicAnimation animation, bool canceled, float value, float velocity)
        {
            ++_countAnimationsEnd;
            if(_countAnimationsEnd == CountAnimations)
            {
                _menu.HandleOnScrollSpringAnimationEnd();
                _countAnimationsEnd = 0;
            }
        }

        #endregion
    }
}
