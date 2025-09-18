using System.Text.RegularExpressions;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace ExtractTranslations;

public static class CardTrans
{
    public static void ExtractCardText(string directoryPath, string outJsonPath)
    {
        
        var translations = new List<Dictionary<string, object>>();

       

        
        foreach (var filePath in Directory.GetFiles(directoryPath, "*.xml", SearchOption.AllDirectories))
        {
            FileInfo fi = new FileInfo(filePath);
            var parentName = fi.Name;
            var fileNumber = 0;
            Match match = Regex.Match(parentName, @"\d+");
            if (match.Success)
            {
                fileNumber = int.Parse(match.Value);
            }
            var txt = File.ReadAllLines(filePath);
            foreach (var line in txt)
            {
                var doc = XDocument.Parse(line);
                var mainClass = doc.Root.Element("mainClass")?.Value;
                var name = doc.Root.Element("name")?.Value;
                var id = doc.Root.Element("id")?.Value;
                var context = doc.Root.Element("context")?.Value;

                if (mainClass != null)
                {
                    translations.Add(new Dictionary<string, object>
                    {
                        { "key", $"{fileNumber}.{id}.xml.name" },
                        { "original", name },
                        { "translation", name },
                        { "context", null }
                    });

                    translations.Add(new Dictionary<string, object>
                    {
                        { "key", $"{fileNumber}.{id}.xml.context" },
                        { "original", context },
                        { "translation", context },
                        { "context", null }
                    });
                }
            }
        }
        
        
        var json = JsonConvert.SerializeObject(translations, Formatting.Indented);
        File.WriteAllText(outJsonPath, json);
    }
    
    public static void ExtractCardPollText(string directoryPath, string outJsonPath)
    {
        
        var translations = new List<Dictionary<string, object>>();


        
        
        foreach (var filePath in Directory.GetFiles(directoryPath, "*.pool", SearchOption.AllDirectories))
        {
            FileInfo fi = new FileInfo(filePath);
            var doc = XDocument.Load(filePath);
            //var mainClass = doc.Root.Element("mainClass")?.Value;
            var title = doc.Root.Element("title")?.Value;
            var context = doc.Root.Element("context")?.Value;

            if (true)
            {
                translations.Add(new Dictionary<string, object>
                {
                    { "key", $"{fi.Name}.title" },
                    { "original", title },
                    { "translation", title },
                    { "context", null }
                });

                translations.Add(new Dictionary<string, object>
                {
                    { "key", $"{fi.Name}.context" },
                    { "original", context },
                    { "translation", context },
                    { "context", null }
                });
            }
        }
        
        
        var json = JsonConvert.SerializeObject(translations, Formatting.Indented);
        File.WriteAllText(outJsonPath, json);
    }
}