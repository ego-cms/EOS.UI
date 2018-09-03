using Android.Views;

namespace EOS.UI.Droid.Components
{
    internal class CircleMenuScrollListener
    {
        private const float MinScrollWidth = 50;
        private const float MinSpinWidth = 10;

        private float _bufferScrollX;
        private float _bufferScrollY;
        private bool _isScrollMovedRight;
        private bool _isScrollMovedLeft;

        private float _bufferSpinX;
        private float _bufferSpinY;
        private bool _isSpinMovedRight;
        private bool _isSpinMovedLeft;

        public bool IsScrolled(ref bool isForward, MotionEvent motionEvent)
        {
            if(motionEvent.Action == MotionEventActions.Down)
            {
                _bufferScrollX = motionEvent.RawX;
                _bufferScrollY = motionEvent.RawY;
                return false;
            }
            else if(motionEvent.Action == MotionEventActions.Move && _bufferScrollX != motionEvent.RawX)
            {
                //checking if swipe was horizontal and not vertical
                if(_bufferScrollX - motionEvent.RawX > MinScrollWidth || _bufferScrollY - motionEvent.RawY < -MinScrollWidth)
                    _isScrollMovedLeft = true;
                else if(_bufferScrollX - motionEvent.RawX < -MinScrollWidth || _bufferScrollY - motionEvent.RawY > MinScrollWidth)
                    _isScrollMovedRight = true;

                return false;
            }
            else if(motionEvent.Action == MotionEventActions.Up && !_isScrollMovedRight && !_isScrollMovedLeft)
            {
                return false;
            }
            else if(motionEvent.Action == MotionEventActions.Up && (_isScrollMovedRight || _isScrollMovedLeft))
            {
                isForward = _isScrollMovedRight;
                _bufferScrollX = 0f;
                _bufferScrollY = 0f;
                _isScrollMovedRight = false;
                _isScrollMovedLeft = false;

                return true;
            }
            return false;
        }

        public bool IsSpinRound(ref bool isForward, MotionEvent motionEvent)
        {
            if(motionEvent.Action == MotionEventActions.Down)
            {
                _bufferSpinX = motionEvent.RawX;
                _bufferSpinY = motionEvent.RawY;
                return false;
            }
            else if(motionEvent.Action == MotionEventActions.Move && _bufferScrollX != motionEvent.RawX)
            {
                if(_bufferSpinX - motionEvent.RawX > MinSpinWidth || _bufferSpinY - motionEvent.RawY < -MinSpinWidth)
                    _isSpinMovedLeft = true;
                if(_bufferSpinX - motionEvent.RawX < -MinSpinWidth || _bufferSpinY - motionEvent.RawY > MinSpinWidth)
                    _isSpinMovedRight = true;

                return false;
            }
            else if((motionEvent.Action == MotionEventActions.Up || motionEvent.Action == MotionEventActions.Cancel) && !_isSpinMovedRight && !_isSpinMovedLeft)
            {
                return false;
            }
            else if((motionEvent.Action == MotionEventActions.Up || motionEvent.Action == MotionEventActions.Cancel) && (_isSpinMovedRight || _isSpinMovedLeft))
            {
                isForward = _isSpinMovedRight;
                _bufferSpinX = 0f;
                _bufferSpinY = 0f;
                _isSpinMovedRight = false;
                _isSpinMovedLeft = false;

                return true;
            }
            return false;
        }
    }
}
