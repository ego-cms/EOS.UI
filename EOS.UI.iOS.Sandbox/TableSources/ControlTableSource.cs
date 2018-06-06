using System;
using System.Linq;
using EOS.UI.iOS.Sandbox.Controls;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Foundation;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using UIKit;

namespace EOS.UI.iOS.Sandbox.TableSources
{
    public class ControlTableSource : UITableViewSource, IEOSThemeControl
    {
        private UIColor _textColor;
        private UIColor _backgroundColor;

        private UITableView _tableView;
        private const string _cellIdentifier = "ControlTableViewCell";
        public event EventHandler<NSIndexPath> Selected;

        public ControlTableSource(UITableView table)
        {
            _tableView = table;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(_cellIdentifier, indexPath);
            cell.TextLabel.Text = ControlsData.Instance.Names.ElementAt(indexPath.Row).Key;
            if(_textColor != null)
                cell.TextLabel.TextColor = _textColor;
            if(_backgroundColor != null)
                cell.BackgroundColor = _backgroundColor;
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return ControlsData.Instance.Names.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            Selected?.Invoke(this, indexPath);
        }

        #region IEOSThemeControl implementation

        public bool IsEOSCustomizationIgnored { get; set; }

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
            if(!IsEOSCustomizationIgnored)
            {
                _backgroundColor = GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor6);
                _textColor = GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor1);
            }
        }

        #endregion

    }
}
