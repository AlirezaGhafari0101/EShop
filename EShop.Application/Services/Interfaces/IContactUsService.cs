
using EShop.Application.ViewModels.ContactUs;
using EShop.Domain.Models.Home;

namespace EShop.Application.Services.Interfaces
{
    public interface IContactUsService
    {
        Task CreateQuestionServiceAsync(CreateContactUsViewModel createContactUsViewModel);


        #region Admin
        Task<IEnumerable<ContactUsViewModel>> GetAllContactUsServiceAsync();
        Task DeleteContactUsServiceAsyc(int id);
        Task<SendAnswerContactUsViewModel> GetContaUsInfoForSendAnswer(int id);
        Task<EmailSendAnswerContactUsViewModel> UpdateContactUsServiceAsync(SendAnswerContactUsViewModel sendAnswerContactUsViewModelk,int id);
        #endregion
    }
}
