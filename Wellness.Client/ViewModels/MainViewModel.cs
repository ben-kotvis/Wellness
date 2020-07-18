using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.ViewModels
{
    public class MainViewModel : IViewModelBase
    {
        public User CurrentUser { get; private set; }
        public string SearchTerm { get; set; }

        public IEnumerable<User> SearchResults { get; set; }

        private IProfileService _profileService;

        public MainViewModel(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task OnInit()
        {
            CurrentUser = await _profileService.GetCurrent();
            SearchResults = Enumerable.Empty<User>();
        }

        public async Task Search()
        {
            SearchResults = await _profileService.Find(SearchTerm);
        }
    }
}
