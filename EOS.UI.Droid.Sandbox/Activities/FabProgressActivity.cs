using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using EOS.UI.Droid.Controls;
using EOS.UI.Droid.Helpers;
using EOS.UI.Droid.Sandbox.Controls;
using EOS.UI.Shared.Helpers;
using EOS.UI.Shared.Sandbox.Helpers;
using EOS.UI.Shared.Themes.Themes;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.Droid.Sandbox.Activities
{
    [Activity(Label = ControlNames.FabProgress, Theme = "@style/Sandbox.Main")]
    public class FabProgressActivity : BaseActivity
    {
        public const string ShadowOffsetXKey = "ShadowOffsetXKey";
        public const string ShadowOffsetYKey = "ShadowOffsetYKey";
        public const string ShadowBlurKey = "ShadowBlurKey";
        public const string ShadowColorAKey = "ShadowColorAKey";
        public const string ShadowColorRKey = "ShadowColorRKey";
        public const string ShadowColorGKey = "ShadowColorGKey";
        public const string ShadowColorBKey = "ShadowColorBKey";

        EOSSandboxToolbar _toolbar;

        int _fabInitialSize = 0;
        double _shadowAlpha = 1.0f;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FabProgressLayout);
            SetToolbar();

            var fab = FindViewById<FabProgress>(Resource.Id.fabProgress);
            var themeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.themeDropDown);
            var backgroundColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.backgroundDropDown);
            var disabledColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.disabledColorDropDown);
            var pressedColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.pressedColorDropDown);
            var shadowOffsetXDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.shadowOffsetXDropDown);
            var shadowOffsetYDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.shadowOffsetYDropDown);
            var shadowBlurDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.shadowBlurDropDown);
            var shadowColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.shadowColorDropDown);
            var shadowOpacityDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.shadowOpacityDropDown);

            var stateSwitch = FindViewById<Switch>(Resource.Id.stateSwitch);
            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);
            fab.Click += async (sender, e) =>
            {
                if(fab.InProgress)
                    return;
                themeDropDown.Enabled = false;
                resetButton.Enabled = false;
                fab.StartProgressAnimation();
                await Task.Delay(5000);
                fab.StopProgressAnimation();
                themeDropDown.Enabled = true;
                resetButton.Enabled = true;
            };

            var spinners = new List<EOSSandboxDropDown>()
            {
                themeDropDown,
                disabledColorDropDown,
                pressedColorDropDown,
                backgroundColorDropDown,
                shadowOffsetXDropDown,
                shadowOffsetYDropDown,
                shadowBlurDropDown,
                shadowColorDropDown,
                shadowOpacityDropDown
            };

            themeDropDown.Name = Fields.Theme;
            themeDropDown.SetupAdapter(ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            themeDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                {
                    fab.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
                    fab.ResetCustomization();
                    ResetShadowFields(shadowOffsetXDropDown, shadowOffsetYDropDown, shadowBlurDropDown, shadowColorDropDown);
                    spinners.Except(new[] { themeDropDown }).ToList().ForEach(s => s.SetSpinnerSelection(0));
                    UpdateApperaence();
                    _shadowAlpha = fab.ShadowConfig != null ? (float)fab.ShadowConfig?.Color.A / 255 : 1;
                }
            };
            var theme = fab.GetThemeProvider().GetCurrentTheme();
            if(theme is LightEOSTheme)
                themeDropDown.SetSpinnerSelection(1);
            if(theme is DarkEOSTheme)
                themeDropDown.SetSpinnerSelection(2);

            backgroundColorDropDown.Name = Fields.Background;
            backgroundColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            backgroundColorDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                    fab.BackgroundColor = Colors.ColorsCollection.ElementAt(position).Value;
            };

            disabledColorDropDown.Name = Fields.DisabledColor;
            disabledColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            disabledColorDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                    fab.DisabledBackgroundColor = Colors.ColorsCollection.ElementAt(position).Value;
            };

            pressedColorDropDown.Name = Fields.PressedColor;
            pressedColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            pressedColorDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                    fab.PressedBackgroundColor = Colors.ColorsCollection.ElementAt(position).Value;
            };

            shadowOffsetXDropDown.Name = Fields.ShadowOffsetX;
            shadowOffsetXDropDown.SetupAdapter(Shadow.OffsetCollection.Select(item => item.Key).ToList());
            shadowOffsetXDropDown.ItemSelected += (position) =>
            {
                if (position > 0)
                {
                    fab.ShadowConfig.Offset.X = Shadow.OffsetCollection.ElementAt(position).Value;
                    ChangeShadow(fab);
                }
            };


            shadowOffsetYDropDown.Name = Fields.ShadowOffsetY;
            shadowOffsetYDropDown.SetupAdapter(Shadow.OffsetCollection.Select(item => item.Key).ToList());
            shadowOffsetYDropDown.ItemSelected += (position) =>
            {
                if (position > 0)
                {
                    fab.ShadowConfig.Offset.Y = Shadow.OffsetCollection.ElementAt(position).Value;
                    ChangeShadow(fab);
                }
            };

            shadowBlurDropDown.Name = Fields.ShadowRadius;
            shadowBlurDropDown.SetupAdapter(Shadow.RadiusCollection.Select(item => item.Key).ToList());
            shadowBlurDropDown.ItemSelected += (position) =>
            {
                if (position > 0)
                {
                    fab.ShadowConfig.Blur = Shadow.RadiusCollection.ElementAt(position).Value;
                    ChangeShadow(fab);
                }
            };

            shadowColorDropDown.Name = Fields.ShadowColor;
            shadowColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(i => i.Key).ToList());
            shadowColorDropDown.ItemSelected += (position) =>
            {
                if (position > 0)
                {
                    fab.ShadowConfig.Color = Colors.ColorsCollection.ElementAt(position).Value;
                    ChangeShadow(fab);
                }
            };

            shadowOpacityDropDown.Name = Fields.ShadowOpacity;
            shadowOpacityDropDown.SetupAdapter(Shadow.OpacityCollection.Select(item => item.Key).ToList());
            shadowOpacityDropDown.ItemSelected += (position) =>
            {
                if (position > 0)
                {
                    _shadowAlpha = (float)Shadow.OpacityCollection.ElementAt(position).Value;
                    ChangeShadow(fab);
                }
            };

            stateSwitch.CheckedChange += (sender, e) =>
            {
                fab.Enabled = stateSwitch.Checked;
            };

            resetButton.Click += delegate
            {
                if (_fabInitialSize != 0)
                {
                    ChangeFabLayoutParameters(_fabInitialSize, fab);
                }
                ResetCustomization(fab, themeDropDown, spinners);
                ResetShadowFields(shadowOffsetXDropDown, shadowOffsetYDropDown, shadowBlurDropDown, shadowColorDropDown);
            };
        }

        private void SetToolbar()
        {
            _toolbar = FindViewById<EOSSandboxToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(_toolbar);
        }

        private void ChangeShadow(FabProgress fab)
        {
            fab.ShadowConfig = CreateShadowConfig(fab);
            fab.StopProgressAnimation();
        }

        private ShadowConfig CreateShadowConfig(FabProgress fab)
        {
            Color resultColor;
            if (_shadowAlpha == 1)
            {
                resultColor = fab.ShadowConfig.Color;
                resultColor.A = 255;
            }
            else
            {
                resultColor = fab.ShadowConfig.Color;
                resultColor.A = (byte)(255 * _shadowAlpha);
            }
            return new ShadowConfig
            {
                Offset = fab.ShadowConfig.Offset,
                Blur = fab.ShadowConfig.Blur,
                Color = resultColor
            };
        }

        private void ResetShadowFields(EOSSandboxDropDown shadowOffsetX, EOSSandboxDropDown shadowOffsetY, EOSSandboxDropDown shadowBlur, EOSSandboxDropDown shadowColorDropDown)
        {
            shadowColorDropDown.SetSpinnerSelection(0);
            shadowOffsetX.SetSpinnerSelection(0);
            shadowOffsetY.SetSpinnerSelection(0);
            shadowBlur.SetSpinnerSelection(0);
        }

        private void ChangeFabLayoutParameters(int width, FabProgress fab)
        {
            fab.ShadowConfig = null;
            fab.SetLayoutParameters(width, width);
        }

        private void ResetCustomization(FabProgress fab, EOSSandboxDropDown themeDropDown, List<EOSSandboxDropDown> spinners)
        {
            spinners.Except(new[] { themeDropDown }).ToList().ForEach(s => s.SetSpinnerSelection(0));
            fab.ResetCustomization();
        }
    }
}