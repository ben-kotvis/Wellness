using System;
using System.Collections.Generic;
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
        public Guid DialogId { get; set; } = Guid.Empty;

        public async Task OnInit()
        {
            FAQs = await _frequentlyAskedQuestionService.GetAll();
            NewOrEditFAQ = new FrequentlyAskedQuestion()
            {
                Active = true
            };
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
            if (DialogId == default)
            {
                NewOrEditFAQ.Id = Guid.NewGuid();
                await _frequentlyAskedQuestionService.Create(NewOrEditFAQ);
            }
            else
            {
                await _frequentlyAskedQuestionService.Update(NewOrEditFAQ);
            }

            FAQs = await _frequentlyAskedQuestionService.GetAll();
            NewOrEditFAQ = new FrequentlyAskedQuestion();
            EditModalOpen = false;
        }

        public async Task FileAttached(List<EventAttachmentArgs> args)
        {
            if (NewOrEditFAQ.Images == default)
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
