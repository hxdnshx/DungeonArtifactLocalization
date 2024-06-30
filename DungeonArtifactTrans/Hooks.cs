using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using BepInEx;
using Compiler;
using HarmonyLib;
using Localize;
using Newtonsoft.Json;
using Roguelike;
using UnityEngine;
using Spell;
using TMPro;

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
                Vocabulary.VrbToStr = ApplyLocalization(Vocabulary.VrbToStr);
                Vocabulary.AuxToStr = ApplyLocalization(Vocabulary.AuxToStr);
            }

            var fi = AccessTools.Field(typeof(GameProgress), "values");
            var value = fi.GetValue(null) as GameProgress.Progress;
            value.exp += 999999;
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

    [HarmonyPatch(typeof(Lang), nameof(Lang.ToText))]
    public static class LangFix
    {
        public static void Postfix(ref string __result)
        {
            if (String.IsNullOrEmpty(__result)) return;
            if (TranslationDB.LangDescription.TryGetValue(__result, out var transValue))
            {
                //Logger.Log($"{__result} to {transValue.Translation}");
                if (!transValue.Translation.Trim().IsNullOrWhiteSpace())
                    __result = transValue.Translation;
            }
            else
            {
                Logger.Log($"Unmatched <{__result}>");
            }
        }
    }
    
    [HarmonyPatch(typeof(Lang), nameof(Lang.LoadResource))]
    public static class LangFix2
    {
        public static void Postfix()
        {
            var fi = AccessTools.Field(typeof(Lang), "dictionary");
            var val = fi.GetValue(null) as Dictionary<string, string>;
            if (val == null)
            {
                Logger.Error($"Failed to Get Field dictionary");
            }
            foreach (var ele in TranslationDB.VocabularyInfo2)
            {
                val[ele.Key] = ele.Value;
            }
        }
    }

    [HarmonyPatch(typeof(PlayableCharacter), nameof(PlayableCharacter.LoadResource))]
    public static class CharacterFix
    {
        public static void Postfix()
        {
            var piName = AccessTools.Property(typeof(PlayableCharacter), "name");
            var piDesc = AccessTools.Property(typeof(PlayableCharacter), "discription");
            var players = PlayableCharacter.GetAll();
            foreach (var player in players)
            {
                if (TranslationDB.EntityInfo.TryGetValue(player.name, out var transValue))
                {
                    Logger.Log($"{player.name} to {transValue}");
                    if (!transValue.Trim().IsNullOrWhiteSpace())
                        piName.SetValue(player, transValue);
                }
                
                if (TranslationDB.EntityInfo.TryGetValue(player.discription, out var transValue2))
                {
                    Logger.Log($"{player.discription} to {transValue2}");
                    if (!transValue2.Trim().IsNullOrWhiteSpace())
                        piDesc.SetValue(player, transValue2);
                }
            }
        }
    }

    [HarmonyPatch(typeof(Controler), nameof(PlayableCharacter.LoadResource))]
    public static class EntityFix
    {
        public static void Postfix(ref Controler.EntityXml __result)
        {
            if (TranslationDB.EntityInfo.TryGetValue(__result.name, out var transValue))
            {
                if (!transValue.Trim().IsNullOrWhiteSpace())
                    __result.name = transValue;
            }
        }
    }

    [HarmonyPatch(typeof(CreditComponent), "Update")]
    public static class CreditFix
    {
        public static bool DoOnce = false;
        public static void Postfix()
        {
            if (DoOnce) return;
            DoOnce = true;
            var tailCredit = GameObject.Find("item (4)");
            var inner = tailCredit.transform.Find("auther");
            var txt = inner.GetComponent<TextMeshProUGUI>();
            /* Expected:ExertionGame
               https://exertiongame.com */
            const string MyString = "\n\n\n汉化工作：\n程序：Finn.\n翻译&校对：Phenix02, 哔哩是梨子";
            txt.text += MyString;
        }
    }
}
