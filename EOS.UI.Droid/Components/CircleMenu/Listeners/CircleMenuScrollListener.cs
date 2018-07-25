using Android.Views;

namespace EOS.UI.Droid.Components
{
    internal class CircleMenuScrollListener
    {
        private const float MinSwipeWidth = 50;

        private float _bufferX;
        private float _bufferY;
        private bool _isMovedRight;
        private bool _isMovedLeft;

        public bool IsScrolled(ref bool isForward, MotionEvent motionEvent)
        {
            if(motionEvent.Action == MotionEventActions.Down)
            {
                _bufferX = motionEvent.RawX;
                _bufferY = motionEvent.RawY;
                return false;
            }
            else if(motionEvent.Action == MotionEventActions.Move && _bufferX != motionEvent.RawX)
            {
                //checking if swipe was horizontal and not vertical
                if(_bufferX - motionEvent.RawX > MinSwipeWidth && System.Math.Abs(_bufferX - motionEvent.RawX) > System.Math.Abs(_bufferY - motionEvent.RawY))
                    _isMovedLeft = true;
                if(_bufferX - motionEvent.RawX < -MinSwipeWidth && System.Math.Abs(_bufferX - motionEvent.RawX) > System.Math.Abs(_bufferY - motionEvent.RawY))
                    _isMovedRight = true;

                return false;
            }
            else if(motionEvent.Action == MotionEventActions.Up && !_isMovedRight && !_isMovedLeft)
            {
                return false;
            }
            else if(motionEvent.Action == MotionEventActions.Up && (_isMovedRight || _isMovedLeft))
            {
                isForward = _isMovedRight;
                _bufferX = 0f;
                _bufferY = 0f;
                _isMovedRight = false;
                _isMovedLeft = false;

                return true;
            }
            return false;
        }
    }
}
