using HyPlayer.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyPlayer.Services
{
    class ShellService : IShellService
    {
        public bool IsProgressBarVisible { get; set; }
    }
}
