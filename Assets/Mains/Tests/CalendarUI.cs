using UnityEngine;
using UnityEngine.UIElements;
using System;

public class CalendarUI : MonoBehaviour
{
    public UIDocument uiDocument;

    private void Start()
    {
        // Root UI element
        VisualElement root = uiDocument.rootVisualElement;

        // Create Calendar container
        VisualElement calendarContainer = new VisualElement();
        calendarContainer.style.flexDirection = FlexDirection.Column;
        root.Add(calendarContainer);

        // Generate Month View
        GenerateMonth(calendarContainer, DateTime.Now.Year, DateTime.Now.Month);
    }

    private void GenerateMonth(VisualElement parent, int year, int month)
    {
        // Add month header
        Label monthLabel = new Label(new DateTime(year, month, 1).ToString("MMMM yyyy"));
        monthLabel.style.unityFontStyleAndWeight = FontStyle.Bold;
        parent.Add(monthLabel);

        // Create grid container
        VisualElement grid = new VisualElement();
        grid.style.flexWrap = Wrap.Wrap;
        grid.style.flexDirection = FlexDirection.Row;
        grid.style.justifyContent = Justify.Center;
        parent.Add(grid);

        // Get first day & total days in the month
        int daysInMonth = DateTime.DaysInMonth(year, month);
        DateTime firstDay = new DateTime(year, month, 1);

        // Generate day cells
        for (int i = 1; i <= daysInMonth; i++)
        {
            Label dayLabel = new Label(i.ToString());
            dayLabel.style.width = 40;
            dayLabel.style.height = 40;
            dayLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
            dayLabel.style.borderBottomWidth = 1;
            dayLabel.style.borderRightWidth = 1;
            dayLabel.style.borderLeftWidth = 1;
            dayLabel.style.borderTopWidth = 1;

            grid.Add(dayLabel);
        }
    }
}