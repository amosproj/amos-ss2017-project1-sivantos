﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMachineFrontend1.Helpers;

namespace TestMachineFrontend1.Model
{
    
   public class TabControlModel
    {
        /// <summary>
        /// Tab Header text
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Tab Content 
        /// </summary>
        public ObservableObject CurrentTabContentViewModel { get; set; }
    }
}
