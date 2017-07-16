﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommonFiles.TransferObjects;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.Commands
{
    public class PressPushButtonCommand : ICommand
    {
        //private UserControlsViewModel ucViewModel;
        //private DetectTabViewModel dtViewModel;
        private RemoteControllerViewModel remoteVM;
        private DebugViewModel debugViewModel;

        public PressPushButtonCommand()
        {
            debugViewModel = MainWindowViewModel.CurrentViewModelDebug;
            remoteVM = MainWindowViewModel.CurrentViewModelRemoteController;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (remoteVM.getDuration() != -1)
            {
                remoteVM.sendRequest(parameter as Request);
                remoteVM.getResult(parameter as Request);
            }
            else
            {
                debugViewModel.AddDebugInfo("Debug", "Invalid duration");
            }
        }
    }
}
