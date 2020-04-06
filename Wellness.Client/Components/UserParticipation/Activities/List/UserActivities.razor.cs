using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.Components.UserParticipation.Activities.List
{
    public class UserActivitiesComponent : ComponentBase
    {

        [Inject] AppState State { get; set; }

        [Parameter]
        public List<ActivityParticipation> ActivityParticipations { get; set; }



        private string SelectedValue => SelectedId == default ? default : State.Activities.First(i => i.Id == SelectedId).Name;

        private Guid SelectedId { set; get; } = default;

        public BaseMatIconButton activityItemButton;
        public BaseMatMenu activityItemMenu;

        public void OnClick(MouseEventArgs e)
        {
            this.activityItemMenu.OpenAsync(activityItemButton.Ref);
        }

        private void UpdateSelectedIndex(Guid id)
        {
            SelectedId = id;

            Open = false;

            StateHasChanged();
        }

        private void HandleClose(EventArgs args)
        {
            Open = false;

            StateHasChanged();
        }

        private void HandleClickListItem()
        {
            Open = true;

            StateHasChanged();
        }

        private void UpdateSelected(string id)
        {

        }

        bool Open = false;
        private object Value { set; get; } = 0;

        private void HandleChange(object value)
        {
            Value = value;

            StateHasChanged();
        }

    }
}
