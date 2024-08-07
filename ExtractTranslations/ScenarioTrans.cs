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
            var fi = new FileInfo(filePath);
            var fileContent = File.ReadAllText(filePath);
            var matches = Regex.Matches(fileContent, @"Write(Add)*\(""(.*?)""\);");

            foreach (Match match in matches)
            {
                if (match.Groups.Count > 2) // 确保有捕获组
                {
                    var text = match.Groups[2].Value; // 获取Write函数中的字符串参数
                    jsonList.Add(new Dictionary<string, object>
                    {
                        { "key", $"{fi.Name}.{text}" },
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