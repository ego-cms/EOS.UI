using System;
using Android.Views;

namespace EOS.UI.Droid.Traverser
{
    public interface IEOSViewTraverser
    {
        void TraverseView<T>(ViewGroup viewGroup, Action<T> action);
    }
}