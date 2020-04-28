using MatBlazor;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.ViewModels
{    
    public class FAQManagementViewModel : IViewModelBase
    {
        private IFrequentlyAskedQuestionService _frequentlyAskedQuestionService;
        
        public FAQManagementViewModel(IFrequentlyAskedQuestionService frequentlyAskedQuestionService)
        {
            _frequentlyAskedQuestionService = frequentlyAskedQuestionService;
        }

        public IEnumerable<PersistenceWrapper<FrequentlyAskedQuestion>> FAQs { get; set; }
        public FrequentlyAskedQuestion NewOrEditFAQ { get; set; }
        public bool EditModalOpen { get; set; }

        public async Task OnInit()
        {
            FAQs = await _frequentlyAskedQuestionService.GetAll();
            NewOrEditFAQ = new FrequentlyAskedQuestion();
        }

        public async Task New()
        {
            EditModalOpen = true;
        }

        public async Task Save()
        {
            await _frequentlyAskedQuestionService.Create(NewOrEditFAQ);
            FAQs = await _frequentlyAskedQuestionService.GetAll();
            NewOrEditFAQ = new FrequentlyAskedQuestion();
            EditModalOpen = false;
        }

    }
}
