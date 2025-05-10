using System;
using UnityEngine.UIElements;
using YNL.Utilities.UIToolkits;

namespace YNL.Checkotel
{
    public partial class SearchingTimePickingPageUI
    {
        public class HourlyPage : IInitializable<VisualElement, HourlyPage>
        {
            private VisualElement _hourlyPage;
            private ScrollView _checkInTimeScroll;
            private ScrollView _durationScroll;
            private VisualElement _calendarView;

            public HourlyPage Initialize(VisualElement root)
            {
                _hourlyPage = root.Q("ContentPage").Q("HourlyPage");

                _checkInTimeScroll = _hourlyPage.Q("CheckInTimeView").Q("TimeScroll") as ScrollView;
                _checkInTimeScroll.Clear();

                var nowTime = DateTime.Now;

                for (int i = nowTime.Hour + 1; i < 24; i++)
                {
                    bool isSelected = i == nowTime.Hour + 1;
                    var timeButton = new TimePointButtonUI($"{i.ToString("00")}:00", isSelected, true);
                    _checkInTimeScroll.Add(timeButton);
                }

                _durationScroll = _hourlyPage.Q("DurationView").Q("TimeScroll") as ScrollView;
                _durationScroll.Clear();

                for (int i = 23; i > nowTime.Hour; i--)
                {
                    bool isSelected = i == 23;
                    var timeButton = new TimePointButtonUI($"{24 - i} hours", isSelected, false);
                    _durationScroll.Add(timeButton);
                }

                _calendarView = _hourlyPage.Q("CalendarView");
                _hourlyPage.Remove(_calendarView);

                _hourlyPage.Insert(0, new MonthCalendarViewUI());

                return this;
            }
        }
    }
}