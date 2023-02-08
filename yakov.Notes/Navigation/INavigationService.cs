using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yakov.Notes.Domain.Entities;

namespace yakov.Notes.Navigation
{
    public interface INavigationService
    {
        Task NavigateToLoginPage();
        Task NavigateToRegisterPage();
        Task NavigateToMainPage();
        Task NavigateToNotePage(Note note = null);

        Task NavigateBack();
    }
}
