using System;
using Android.Views;

namespace EOS.UI.Droid.Traverser
{
    public class EOSViewTraverser : IEOSViewTraverser
    {
        public void TraverseView<T>(ViewGroup viewGroup, Action<T> action)
        {
            for(int i = 0; i < viewGroup.ChildCount; i++)
            {
                if(viewGroup.GetChildAt(i) is T view)
                    action?.Invoke(view);
                else if(viewGroup.GetChildAt(i) is ViewGroup childrenViewGroup)
                    TraverseView(childrenViewGroup, action);
            }
        }
    }
}