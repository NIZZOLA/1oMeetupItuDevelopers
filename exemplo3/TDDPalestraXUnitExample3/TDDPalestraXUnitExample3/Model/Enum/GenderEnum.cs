﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TDDPalestraXUnitExample3.Model.Enum
{
    public enum GenderEnum
    {
        [Description("Não informado")]
        Uninformed = 0,
        [Description("Masculino")]
        Male = 1,
        [Description("Feminino")]
        Female = 2
    }
}