using System;
using Android.Views;

namespace UIFrameworks.Android.Traverser
{
    public interface IViewTraverser
    {
        void TraverseView<T>(ViewGroup viewGroup, Action action);
    }
}