﻿using Invoice.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Service.Interfaces
{
    public interface IParameterService : IEntityService<ParameterGlobal, string>
    {
    }
}
