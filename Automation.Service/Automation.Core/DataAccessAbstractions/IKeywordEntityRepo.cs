using System;
using System.Collections.Generic;
using System.Text;

namespace Automation.Core.DataAccessAbstractions
{
    public interface IKeywordEntityRepo
    {
        void CreateKeyword(KeywordLibrary keyword);
        void CreateKeyword_Map(KeywordLibrary_Map keyword);
        void UpdateKeyword(int keywordId, KeywordLibrary keyword);
        void UpdateKeywordMap(int? userId, KeywordLibrary_Map keyword);
        void DeleteKeyword(int keywordId,int userId);
        void DeleteKeywordMap(int? userId,int masterKeywordId);
        void UpdateLockedByFlags(KeywordLibrary keyword);
        List<KeywordLibrary> GetFilteredKeywords(List<int> Ids);
        KeywordLibrary_Map GetMappedKeywordLibrary(int id, int? userId);
        List<string> GetAllFunctionNames();
    }
}
