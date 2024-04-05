using System.Xml.Linq;
using Newtonsoft.Json;

namespace ExtractTranslations;

public static class EnchantTrans
{
    
    public static void ExtractEnchantText(string directoryPath, string outJsonPath)
    {
        
        var translations = new List<Dictionary<string, object>>();

       

        
        foreach (var filePath in Directory.GetFiles(directoryPath, "*.xml", SearchOption.AllDirectories))
        {
            FileInfo fi = new FileInfo(filePath);
            var doc = XDocument.Load(filePath);
            var mainClass = doc.Root.Element("enchant")?.Value;
            var name = doc.Root.Element("name")?.Value;
            var context = doc.Root.Element("context")?.Value;

            if (mainClass != null)
            {
                translations.Add(new Dictionary<string, object>
                {
                    { "key", $"{fi.Name}.name" },
                    { "original", name },
                    { "translation", name },
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