using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.IO;
using System.Threading.Tasks;
using Wellness.Client.ViewModels;
using Wellness.Model;


namespace Wellness.Client.Components.UserParticipation.Events.Add
{
    public class NewEventComponent : ComponentBase
    {
        [Inject] IJSRuntime JSRuntime { get; set; }
        [Parameter] public IEventParticipationViewModel ViewModel { get; set; }
        [Parameter] public EventCallback<EventAttachmentArgs> OnFileAttached { get; set; }
        [Parameter] public EventCallback OnEventSubmissionCompleted { get; set; }

        public async Task FilesReady(IMatFileUploadEntry[] files)
        {
            foreach (var file in files)
            {
                EventAttachmentArgs args = new EventAttachmentArgs
                {
                    LastModified = file.LastModified,
                    Name = file.Name,
                    Size = file.Size,
                    Type = file.Type,
                    WriteToStreamAsync = (async (Stream s) =>
                    {
                        await file.WriteToStreamAsync(s);
                    })
                };
                await OnFileAttached.InvokeAsync(args);
            }
        }
    }
}
