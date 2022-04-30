using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Goat.View.Exceptions;

namespace Goat.View.Services
{
    public interface IUIStringsService
    {
        string StartGameButtonText { get; }
    }

    public class UIStringsService : IUIStringsService
    {
        private static readonly ResourceSet _resources;

        static UIStringsService()
        {
            _resources = new ResourceSet("UIStrings");
        }

        public string StartGameButtonText => 
            _resources.GetString("") ?? throw new ResourceNotFoundException(nameof(StartGameButtonText));
    }
}
