using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using EOS.UI.Android.Controls;
using EOS.UI.Android.Helpers;
using EOS.UI.Android.Sandbox.Controls;
using EOS.UI.Shared.Helpers;
using EOS.UI.Shared.Themes.Themes;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using static EOS.UI.Android.Sandbox.Helpers.Constants;

namespace EOS.UI.Android.Sandbox.Activities
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
        int _shadowOffsetX; 
        int _shadowOffsetY;
        int _shadowBlur;
        float _shadowAlpha = 1.0f;
        Color _shadowColor;

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
            //var shadowDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.shadowDropDown);
            var shadowOffsetX = FindViewById<EOSSandboxEditText>(Resource.Id.shadowOffsetX);
            var shadowOffsetY = FindViewById<EOSSandboxEditText>(Resource.Id.shadowOffsetY);
            var shadowBlur = FindViewById<EOSSandboxEditText>(Resource.Id.shadowBlur);
            var shadowColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.shadowColorDropDown);
            var shadowOpacityDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.shadowOpacityDropDown);

            var sizeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.sizeDropDown);
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
                //shadowDropDown,
                shadowColorDropDown,
                shadowOpacityDropDown,
                sizeDropDown
            };

            themeDropDown.Name = Fields.Theme;
            themeDropDown.SetupAdapter(ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            themeDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                {
                    fab.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
                    fab.ResetCustomization();
                    spinners.Except(new[] { themeDropDown }).ToList().ForEach(s => s.SetSpinnerSelection(0));
                    UpdateApperaence();
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

            //shadowDropDown.Name = Fields.Shadow;
            //shadowDropDown.SetupAdapter(Shadows.ShadowsCollection.Select(i => i.Key).ToList());
            //shadowDropDown.ItemSelected += (position) =>
            //{
            //    if (position > 0)
            //    {
            //        if (fab.ShadowConfig != null)
            //        {
            //            fab.ShadowConfig = null;
            //        }
            //        fab.ShadowConfig = Shadows.ShadowsCollection.ElementAt(position).Value;
            //        fab.StopProgressAnimation();
            //    }
            //};

            shadowColorDropDown.Name = Fields.ShadowColor;
            shadowColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(i => i.Key).ToList());
            shadowColorDropDown.ItemSelected += (position) =>
            {
                if (position > 0)
                {
                    if (fab.ShadowConfig != null)
                    {
                        fab.ShadowConfig = null;
                    }
                    _shadowColor = Colors.ColorsCollection.ElementAt(position).Value;
                    fab.ShadowConfig = CreateShadowConfig();
                    fab.StopProgressAnimation();
                }
            };

            shadowOpacityDropDown.Name = Fields.ShadowOpacity;
            shadowOpacityDropDown.SetupAdapter(Android.Sandbox.Helpers.Constants.ShadowOpacityValues.ToList());
            shadowOpacityDropDown.ItemSelected += (position) =>
            {
                if (position > 0)
                {
                    if (fab.ShadowConfig != null)
                    {
                        fab.ShadowConfig = null;
                    }
                    _shadowAlpha = (float)Android.Sandbox.Helpers.Constants.ShadowOpacityValues.ElementAt(position);
                    fab.ShadowConfig = CreateShadowConfig();
                    fab.StopProgressAnimation();
                }
            };

            shadowOffsetX.TextChanged += (sender, e) =>
            {
                if (string.IsNullOrEmpty(e.Text.ToString()))
                {
                    _shadowOffsetX = 0;
                    ChangeShadow(fab);
                    return;
                }
                if (Int32.TryParse(e.Text.ToString(), out _shadowOffsetX))
                {
                    ChangeShadow(fab);
                }
            };

            shadowOffsetY.TextChanged += (sender, e) =>
            {
                if (string.IsNullOrEmpty(e.Text.ToString()))
                {
                    _shadowOffsetY = 0;
                    ChangeShadow(fab);
                    return;
                }

                if (Int32.TryParse(e.Text.ToString(), out _shadowOffsetY))
                {
                    ChangeShadow(fab);
                }
            };


            shadowBlur.TextChanged += (sender, e) =>
            {
                if (string.IsNullOrEmpty(e.Text.ToString()))
                {
                    _shadowBlur = 0;
                    ChangeShadow(fab);
                    return;
                }
                
                if (Int32.TryParse(e.Text.ToString(), out _shadowBlur))
                {
                    ChangeShadow(fab);
                }
            };

            sizeDropDown.Name = Fields.Size;
            sizeDropDown.SetupAdapter(Sizes.FabProgressSizes.Select(i => i.Key).ToList());
            sizeDropDown.ItemSelected += (position) =>
            {
                if (_fabInitialSize == 0)
                    _fabInitialSize = fab.Width;

                if (position > 0)
                {
                    ChangeFabLayoutParameters(Sizes.FabProgressSizes.ElementAt(position).Value, fab);
                    ResetShadowFields(shadowOffsetX, shadowOffsetY, shadowBlur, shadowColorDropDown);
                    //shadowDropDown.SetSpinnerSelection(0);
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
                ResetShadowFields(shadowOffsetX, shadowOffsetY, shadowBlur, shadowColorDropDown);
            };
        }

        private void SetToolbar()
        {
            _toolbar = FindViewById<EOSSandboxToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(_toolbar);
        }

        private void ChangeShadow(FabProgress fab)
        {
            if (fab.ShadowConfig != null)
            {
                fab.ShadowConfig = null;
            }

            fab.ShadowConfig = CreateShadowConfig();
            fab.StopProgressAnimation();
        }

        private ShadowConfig CreateShadowConfig()
        {
            Color resultColor;
            if (_shadowAlpha == 1)
            {
                resultColor = _shadowColor;
            }
            else
            {
                resultColor = _shadowColor;
                resultColor.A = (byte)(255 * _shadowAlpha);
            }
            return new ShadowConfig
            {
                Offset = new Point(_shadowOffsetX, _shadowOffsetY),
                Blur = _shadowBlur,
                Color = resultColor
            };
        }

        private void ResetShadowFields(EOSSandboxEditText shadowOffsetX, EOSSandboxEditText shadowOffsetY, EOSSandboxEditText shadowBlur, EOSSandboxDropDown shadowColorDropDown)
        {
            shadowColorDropDown.SetSpinnerSelection(0);
            shadowOffsetX.Text = "";
            shadowOffsetY.Text = "";
            shadowBlur.Text = "";
            _shadowOffsetX = 0;
            _shadowOffsetY = 0;
            _shadowBlur = 0;
            _shadowColor = Color.Transparent;
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

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.FabProgressMenu, menu);
            ChangeMenuItemColor(menu);
            return true;
        }

        private void ChangeMenuItemColor(IMenu menu)
        {
            var item = _toolbar.Menu.FindItem(Resource.Id.fab_progress_menu_new);

            if (item == null)
                return;

            item.Icon.SetColorFilter(EOSThemeProvider.Instance.GetEOSProperty<Color>(_toolbar, EOSConstants.BrandPrimaryColor), PorterDuff.Mode.SrcIn);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.fab_progress_menu_new:
                    var intent = new Intent(this, typeof(FabProgressSampleActivity));
                    intent.PutExtra(ShadowOffsetXKey, _shadowOffsetX);
                    intent.PutExtra(ShadowOffsetYKey, _shadowOffsetY);
                    intent.PutExtra(ShadowBlurKey, _shadowBlur);
                    intent.PutExtra(ShadowColorAKey, (byte)(255 * _shadowAlpha));
                    intent.PutExtra(ShadowColorRKey, _shadowColor.R);
                    intent.PutExtra(ShadowColorGKey, _shadowColor.G);
                    intent.PutExtra(ShadowColorBKey, _shadowColor.B);
                    StartActivity(intent);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}