using System;
using System.Collections.Generic;
using System.Text;

namespace Automation.Core.DataAccessAbstractions
{
    public interface IGenericRepo<T>
    {
        IList<T> GetAll();
        T GetById(int id);
        
    }
}
