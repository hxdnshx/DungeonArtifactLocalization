using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Compiler;
using HarmonyLib;
using Newtonsoft.Json;
using UnityEngine;
using Spell;

namespace catrice.DungeonArtifactTrans
{

    [HarmonyPatch(typeof(CardPool), nameof(CardPool.LoadResource))]
    public static class LoadCardPrefix
    {
        public static void Postfix(ref SpellXml __result)
        {
            if (__result == null) return;
            string nameId = $"{__result.name}";
            string contextId = $"{__result.context}";
            if (TranslationDB.CardInfo.TryGetValue(nameId, out var fndValue))
            {
                if (fndValue.IsReplaced) return;
                fndValue.IsReplaced = true;
                __result.name = fndValue.Translation;
                if (TranslationDB.CardInfo.TryGetValue(contextId, out var fndContext))
                {
                    __result.context = fndContext.Translation;
                }
                else
                {
                    
                    Logger.Log($"Unexpected Replace Result {nameId} with {__result.name} , {contextId} with {__result.context}");
                }

            }
        }
    }
    

    [HarmonyPatch(typeof(EnchantContainer), nameof(EnchantContainer.LoadResource))]
    public static class EnchantPostfix
    {
        public static void Postfix(ref EnchantContainer.EnchantXml __result)
        {
            if (__result == null) return;
            string nameId = $"{__result.name}";
            string contextId = $"{__result.context}";
            if (TranslationDB.EnchantInfo.TryGetValue(nameId, out var fndValue))
            {
                if (fndValue.IsReplaced) return;
                fndValue.IsReplaced = true;
                __result.name = fndValue.Translation;
                if (TranslationDB.EnchantInfo.TryGetValue(contextId, out var fndContext))
                {
                    __result.context = fndContext.Translation;
                }
                else
                {
                    Logger.Log($"Error: could not found {contextId}, but found {nameId} {fndValue.Translation}");
                }
            }
        }
    }
    [HarmonyPatch(typeof(Vocabulary), nameof(Vocabulary.Load))]
    public static class VocabularyFix
    {
        public static string[] ApplyLocalization(string[] input)
        {
            return input
                .Select(item => TranslationDB.VocabularyInfo.GetValueOrDefault(item, item))
                .ToArray();
        }

        public static void Postfix()
        {
            {
                Vocabulary.SubExpToStr = ApplyLocalization(Vocabulary.SubExpToStr);
                Vocabulary.PlaceToStr = ApplyLocalization(Vocabulary.PlaceToStr);
                Vocabulary.PssToStr = ApplyLocalization(Vocabulary.PssToStr);
                Vocabulary.attrToStr = ApplyLocalization(Vocabulary.attrToStr);
                Vocabulary.SubToStr = ApplyLocalization(Vocabulary.SubToStr);
                Vocabulary.typeToStr = ApplyLocalization(Vocabulary.typeToStr);
                Vocabulary.keyWords = ApplyLocalization(Vocabulary.keyWords);
                Vocabulary.keyWordsExp = ApplyLocalization(Vocabulary.keyWordsExp);
                Vocabulary.categoryToStr = ApplyLocalization(Vocabulary.categoryToStr);
            }
        }
    }

    [HarmonyPatch(typeof(ScenarioComponent), nameof(ScenarioComponent.Write))]
    public static class ScenarioFix
    {
        public static void Prefix(ScenarioComponent __instance, ref string str)
        {
            Logger.Log($"Translate:{str}");
            str = TranslationDB.ScenarioInfo.GetValueOrDefault(str, str);
            Logger.Log($"Translated:{str}");
        }
    }
    
}
