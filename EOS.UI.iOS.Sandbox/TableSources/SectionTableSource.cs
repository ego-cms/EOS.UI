using System;
using System.Collections.Generic;
using System.Linq;
using EOS.UI.iOS.Components;
using EOS.UI.iOS.Models;
using Foundation;
using UIKit;

namespace EOS.UI.iOS.Sandbox.TableSources
{
    public class SectionTableSource : UITableViewSource
    {
        private UITableView _tableView;
        private const string _cellIdentifier = "SectionTableViewCell";
        private List<string> _source;
        public SectionModel SectionModel { get; set; }


        public SectionTableSource(UITableView table, List<object> dataSource)
        {
            SectionModel = dataSource.FirstOrDefault(item => item is SectionModel) as SectionModel;
            _source = dataSource.FindAll(item => item is string).Select(item => (string)item).ToList();

            _tableView = table;
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

        //public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        //{
        //    return 60 + SectionModel.TopPadding + SectionModel.BottonPadding;
        //}

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            var header = _tableView.DequeueReusableHeaderFooterView("Section");

            var customSectoion = (header as Section);

            customSectoion.Initialize();

            if(SectionModel.ResetCustomization)
            {
                customSectoion.ResetCustomization();
                SectionModel.ResetCustomization = false;
            }
            else
            {
                customSectoion.HasBorder = SectionModel.HasBorder;
                customSectoion.HasButton = SectionModel.HasButton;
                customSectoion.SectionAction = SectionModel.SectionAction;

                if(!string.IsNullOrEmpty(SectionModel.SectionName))
                    customSectoion.SectionName = SectionModel.SectionName;

                if(!string.IsNullOrEmpty(SectionModel.ButtonText))
                    customSectoion.ButtonText = SectionModel.ButtonText;

                if(SectionModel.SectionNameTextSize != 0)
                    customSectoion.SectionTextSize = SectionModel.SectionNameTextSize;

                if(SectionModel.ButtonTextTextSize != 0)
                    customSectoion.ButtonTextSize = SectionModel.ButtonTextTextSize;

                if(SectionModel.SectionTextLetterSpacing != 0)
                    customSectoion.SectionTextLetterSpacing = SectionModel.SectionTextLetterSpacing;

                if(SectionModel.ButtonTextLetterSpacing != 0)
                    customSectoion.ButtonTextLetterSpacing = SectionModel.ButtonTextLetterSpacing;

                if(SectionModel.SectionNameFont != null)
                    customSectoion.SectionNameFont = SectionModel.SectionNameFont;

                if(SectionModel.ButtonNameFont != null)
                    customSectoion.ButtonNameFont = SectionModel.ButtonNameFont;

                if(SectionModel.BackgroundColor != UIColor.Clear)
                    customSectoion.BackgroundColor = SectionModel.BackgroundColor;

                if(SectionModel.BorderWidth != 0)
                    customSectoion.BorderWidth = SectionModel.BorderWidth;

                if(SectionModel.BorderColor != UIColor.Clear)
                    customSectoion.BorderColor = SectionModel.BorderColor;

                if(SectionModel.SectionNameColor != UIColor.Clear)
                    customSectoion.SectionNameColor = SectionModel.SectionNameColor;

                if(SectionModel.ButtonNameColor != UIColor.Clear)
                    customSectoion.ButtonNameColor = SectionModel.ButtonNameColor;

                customSectoion.SetPaddings(SectionModel.LeftPadding, SectionModel.TopPadding, SectionModel.RightPadding, SectionModel.BottonPadding);
            }
            return header;
        }
    }
}
