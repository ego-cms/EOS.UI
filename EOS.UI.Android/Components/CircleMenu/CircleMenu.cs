using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace EOS.UI.Android.Components
{
    public class CircleMenu: FrameLayout, View.IOnTouchListener
    {
        #region fields

        private HamburgerMenu _hamburgerMenu;
        private RelativeLayout _container;
        private float _bufferX;
        private bool _isMovedRight;
        private bool _isMovedLeft;

        #endregion

        #region .ctors

        public CircleMenu(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        public CircleMenu(Context context) : base(context)
        {
            Initialize();
        }

        public CircleMenu(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public CircleMenu(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(attrs);
        }

        public CircleMenu(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize(attrs);
        }

        #endregion

        #region utility methods

        private void Initialize(IAttributeSet attrs = null)
        {
            var inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            var view = inflater.Inflate(Resource.Layout.CircleMenu, this);
            _container = view.FindViewById<RelativeLayout>(Resource.Id.container);

            _hamburgerMenu = view.FindViewById<HamburgerMenu>(Resource.Id.hamburgerMenu);
            _hamburgerMenu.Click += HamburgerMenuClick;

            _container.SetOnTouchListener(this);
        }

        private void HamburgerMenuClick(object sender, EventArgs e)
        {
            _hamburgerMenu.IsOpened = !_hamburgerMenu.IsOpened;
            _container.SetBackgroundColor(_hamburgerMenu.IsOpened ? Color.Argb(50, 0, 0, 0) : Color.Transparent);
        }

        public void Attach(ViewGroup viewGroup)
        {
            var parameters = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, RelativeLayout.LayoutParams.MatchParent);
            viewGroup.AddView(this, parameters);
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            if(e.Action == MotionEventActions.Down)
            {
                _bufferX = e.RawX;
            }
            else if(e.Action == MotionEventActions.Move && _bufferX != e.RawX)
            {
                if(_bufferX - e.RawX > 10)
                    _isMovedLeft = true;
                if(_bufferX - e.RawX < -10)
                    _isMovedRight = true;
            }
            if(e.Action == MotionEventActions.Up && !_isMovedRight && !_isMovedLeft)
            {
            }
            if(e.Action == MotionEventActions.Up && (_isMovedRight || _isMovedLeft))
            {
                Toast.MakeText(Context, _isMovedRight ? "right" : "left", ToastLength.Short).Show();
                _bufferX = 0f;
                _isMovedRight = false;
                _isMovedLeft = false;
            }
            return true;
        }

        #endregion
    }
}
