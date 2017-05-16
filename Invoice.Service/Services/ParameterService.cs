using Invoice.Database;
using Invoice.Database.Interfaces;
using Invoice.Service.Base;
using Invoice.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Service.Services
{
    public class ParameterService : EntityService<ParameterGlobal, string>, IParameterService
    {
        public ParameterService(IContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<ParameterGlobal>();
        }
    }
}
