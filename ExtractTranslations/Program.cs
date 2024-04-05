using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
using CommandLine;
using ExtractTranslations;


public class Program
{
    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(RunOptionsAndReturnExitCode)
            .WithNotParsed<Options>(HandleParseError);
    }

    private static void RunOptionsAndReturnExitCode(Options opts)
    {
        var directoryPath = opts.InputDirectory;
        var jsonFilePath = opts.OutputDirectory;

        CardTrans.ExtractCardText(Path.Join(directoryPath, "Spell"), Path.Join(jsonFilePath, "spell.json"));
        CardTrans.ExtractCardPollText(Path.Join(directoryPath, "Spell"), Path.Join(jsonFilePath, "pool.json"));
        EnchantTrans.ExtractEnchantText(Path.Join(directoryPath, "Enchant"), Path.Join(jsonFilePath, "enchant.json"));
        ScenarioTrans.ExtractScenario(Path.Join(directoryPath, "Scenario"), Path.Join(jsonFilePath, "scenario.json"));

        VocabularyTrans.ExtractVocabulary(Path.Join(directoryPath, "Language/Japanese_Vocabulary.txt"),
            Path.Join(jsonFilePath, "vocabulary.json"));
        
        Console.WriteLine("转换完成喵！");
    }

    private static void HandleParseError(IEnumerable<Error> errs)
    {
        // 处理错误...
    }
}