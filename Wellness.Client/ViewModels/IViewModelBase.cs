using System.Threading.Tasks;

namespace Wellness.Client.ViewModels
{
    public interface IViewModelBase
    {
        Task OnInit();
    }
}
