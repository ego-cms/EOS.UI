using System;
using Android.App;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Widget;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.Android.Controls
{
    public class CircleProgress : Fragment, View.IOnTouchListener, IEOSThemeControl
    {
        private bool _isRunning;
        private ProgressBar _progressBar;
        private TextView _percentText;
        private ImageView _checkmarkImage;

        public event EventHandler Started;
        public event EventHandler Stopped;
        public event EventHandler Finished;

        public bool IsEOSCustomizationIgnored { get; private set; }

        private int _progress;
        public int Progress
        {
            get => _progress;
            set
            {
                if (value > 100)
                    return;
                Activity.RunOnUiThread(() =>
                {
                    _progress = value;
                    if (_checkmarkImage.Visibility == ViewStates.Visible)
                        _checkmarkImage.Visibility = ViewStates.Invisible;
                    _progressBar.SetProgress(_progress, true);
                    _percentText.Text = $"{value} %";
                    if (_progress == 100)
                    {
                        ShowCheckmark();
                    }
                });
            }
        }

        private Color _color;
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                IsEOSCustomizationIgnored = true;
                _progressBar.ProgressTintList = ColorStateList.ValueOf(_color);
                _progressBar.SecondaryProgressTintList = ColorStateList.ValueOf(_color);
                _percentText.SetTextColor(_color);
            }
        }

        private Color _alternativeColor;
        public Color AlternativeColor
        {
            get => _alternativeColor;
            set
            {
                _alternativeColor = value;
                (_checkmarkImage.Background as GradientDrawable).SetColor(ColorStateList.ValueOf(_alternativeColor));
                IsEOSCustomizationIgnored = true;
            }
        }

        private bool _showProgress;
        public bool ShowProgress
        {
            get => _showProgress;
            set
            {
                _showProgress = value;
                _percentText.Visibility = _showProgress ? ViewStates.Visible : ViewStates.Invisible;
                IsEOSCustomizationIgnored = true;
            }
        }

        private Typeface _typeface;
        public Typeface Typeface
        {
            get => _typeface;
            set
            {
                _typeface = value;
                _percentText.Typeface = _typeface;
                IsEOSCustomizationIgnored = true;
            }
        }

        private float _textSize;
        public float TextSize
        {
            get => _textSize;
            set
            {
                _textSize = value;
                _percentText.TextSize = _textSize;
                IsEOSCustomizationIgnored = true;
            }
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CircleProgress, container, false);
            _progressBar = view.FindViewById<ProgressBar>(Resource.Id.circularProgressbar);
            _percentText = view.FindViewById<TextView>(Resource.Id.percentText);
            _checkmarkImage = view.FindViewById<ImageView>(Resource.Id.checkmark);
            view.SetOnTouchListener(this);
            _checkmarkImage.Visibility = ViewStates.Invisible;
            _percentText.Text = "0 %";
            UpdateAppearance();
            return view;
        }

        public IEOSStyle GetCurrentEOSStyle()
        {
            return null;
        }

        public IEOSThemeProvider GetThemeProvider()
        {
            return EOSThemeProvider.Instance;
        }

        public void ResetCustomization()
        {
            IsEOSCustomizationIgnored = false;
            UpdateAppearance();
        }

        public void SetEOSStyle(EOSStyleEnumeration style)
        {
        }

        public void UpdateAppearance()
        {
            if (!IsEOSCustomizationIgnored)
            {
                var provider = GetThemeProvider();
                Color = provider.GetEOSProperty<Color>(this, EOSConstants.PrimaryColor);
                AlternativeColor = provider.GetEOSProperty<Color>(this, EOSConstants.SecondaryColor);
                ShowProgress = provider.GetEOSProperty<bool>(this, EOSConstants.CircleProgressShown);
                Typeface = Typeface.CreateFromAsset(Context.Assets, provider.GetEOSProperty<string>(this, EOSConstants.Font));
                TextSize = provider.GetEOSProperty<float>(this, EOSConstants.TextSize);

                IsEOSCustomizationIgnored = false;
            }
        }

        private void ShowCheckmark()
        {
            Finished?.Invoke(this, new EventArgs());
            _checkmarkImage.Visibility = ViewStates.Visible;
            _isRunning = false;
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            if (!_isRunning)
            {
                Started?.Invoke(this, new EventArgs());
                _isRunning = true;
            }
            else
            {
                Stopped?.Invoke(this, new EventArgs());
                _isRunning = false;
            }
            return false;
        }
    }
}
