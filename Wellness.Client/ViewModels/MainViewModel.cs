using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.ViewModels
{
    public class MainViewModel : IViewModelBase
    {
        public User CurrentUser { get; private set; }
        public string SearchTerm { get; set; }

        public IEnumerable<PersistenceWrapper<User>> SearchResults { get; set; }

        private IProfileService _profileService;
        private IClientState _clientState;

        public MainViewModel(IProfileService profileService, IClientState clientState)
        {
            _profileService = profileService;
            _clientState = clientState;
        }

        public async Task OnInit()
        {
            CurrentUser = _clientState.CurrentUser;
            SearchResults = Enumerable.Empty<PersistenceWrapper<User>>();
        }

        public async Task Search()
        {
            SearchResults = await _profileService.Find(SearchTerm, CancellationToken.None);
        }
    }
}
