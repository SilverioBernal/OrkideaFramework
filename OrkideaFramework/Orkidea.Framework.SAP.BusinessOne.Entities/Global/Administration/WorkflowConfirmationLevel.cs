﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.Global.Administration
{
    [DataContract(Namespace = "http://WSSAP")]
    public class WorkflowConfirmationLevel
    {
        [DataMember]
        public int wstCode { get; set; }
        [DataMember]
        public string name { get; set; }
    }
}