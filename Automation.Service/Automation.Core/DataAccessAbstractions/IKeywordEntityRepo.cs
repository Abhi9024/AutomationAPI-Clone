using System;
using System.Collections.Generic;
using System.Text;

namespace Automation.Core.DataAccessAbstractions
{
    public interface IKeywordEntityRepo
    {
        void CreateKeyword(KeywordLibrary keyword);
        void UpdateKeyword(int keywordId, KeywordLibrary keyword);
        void DeleteKeyword(int keywordId,int userId);
        void UpdateLockedByFlags(KeywordLibrary keyword);
    }
}
