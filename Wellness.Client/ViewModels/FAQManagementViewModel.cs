using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.ViewModels
{
    public class FAQManagementViewModel : IViewModelBase
    {
        private IFrequentlyAskedQuestionService _frequentlyAskedQuestionService;

        public string IconClass { get; set; } = "d-none";
        public bool IsSaving { get; set; } = false;
        public FAQManagementViewModel(IFrequentlyAskedQuestionService frequentlyAskedQuestionService)
        {
            _frequentlyAskedQuestionService = frequentlyAskedQuestionService; 
            
            NewOrEditFAQ = new FrequentlyAskedQuestion()
            {
                Active = true
            };
        }

        public IEnumerable<PersistenceWrapper<FrequentlyAskedQuestion>> FAQs { get; set; }
        public FrequentlyAskedQuestion NewOrEditFAQ { get; set; }
        public bool EditModalOpen { get; set; }
        public Guid DialogId { get; set; } = Guid.Empty;

        public async Task OnInit()
        {
            FAQs = await _frequentlyAskedQuestionService.GetAll(CancellationToken.None);
        }

        public async Task New()
        {
            EditModalOpen = true;
        }

        public async Task Edit(FrequentlyAskedQuestion faq)
        {
            DialogId = faq.Id;
            NewOrEditFAQ = faq;
            EditModalOpen = true;
        }

        public async Task Cancel()
        {
            NewOrEditFAQ = new FrequentlyAskedQuestion();
            EditModalOpen = false;
        }

        public async Task Save()
        {
            IsSaving = true;
            IconClass = "spinning-icon";
            if (DialogId == default)
            {
                NewOrEditFAQ.Id = Guid.NewGuid();
                await _frequentlyAskedQuestionService.Create(NewOrEditFAQ, CancellationToken.None);
            }
            else
            {
                await _frequentlyAskedQuestionService.Update(NewOrEditFAQ, CancellationToken.None);
            }

            FAQs = await _frequentlyAskedQuestionService.GetAll(CancellationToken.None);
            NewOrEditFAQ = new FrequentlyAskedQuestion();
            EditModalOpen = false;

            IsSaving = false;
            IconClass = "d-none";
        }

        public async Task FileAttached(List<EventAttachmentArgs> args)
        {
            if (NewOrEditFAQ.Images == default)
            {
                NewOrEditFAQ.Images = new List<EventAttachment>();
            }

            foreach (var arg in args)
            {
                var eventAttachmentFileLocation = await _frequentlyAskedQuestionService.UploadFile(arg.Name, arg.Type, arg.WriteToStreamAsync, CancellationToken.None);
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
