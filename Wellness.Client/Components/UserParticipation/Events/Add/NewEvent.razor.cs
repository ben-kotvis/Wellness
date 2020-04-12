using MatBlazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Client.ViewModels;
using Wellness.Model;

namespace Wellness.Client.Components.UserParticipation.Events.Add
{
    public class NewEventComponent : ComponentBase
    {
        [Parameter] public IEventParticipationViewModel ViewModel { get; set; }
        [Parameter] public EventCallback OnSaveSelected { get; set; }
        [Parameter] public IEnumerable<Event> Events { get; set; }
        [Parameter] public EventCallback<EventAttachmentArgs> OnFileAttached { get; set; }

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
                        try
                        {
                            await file.WriteToStreamAsync(s);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    })
                };
                await OnFileAttached.InvokeAsync(args);
            }
        }
    }
}
