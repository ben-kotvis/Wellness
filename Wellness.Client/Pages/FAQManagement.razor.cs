using Markdig;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Client.Components;
using Wellness.Client.ViewModels;
using Wellness.Model;

namespace Wellness.Client.Pages
{
    public class FAQManagementComponent : WellnessComponentBase<FAQManagementViewModel>
    {
        [Inject] public override FAQManagementViewModel ViewModel { get; set; }
        [Inject] public MarkdownPipeline Pipeline { get; set; }

        public async Task FilesReady(IMatFileUploadEntry[] files)
        {
            var attachments = new List<EventAttachmentArgs>();

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
                attachments.Add(args);
            }

            await ViewModel.FileAttached(attachments);
        }


    }
}
