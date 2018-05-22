using System;

namespace EOS.UI.Android.Sandbox.Helpers
{
    public class PlatformTimer
    {
        private System.Timers.Timer _timer;
        private TimeSpan _interval;
        private Action _callback;

        public bool IsRunning => _timer?.Enabled ?? false;

        public TimeSpan Interval => _interval;

        public void Setup(TimeSpan interval, Action callback)
        {
            Reset();
            _interval = interval;
            _callback = callback;
            _timer = new System.Timers.Timer(_interval.TotalMilliseconds);
            _timer.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) => _callback();
        }

        public void Start()
        {
            if (_callback == null || _timer == null)
                return;
            _timer.Start();
        }

        public void Stop()
        {
            if (_timer == null)
                return;

            _timer.Stop();
        }

        public void Reset()
        {
            if (_timer == null)
                return;

            _timer.Stop();
            _timer.Dispose();
            _timer = null;
        }
    }
}
