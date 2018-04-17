using System;
using UIKit;

namespace EOS.UI.iOS.Traverser
{
    public class EOSViewTraverser : IEOSViewTraverser
    {
        public void TraverseView<T>(UIView parentView, Action<T> action)
        {
            foreach(var subView in parentView.Subviews)
            {
                if(subView is T view)
                    action?.Invoke(view);
                else
                    TraverseView(subView, action);
            }
        }
    }
}
