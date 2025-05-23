﻿using System;
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
        CharacterTrans.ExtractCharacterText(Path.Join(directoryPath, "Japanese"), Path.Join(jsonFilePath, "entity.json"));

        VocabularyTrans.ExtractVocabulary(Path.Join(directoryPath, "Language/Japanese_Vocabulary.txt"),
            Path.Join(jsonFilePath, "vocabulary.json"));
        VocabularyTrans.ExtractTsv(Path.Join(directoryPath, "Language/Japanese.tsv"),
            Path.Join(jsonFilePath, "vocabulary2.json"));
        VocabularyTrans.ExtractTsvAchievementAcc(Path.Join(directoryPath, "Japanese/AchievementAccumlate.tsv"),
            Path.Join(jsonFilePath, "AchievementAccumlate.json"));
        VocabularyTrans.ExtractTsvAchievementTitle(Path.Join(directoryPath, "Japanese/AchievementTitle.tsv"),
            Path.Join(jsonFilePath, "AchievementTitle.json"));
        
        Console.WriteLine("转换完成喵！");
        
        
    }

    private static void HandleParseError(IEnumerable<Error> errs)
    {
        // 处理错误...
    }
}