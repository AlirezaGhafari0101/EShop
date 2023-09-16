using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Implementation
{
    public class ContactUsService : IContactUsService
    {
        private readonly IContactUsRepository _contactUsRepository;
        public ContactUsService(IContactUsRepository contactUsRepository)
        {
            _contactUsRepository = contactUsRepository;
        }
        public async  Task CreateQuestionServiceAsync(ContactUsViewModel contactUsViewModel)
        {
            ContactUs contactUs = new ContactUs()
            {
                FullName = contactUsViewModel.FullName,
                Email = contactUsViewModel.Email,
                Message = contactUsViewModel.Message,
            };


           await _contactUsRepository.CreateQuestionAsync(contactUs);
            await _contactUsRepository.SaveChangeAsync();


           



        }
    }
}
