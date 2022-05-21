using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Goat.Application.ViewModels;

namespace Goat.Application.Interfaces
{
    public interface INavigation
    {
        Task Navigate<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateModal<TViewModel>() where TViewModel : ViewModelBase;

        Task Back();
    }
}
