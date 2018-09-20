using Android.Views;

namespace EOS.UI.Droid.Components
{
    internal class CircleMenuScrollListener
    {
        private const float MinScrollWidth = 20;
        private const float MinSpinWidth = 15;

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
            _isScrollMovedRight = false;
            _isScrollMovedLeft = false;

            if(motionEvent.Action == MotionEventActions.Down)
            {
                _bufferScrollX = motionEvent.RawX;
                _bufferScrollY = motionEvent.RawY;
                return false;
            }
            else if(motionEvent.Action == MotionEventActions.Move && _bufferScrollX != motionEvent.RawX && (!_isScrollMovedLeft && !_isScrollMovedRight))
            {
                if(_bufferScrollX != 0 && _bufferScrollX - motionEvent.RawX > MinScrollWidth && System.Math.Abs(_bufferScrollX - motionEvent.RawX) > System.Math.Abs(_bufferScrollY - motionEvent.RawY))
                    _isScrollMovedLeft = true;

                else if(_bufferScrollY != 0 &&_bufferScrollY - motionEvent.RawY < -MinScrollWidth && System.Math.Abs(_bufferScrollX - motionEvent.RawX) < System.Math.Abs(_bufferScrollY - motionEvent.RawY))
                    _isScrollMovedLeft = true;

                else if(_bufferScrollX != 0 && _bufferScrollX - motionEvent.RawX < -MinScrollWidth && System.Math.Abs(_bufferScrollX - motionEvent.RawX) > System.Math.Abs(_bufferScrollY - motionEvent.RawY))
                    _isScrollMovedRight = true;

                else if(_bufferScrollY != 0 && _bufferScrollY - motionEvent.RawY > MinScrollWidth && System.Math.Abs(_bufferScrollX - motionEvent.RawX) < System.Math.Abs(_bufferScrollY - motionEvent.RawY))
                    _isScrollMovedRight = true;

                if(_isScrollMovedRight || _isScrollMovedLeft)
                {
                    isForward = _isScrollMovedRight;
                    _bufferScrollX = 0f;
                    _bufferScrollY = 0f;
                }

                return _isScrollMovedRight || _isScrollMovedLeft;
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
