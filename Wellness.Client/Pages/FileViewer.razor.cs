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
    public class FileViewerComponent : WellnessComponentBase<FileViewerViewModel>
    {
        [Inject] public override FileViewerViewModel ViewModel { get; set; }


    }
}
