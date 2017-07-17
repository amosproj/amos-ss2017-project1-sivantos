﻿using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.Commands
{
    public class EndlessVcCommand : ICommand
    {
        //private DetectTabViewModel dtViewModel;
        private DebugViewModel debugViewModel;
        private RemoteControllerViewModel remoteVM;

        public EndlessVcCommand()
        {
            //dtViewModel = MainWindowViewModel.CurrentViewModelDetectTab;
            debugViewModel = MainWindowViewModel.CurrentViewModelDebug;
            remoteVM = MainWindowViewModel.CurrentViewModelRemoteController;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object par)
        {
            remoteVM.sendRequest(par as Request);
            remoteVM.getResult(par as Request);

            if ((par as Request).command.Equals("EndlessVCUp"))
            {
                debugViewModel.AddDebugInfo("Endless_VC_Up", "+1");
            }
            else if((par as Request).command.Equals("EndlessVCUp"))
            {
                debugViewModel.AddDebugInfo("Endless_VC_Down", "-1");
            }
            else
            {
                debugViewModel.AddDebugInfo("Unknown command", "");
            }
        }
    }
}