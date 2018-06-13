using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using EOS.UI.iOS.Components;
using EOS.UI.iOS.Models;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Extensions;
using Foundation;
using UIKit;

namespace EOS.UI.iOS.CollectionViewSources
{
    public class WorkTimeCalendarCollectionSource : UICollectionViewSource
    {
        private readonly UICollectionView _collectionView;

        public WorkTimeCalendarModel CalendarModel { get; }

        public WorkTimeCalendarCollectionSource(UICollectionView collectionView)
        {
            _collectionView = collectionView;
            _collectionView.RegisterNibForCell(WorkTimeCalendarCell.Nib, WorkTimeCalendarCell.Key);
            CalendarModel = new WorkTimeCalendarModel();
            CalendarModel.UpdateAppearance();
            CalendarModel.PropertyChanged += (sender, e) => this._collectionView.ReloadData();

            var layout = new WorkTimeCalendarFlowLayout();
            layout.ItemSize = new CGSize((_collectionView.Frame.Width - layout.SectionInset.Left - layout.SectionInset.Right) / 7,
                                         _collectionView.Frame.Height - layout.SectionInset.Top - layout.SectionInset.Bottom);
            _collectionView.CollectionViewLayout = layout;
        }


        public override nint NumberOfSections(UICollectionView collectionView)
        {
            return 1;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return CalendarModel.Items.Count();
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (WorkTimeCalendarCell)collectionView.DequeueReusableCell(WorkTimeCalendarCell.Key, indexPath);
            InitCell(ref cell, indexPath, CalendarModel.Items.ElementAt(indexPath.Row));
            return cell;
        }

        private void InitCell(ref WorkTimeCalendarCell cell, NSIndexPath indexPath, WorkTimeCalendarItem item)
        {
            cell.Init(item);
            cell.DayTextSize = CalendarModel.DayTextSize;
            cell.TitleTextSize = CalendarModel.TitleTextSize;
            cell.DayTextFont = CalendarModel.DayTextFont;
            cell.TitleFont = CalendarModel.TitleFont;

            if (item.WeekDay == DateTime.Now.DayOfWeek)
            {
                cell.CellBackgroundColor = CalendarModel.CurrentDayBackgroundColor;
                cell.DayTextColor = CalendarModel.CurrentDayTextColor;
                cell.TitleColor = CalendarModel.CurrentDayTextColor;
                cell.DividersColor = CalendarModel.CurrentColorDeviders;
            }
            else
            {
                cell.CellBackgroundColor = indexPath.Row % 2 == 0 ? UIColor.Clear : CalendarModel.DayEvenBackgroundColor;
                cell.DayTextColor = CalendarModel.DayTextColor;
                cell.TitleColor = CalendarModel.TitleColor;
                cell.DividersColor = CalendarModel.ColorDividers;
            }
        }
    }
}