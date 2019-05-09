using PokeQuizz.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PokeQuizz.Services.Interfaces
{
    public interface IContacts
    {
        Task<List<Contact>> GetDeviceContactsAsync();
    }
}
