using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.Components.MonthsMenu
{
    public class MonthsMenuComponent : ComponentBase
    {

        public List<Month> Months;
        private int previousRelativeIndex = -5;
        private int nextRelativeIndex = 5;

        public int SelectedIndex { get; set; }

        [Parameter]
        public EventCallback<MonthChangedEventArgs> OnMonthChanged { get; set; }

        public async Task Previous(MouseEventArgs args)
        {
            var index = Months[0].RelativeIndex - 3;
            await ChangeMenu(index);
        }

        public async Task Next(MouseEventArgs args)
        {
            var index = Months[4].RelativeIndex + 3;
            await ChangeMenu(index);
        }

        public async Task MonthChanged(MouseEventArgs args, int index)
        {
            var today = DateTime.Now;
            var selectedMonth = today.AddMonths(index);
            await OnMonthChanged.InvokeAsync(new MonthChangedEventArgs(createMonth(selectedMonth.Month, index, selectedMonth.Year)));
        }

        private Month createMonth(int monthNumber, int relativeIndex, int year)
        {
            return new Month()
            {
                Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthNumber),
                Number = monthNumber,
                RelativeIndex = relativeIndex,
                Year = year
            };
        }

        protected override async Task OnInitializedAsync()
        {
            Months = new List<Month>();
            var today = DateTime.Now;
            var start = today.AddMonths(-2);

            for (var i = 0; i < 5; i++)
            {
                var current = start.AddMonths(i);

                Months.Add(createMonth(current.Month, (-2 + i), current.Year));
            }

            SelectedIndex = 0;

            await OnMonthChanged.InvokeAsync(new MonthChangedEventArgs(createMonth(today.Month, 0, today.Year)));
        }

        private async Task ChangeMenu(int relatveIndex)
        {
            Months = new List<Month>();
            var today = DateTime.Now;
            var start = today.AddMonths(relatveIndex - 2);

            for (var i = 0; i < 5; i++)
            {
                var current = start.AddMonths(i);
                Months.Add(createMonth(current.Month, (relatveIndex - 2) + i, current.Year));
            }

            previousRelativeIndex = relatveIndex - 5;
            nextRelativeIndex = relatveIndex + 5;

            var selectedMonth = today.AddMonths(relatveIndex);

            SelectedIndex = relatveIndex;

            await OnMonthChanged.InvokeAsync(new MonthChangedEventArgs(createMonth(selectedMonth.Month, relatveIndex, selectedMonth.Year)));
        }

    }
}
