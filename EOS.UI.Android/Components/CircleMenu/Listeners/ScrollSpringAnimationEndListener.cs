using Android.Content;
using Android.Support.Animation;
using Android.Views;
using static Android.Support.Animation.DynamicAnimation;

namespace EOS.UI.Android.Components
{
    internal class ScrollSpringAnimationEndListener: View, IOnAnimationEndListener
    {
        private CircleMenu _menu;

        internal ScrollSpringAnimationEndListener(Context context, CircleMenu menu): base(context)
        {
            _menu = menu;
        }

        #region IOnAnimationEndListener implementation

        public void OnAnimationEnd(DynamicAnimation animation, bool canceled, float value, float velocity)
        {
            if(_menu.IsScrolling)
            {
                _menu.HandleOnScrollSpringAnimationEnd();
                _menu.IsScrolling = false;
            }
        }

        #endregion
    }
}
