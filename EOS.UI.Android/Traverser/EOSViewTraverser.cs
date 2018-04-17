using System;
using Android.Views;

namespace UIFrameworks.Android.Traverser
{
    public class EOSViewTraverser : IEOSViewTraverser
    {
        public void TraverseView<T>(ViewGroup viewGroup, Action action)
        {
            for(int i = 0; i < viewGroup.ChildCount; i++)
            {
                if(viewGroup.GetChildAt(i) is T)
                    action?.Invoke();
            }
        }
    }
}