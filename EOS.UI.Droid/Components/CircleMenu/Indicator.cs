using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace EOS.UI.Droid.Components
{
    public class Indicator : FrameLayout
    {
        #region constants

        private string BlackoutTag = "Blackout";

        #endregion

        #region properties

        public Color BlackoutColor { get; set; }

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;

                var view = FindViewWithTag(BlackoutTag);
                (view.Background as GradientDrawable).SetColor(value ? Color.Transparent : BlackoutColor);
            }
        }

        #endregion

        #region .ctor

        public Indicator(Context context) : base(context)
        {
            Initialize();
        }

        public Indicator(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize();
        }

        public Indicator(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize();
        }

        public Indicator(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize();
        }

        protected Indicator(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        #endregion

        #region utility methods

        private void Initialize(IAttributeSet attrs = null)
        {
            AddView(CreateBlackoutView());
        }

        private View CreateBlackoutView()
        {
            var view = new View(Context);
            var layoutParameters = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, RelativeLayout.LayoutParams.MatchParent);
            layoutParameters.AddRule(LayoutRules.CenterInParent);
            view.LayoutParameters = layoutParameters;
            var roundedDrawable = new GradientDrawable();
            roundedDrawable.SetColor(Color.Transparent);
            roundedDrawable.SetShape(ShapeType.Oval);
            view.SetBackgroundDrawable(roundedDrawable);
            view.Tag = BlackoutTag;
            return view;
        }

        #endregion
    }
}
