using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wellness.Client.Components.Shared
{
    public class WellnessMatTabGroupComponent : BaseMatDomComponent
    {
        public WellnessMatTabGroupComponent()
        {
            ClassMapper.Add("mat-tab-group");
        }

        private BaseMatTabLabel _active;
        private int _activeIndex;
        private bool _firstTime = true;

        [Parameter]
        public int DefaultIndex { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public int ActiveIndex
        {
            get => _activeIndex;
            set
            {
                if (_activeIndex == value && !_firstTime)
                {
                    return;
                }

                _activeIndex = value;

                if (_firstTime && _activeIndex != DefaultIndex)
                {
                    _firstTime = false;
                    _activeIndex = DefaultIndex;
                }

                this.ActiveIndexChanged.InvokeAsync(_activeIndex).GetAwaiter().GetResult();
            }
        }
        [Parameter]
        public EventCallback<int> ActiveIndexChanged { get; set; }
        public BaseMatTabLabel Active
        {
            get => _active;
            set
            {
                _active = value;
                this.InvokeStateHasChanged();
            }
        }
    }
}
