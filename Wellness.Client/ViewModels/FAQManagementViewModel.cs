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

        public async Task Edit(FrequentlyAskedQuestion faq)
        {
            NewOrEditFAQ = faq;
            EditModalOpen = true;
        }

        public async Task Save()
        {
            await _frequentlyAskedQuestionService.Create(NewOrEditFAQ);
            FAQs = await _frequentlyAskedQuestionService.GetAll();
            NewOrEditFAQ = new FrequentlyAskedQuestion();
            EditModalOpen = false;
        }

        public async Task FileAttached(List<EventAttachmentArgs> args)
        {
            if(NewOrEditFAQ.Images == default)
            {
                NewOrEditFAQ.Images = new List<EventAttachment>();
            }

            foreach (var arg in args)
            {
                var eventAttachmentFileLocation = await _frequentlyAskedQuestionService.UploadFile(arg.Name, arg.Type, arg.WriteToStreamAsync);
                NewOrEditFAQ.Images.Add(new EventAttachment()
                {
                    ContentType = arg.Type,
                    FilePath = eventAttachmentFileLocation,
                    FileSize = arg.Size,
                    Name = arg.Name
                });
            }
        }

    }
}
