using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    public class MonthCalendarViewUI : VisualElement
    {
        private const string _rootClass = "month-calendar-view";
        private const string _monthFieldClass = _rootClass + "__month-field";
        private const string _monthTextClass = _rootClass + "__month-text";
        private const string _previousMonthButtonClass = _rootClass + "__previous-month-button";
        private const string _nextMonthButtonClass = _rootClass + "__next-month-button";

        private VisualElement _monthField;
        private Label _monthText;
        private VisualElement _previousMonthButton;
        private VisualElement _nextMonthButton;

        public MonthCalendarViewUI()
        {
            this.AddStyle(Main.Resources.Styles["StyleVariableUI"]);
            this.AddStyle(Main.Resources.Styles["MonthCalendarViewUI"]);
            this.AddClass(_rootClass);

            _monthField = new VisualElement().AddClass(_monthFieldClass);

            _monthText = new Label("October, 2023").AddClass(_monthTextClass);
            _monthField.AddElements(_monthText);

            _previousMonthButton = new VisualElement().AddClass(_previousMonthButtonClass);
            _monthField.AddElements(_previousMonthButton);

            _nextMonthButton = new VisualElement().AddClass(_nextMonthButtonClass);
            _monthField.AddElements(_nextMonthButton);

            this.AddElements(_monthField);

            this.AddElements(new MonthCalendarRowUI().SetAsWeekday());

            var days = GenerateMonthDays(2025, 5);
            FillCalendar(days);
        }

        private List<(string, bool)> GenerateMonthDays(int year, int month)
        {
            List<(string, bool)> days = new List<(string, bool)>();

            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int startDayOfWeek = (int)firstDayOfMonth.DayOfWeek;

            int previousMonth = month == 1 ? 12 : month - 1;
            int previousMonthYear = month == 1 ? year - 1 : year;
            int daysInPreviousMonth = DateTime.DaysInMonth(previousMonthYear, previousMonth);

            for (int i = startDayOfWeek; i > 0; i--)
            {
                days.Add(((daysInPreviousMonth - i + 1).ToString(), false));
            }

            for (int day = 1; day <= daysInMonth; day++)
            {
                days.Add((day.ToString(), true));
            }

            int nextMonth = month == 12 ? 1 : month + 1;
            int nextMonthYear = month == 12 ? year + 1 : year;

            while (days.Count % 7 != 0) 
            {
                days.Add(((days.Count - daysInMonth - startDayOfWeek + 1).ToString(), false));
            }

            return days;
        }

        private void FillCalendar(List<(string, bool)> days)
        {
            int totalCells = ((days.Count + 6) / 7) * 7;

            for (int i = 0; i < totalCells; i += 7)
            {
                var rowItems = new (string, bool)[7];
                for (int j = 0; j < 7; j++)
                {
                    rowItems[j] = (i + j < days.Count) ? days[i + j] : ("", false);
                }

                this.AddElements(new MonthCalendarRowUI().SetAsMonthday(rowItems));
            }
        }
    }
}