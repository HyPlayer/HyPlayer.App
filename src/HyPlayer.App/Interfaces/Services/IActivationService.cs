using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyPlayer.Interfaces.Services
{
    interface IActivationService
    {
        Task OnLaunchedAsync(object ActivateEventArgs);
    }
}
