using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Client.Services.Mock;
using Wellness.Model;

namespace Wellness.Client.ViewModels
{
    public class ActivityManagementViewModel : IViewModelBase
    {


        public string IconClass { get; set; } = "d-none";
        public bool IsSaving { get; set; } = false;

        public bool EditModalOpen { get; set; } = false;

        public bool DeleteDialogIsOpen { get; set; } = false;
        public Guid SelectedDeleteId { get; set; }

        public Activity NewOrEditActivity { get; set; }

        public bool Debug { get; set; } = false;
        public Guid DialogId { get; set; } = Guid.Empty;

        public IEnumerable<PersistenceWrapper<Activity>> Activities { get; private set; }

        public IEnumerable<string> MatIconNames { get; set; }

        private IActivityManagementService _activityManagementService;

        public ActivityManagementViewModel(IActivityManagementService activityManagementService)
        {
            _activityManagementService = activityManagementService;

            NewOrEditActivity = new Activity()
            {
                Active = true
            };

#if DEBUG
            Debug = true;
#endif
        }

        public async Task OnInit()
        {
            Activities = await _activityManagementService.GetAll(CancellationToken.None);
            MatIconNames = GetConstants(typeof(MatBlazor.MatIconNames));
        }

        private List<string> GetConstants(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Static)
                      .Where(f => f.PropertyType == typeof(string))
                      .Select(f => (string)f.GetValue(null)).ToList();
        }

        public void Delete(Guid id)
        {
            DeleteDialogIsOpen = true;
            SelectedDeleteId = id;
        }

        public async Task OnDeleteConfirmed(Guid id)
        {
            await _activityManagementService.Disable(id);
            Activities = await _activityManagementService.GetAll(CancellationToken.None);

            DeleteDialogIsOpen = false;
            SelectedDeleteId = default;
        }

        public void New()
        {
            DialogId = Guid.NewGuid();
            EditModalOpen = true;
        }

        public async Task Load()
        {
            foreach (var item in MockDataGenerator.GetActivities())
            {
                await _activityManagementService.Create(item.Model);
            }
        }

        public void Edit(Guid id)
        {
            var existingItem = Activities.FirstOrDefault(i => i.Model.Id == id);
            DialogId = id;

            NewOrEditActivity.Id = id;
            NewOrEditActivity.Name = existingItem.Model.Name;
            NewOrEditActivity.IconName = existingItem.Model.IconName;
            NewOrEditActivity.Active = existingItem.Model.Active;

            EditModalOpen = true;
        }

        public async Task Save()
        {
            IconClass = "spinning-icon";
            IsSaving = true;
            var activity = Activities.FirstOrDefault(i => i.Model.Id == DialogId)?.Model;

            if (activity == default)
            {
                NewOrEditActivity.Id = DialogId;
                await _activityManagementService.Create(NewOrEditActivity);
            }
            else
            {
                await _activityManagementService.Update(NewOrEditActivity);
            }

            Activities = await _activityManagementService.GetAll(CancellationToken.None);
            NewOrEditActivity = new Activity() { Active = true };
            IconClass = "d-none";
            EditModalOpen = false;
            IsSaving = false;
        }
    }
}
