using System;
using Android.Views;

namespace UIFrameworks.Android.Traverser
{
    public interface IEOSViewTraverser
    {
        void TraverseView<T>(ViewGroup viewGroup, Action<T> action);
    }
}