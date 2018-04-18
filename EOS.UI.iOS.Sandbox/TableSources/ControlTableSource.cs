using System;
using System.Linq;
using EOS.UI.iOS.Sandbox.Controls;
using Foundation;
using UIKit;

namespace EOS.UI.iOS.Sandbox.TableSources
{
    public class ControlTableSource : UITableViewSource
    {
        private UITableView _tableView;
        private const string _cellIdentifier = "ControlTableViewCell";
        public event EventHandler<NSIndexPath> Selected;

        public ControlTableSource(UITableView table)
        {
            _tableView = table;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (UITableViewCell)tableView.DequeueReusableCell(_cellIdentifier, indexPath);
            cell.TextLabel.Text = ControlsData.Instance.Names.ElementAt(indexPath.Row).Key;
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
    }
}
