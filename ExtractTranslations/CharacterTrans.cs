﻿using System.Xml.Linq;
using Newtonsoft.Json;

namespace ExtractTranslations;

public class CharacterTrans
{
    
    public static void ExtractCharacterText(string directoryPath, string outJsonPath)
    {
        
        var translations = new List<Dictionary<string, object>>();

       

        
        foreach (var filePath in Directory.GetFiles(directoryPath, "*.xml", SearchOption.AllDirectories))
        {
            FileInfo fi = new FileInfo(filePath);
            var parentName = fi.Directory.Name;
            var doc = XDocument.Load(filePath);
            var name = doc.Root?.Element("name")?.Value;
            var discription = doc.Root?.Element("discription")?.Value;

            if (name != null)
            {
                translations.Add(new Dictionary<string, object>
                {
                    { "key", $"{parentName}.{fi.Name}.name" },
                    { "original", name },
                    { "translation", name },
                    { "context", null }
                });
            }

            if (discription != null)
            {
                //对于entity 的 xml，不会包含这个部分
                translations.Add(new Dictionary<string, object>
                {
                    { "key", $"{parentName}.{fi.Name}.discription" },
                    { "original", discription },
                    { "translation", discription },
                    { "context", null }
                });
            }
        }
        
        
        var json = JsonConvert.SerializeObject(translations, Formatting.Indented);
        File.WriteAllText(outJsonPath, json);
    }
}