using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace EOS.UI.Android.Components
{
    /// <summary>
    /// Menu custom internal control
    /// On scroll lmplemented rotation animation 
    /// </summary>
    internal class CircleMenuItem : FrameLayout
    {
        #region fields

        private const float StartAngle = -180f;
        private const float EndAngle = 0f;
        private const float PivotScale = 0.5f;
        private const int RotateDimention = 380;

        private const float ShadowRadiusValue = 8f;

        private ImageView _icon;

        #endregion

        #region .ctors

        public CircleMenuItem(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        public CircleMenuItem(Context context) : base(context)
        {
            Initialize();
        }

        public CircleMenuItem(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public CircleMenuItem(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(attrs);
        }

        public CircleMenuItem(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize(attrs);
        }

        #endregion

        #region utility methods

        private void Initialize(IAttributeSet attrs = null)
        {
            var inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            var view = inflater.Inflate(Resource.Layout.CircleMenuItem, this);
            _icon = view.FindViewById<ImageView>(Resource.Id.icon);
            var roundedDrawable = new GradientDrawable();
            roundedDrawable.SetColor(Color.White);
            roundedDrawable.SetShape(ShapeType.Oval);
            view.SetBackgroundDrawable(roundedDrawable);
            view.Elevation = ShadowRadiusValue;
        }

        public void StartRotateAnimation()
        {
            var scaleInAnimation = new RotateAnimation(StartAngle, EndAngle, Dimension.RelativeToSelf, PivotScale, Dimension.RelativeToSelf, PivotScale);
            scaleInAnimation.Duration = RotateDimention;
            _icon.StartAnimation(scaleInAnimation);
        }

        #endregion
    }
}
