using System.Collections.Generic;
using UnityEngine.UIElements;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    public class MonthCalendarRowUI : VisualElement
    {
        private const string _rootClass = "month-calendar-row";
        private const string _itemClass = _rootClass + "__item";
        private const string _itemTextClass = _rootClass + "__item-text";
        private const string _itemTextNotInMonthClass = _rootClass + "__item-text__not-in-month";

        public MonthCalendarRowUI()
        {
            this.AddStyle(Main.Resources.Styles["StyleVariableUI"]);
            this.AddStyle(Main.Resources.Styles["MonthCalendarRowUI"]);
            this.AddClass(_rootClass);
        }

        private void AddRowItems(params (string day, bool isInMonth)[] labels)
        {
            foreach (var label in labels)
            {
                var rowItem = new VisualElement().AddClass(_itemClass);

                var rowItemText = new Label(label.day).AddClass(_itemTextClass);
                rowItemText.EnableClass(!label.isInMonth, _itemTextNotInMonthClass);
                rowItem.AddElements(rowItemText);

                this.AddElements(rowItem);
            }
        }

        public MonthCalendarRowUI SetAsWeekday()
        {
            this.AddRowItems(("SUN", true), ("MON", true), ("TUE", true), ("WED", true), ("THU", true), ("FRI", true), ("SAT", true));
            this.style.marginBottom = 50;

            return this;
        }

        public MonthCalendarRowUI SetAsMonthday((string day, bool isInMonth)[] days)
        {
            this.AddRowItems(days);

            return this;
        }
    }
}