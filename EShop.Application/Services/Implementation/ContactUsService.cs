using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels;
using EShop.Application.ViewModels.ContactUs;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Home;

namespace EShop.Application.Services.Implementation
{
    public class ContactUsService : IContactUsService
    {
        private readonly IContactUsRepository _contactUsRepository;
        public ContactUsService(IContactUsRepository contactUsRepository)
        {
            _contactUsRepository = contactUsRepository;
        }
        public async Task CreateQuestionServiceAsync(CreateContactUsViewModel createContactUsViewModel)
        {
            ContactUs contactUs = new ContactUs()
            {
                FullName = createContactUsViewModel.FullName,
                Email = createContactUsViewModel.Email,
                Message = createContactUsViewModel.Message,
            };
            await _contactUsRepository.CreateQuestionAsync(contactUs);
            await _contactUsRepository.SaveChangeAsync();

        }

        public async Task DeleteContactUsServiceAsyc(int id)
        {
            ContactUs contactUs = await _contactUsRepository.GetContactUsByIdAsync(id);

            await _contactUsRepository.DeleteContactUsAsync(contactUs);
            await _contactUsRepository.SaveChangeAsync();



        }

        public async Task<IEnumerable<ContactUsViewModel>> GetAllContactUsServiceAsync()
        {
            IEnumerable<ContactUs> contactUsList = await _contactUsRepository.GetAllContactUsAsync();
            return contactUsList.Select(contact => new ContactUsViewModel()
            {
                Id = contact.Id,
                FullName = contact.FullName,
                Email = contact.Email,
                Message = contact.Message,
                CreateDate = contact.CreateDate,

            }).ToList();


        }

        public async Task<SendAnswerContactUsViewModel> GetContaUsInfoForSendAnswer(int id)
        {
            ContactUs contactUs = await _contactUsRepository.GetContactUsByIdAsync(id);

            return new SendAnswerContactUsViewModel()
            {
                FullName = contactUs.FullName,
                Email = contactUs.Email,
                Message = contactUs.Message,

            };
        }

        public async Task<EmailSendAnswerContactUsViewModel> UpdateContactUsServiceAsync(SendAnswerContactUsViewModel sendAnswerContactUsViewModel, int id)
        {
            ContactUs contactUs = await _contactUsRepository.GetContactUsByIdAsync(id);

            contactUs.Answer = sendAnswerContactUsViewModel.Answer;
            contactUs.IsRead = true;
            await _contactUsRepository.UpdateContactUsAsync(contactUs);
            await _contactUsRepository.SaveChangeAsync();

            return new EmailSendAnswerContactUsViewModel()
            {
                FullName = contactUs.FullName,
                Email = contactUs.Email,
                Message = contactUs.Message,
                Answer = sendAnswerContactUsViewModel.Answer,
                CreateDate = contactUs.CreateDate,

            };
        }
    }
}
