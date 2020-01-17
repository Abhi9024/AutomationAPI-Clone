using System;
using System.Collections.Generic;
using System.Text;

namespace Automation.Core.DataAccessAbstractions
{
    public interface ITestDataRepo
    {
        void CreateTestData(TestData testDataEntity);
        void UpdateTestData(int testDataId, TestData testDataEntity);
        void DeleteTestData(int testDataId,int userId);
        void UpdateLockedByFlags(TestData testData);
    }
}
