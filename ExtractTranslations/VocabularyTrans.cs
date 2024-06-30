using System.Xml.Linq;
using Newtonsoft.Json;

namespace ExtractTranslations;

public static class VocabularyTrans
{
    static string GetXPath(XElement element)
    {
        string path = element.Name.LocalName;
        XElement? temp = element;

        // 遍历所有父元素，构建完整的 XPath
        while (temp.Parent != null)
        {
            temp = temp.Parent;
            path = temp.Name.LocalName + "/" + path;
        }

        return "/" + path;
    }
    
    public static void ExtractVocabulary(string directoryPath, string outJsonPath)
    {
        
        var document = XDocument.Load(directoryPath);
        var stringElements = document.Descendants("string");

        var jsonList = new List<Dictionary<string, object>>();

        foreach (var element in stringElements)
        {
            string xPath = GetXPath(element);
            string value = element.Value;

            jsonList.Add(new Dictionary<string, object>
            {
                { "key", xPath + value },
                { "original", value },
                { "translation", value },
                { "context", null }
            });
        }

        string json = JsonConvert.SerializeObject(jsonList, Formatting.Indented);
        File.WriteAllText(outJsonPath, json);
    }

    public static void ExtractTsv(string filePath, string outJsonPath)
    {
        var jsonList = new List<Dictionary<string, object>>();
        var stringElements = File.ReadAllLines(filePath);
        foreach (var element in stringElements)
        {
            var values = element.Split("\t");
            if (values.Length < 2) continue;
            
            jsonList.Add(new Dictionary<string, object>
            {
                { "key", values[0] },
                { "original", values[1] },
                { "translation", values[1] },
                { "context", null }
            });
        }
        string json = JsonConvert.SerializeObject(jsonList, Formatting.Indented);
        File.WriteAllText(outJsonPath, json);
    }
}