namespace Doublets.Library;

public interface IDoubletsSearch
{
   abstract static List<string> FindPath(HashSet<string> dictionary, string startWord, string endWord);
}