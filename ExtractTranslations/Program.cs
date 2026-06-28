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

        Directory.CreateDirectory(jsonFilePath);

        var spellDirectory = LocalizedAssetPaths.ResolveDirectory(directoryPath, "Spell");
        var enchantDirectory = LocalizedAssetPaths.ResolveDirectory(directoryPath, "Enchant");
        var scenarioDirectory = LocalizedAssetPaths.ResolveDirectory(directoryPath, "Scenario");
        var characterDirectory = LocalizedAssetPaths.ResolveDirectory(directoryPath, "Character");
        var entityDirectory = LocalizedAssetPaths.ResolveDirectory(directoryPath, "Entity");

        CardTrans.ExtractCardText(spellDirectory, Path.Join(jsonFilePath, "spell.json"));
        CardTrans.ExtractCardPollText(spellDirectory, Path.Join(jsonFilePath, "pool.json"));
        EnchantTrans.ExtractEnchantText(enchantDirectory, Path.Join(jsonFilePath, "enchant.json"));
        ScenarioTrans.ExtractScenario(scenarioDirectory, Path.Join(jsonFilePath, "scenario.json"));
        CharacterTrans.ExtractCharacterText(characterDirectory, entityDirectory, Path.Join(jsonFilePath, "entity.json"));

        VocabularyTrans.ExtractVocabulary(LocalizedAssetPaths.ResolveFile(directoryPath, "vocabulary.xml"),
            Path.Join(jsonFilePath, "vocabulary.json"));
        VocabularyTrans.ExtractTsv(LocalizedAssetPaths.ResolveFile(directoryPath, "uitext.tsv"),
            Path.Join(jsonFilePath, "vocabulary2.json"));
        VocabularyTrans.ExtractTsvAchievementAcc(LocalizedAssetPaths.ResolveFile(directoryPath, "achievement_accumlate.tsv"),
            Path.Join(jsonFilePath, "AchievementAccumlate.json"));
        VocabularyTrans.ExtractTsvAchievementTitle(LocalizedAssetPaths.ResolveFile(directoryPath, "achievement_title.tsv"),
            Path.Join(jsonFilePath, "AchievementTitle.json"));
        
        Console.WriteLine("转换完成喵！");
        
        
    }

    private static void HandleParseError(IEnumerable<Error> errs)
    {
        // 处理错误...
    }
}
