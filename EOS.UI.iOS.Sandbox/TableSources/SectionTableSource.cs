using System;
using System.Collections.Generic;
using System.Linq;
using EOS.UI.iOS.Components;
using EOS.UI.iOS.Sandbox.Controls;
using Foundation;
using UIKit;

namespace EOS.UI.iOS.Sandbox.TableSources
{
    public class SectionTableSource : UITableViewSource
    {
        private UITableView _tableView;
        private const string _cellIdentifier = "SectionTableViewCell";
        private List<string> _source = new List<string>()
        {
            "First item",
            "Second item",
            "Third item"
        };

        public SectionTableSource(UITableView table)
        {
            _tableView = table;

            //_tableView.ReloadData();
            _tableView.RegisterNibForHeaderFooterViewReuse(Section.Nib, Section.Key);
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(_cellIdentifier, indexPath);
            cell.TextLabel.Text = _source[indexPath.Row];
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _source.Count;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            var header = _tableView.DequeueReusableHeaderFooterView("Section");
            (header as Section).Initialize();
            return header;
        }
    }
}
