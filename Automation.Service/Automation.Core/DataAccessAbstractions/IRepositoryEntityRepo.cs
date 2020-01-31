using System;
using System.Collections.Generic;
using System.Text;

namespace Automation.Core.DataAccessAbstractions
{
    public interface IRepositoryEntityRepo
    {
        void CreateRepository(Repository repositoryEntity);
        void UpdateRepository(int repositoryId, Repository repositoryEntity);
        void DeleteRepository(int repositoryId,int userId);
        void UpdateLockedByFlags(Repository repository);
        List<string> GetAllLogicalNames();
        List<string> GetAllFindMethod();
    }
}
