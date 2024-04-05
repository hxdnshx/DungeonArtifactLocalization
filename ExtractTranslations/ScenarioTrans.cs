using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace ExtractTranslations;

public class ScenarioTrans
{
    public static void ExtractScenario(string directoryPath, string outJsonPath)
    {
        
        var jsonList = new List<Dictionary<string, object>>();

        foreach (var filePath in Directory.GetFiles(directoryPath, "*.txt", SearchOption.AllDirectories))
        {
            var fileContent = File.ReadAllText(filePath);
            var matches = Regex.Matches(fileContent, @"Write\(""(.*?)""\);");

            foreach (Match match in matches)
            {
                if (match.Groups.Count > 1) // 确保有捕获组
                {
                    var text = match.Groups[1].Value; // 获取Write函数中的字符串参数
                    jsonList.Add(new Dictionary<string, object>
                    {
                        { "key", text },
                        { "original", text },
                        { "translation", text },
                        { "context", null }
                    });
                }
            }
        }

        var json = JsonConvert.SerializeObject(jsonList, Formatting.Indented);
        File.WriteAllText(outJsonPath, json);
    }

}