using Markdig;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
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

    }
}
