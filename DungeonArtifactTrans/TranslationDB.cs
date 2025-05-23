﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace catrice.DungeonArtifactTrans
{
    public static class TranslationDB
    {
        public static Dictionary<string, TranslationItem> CardInfo = null;
        public static Dictionary<string, TranslationItem> EnchantInfo = null;
        public static Dictionary<string, string> VocabularyInfo = null;
        public static Dictionary<string, string> VocabularyInfo2 = null;
        public static Dictionary<string, string> ScenarioInfo = null;
        public static Dictionary<string, TranslationItem> LangDescription = null;
        public static Dictionary<string, string> EntityInfo = null;
        public static Dictionary<string, string> AchievementAccumlateInfo = null;
        public static Dictionary<string, string> AchievementTitleInfo = null;
        
        public static void Init()
        {
            
            var modPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            {
                // 读取 JSON 文件内容
                string jsonContent = File.ReadAllText(
                    Path.Join(modPath, "spell_translated.json"));

                // 反序列化 JSON 字符串为 TranslationItem 结构体的列表
                var items = JsonConvert.DeserializeObject<List<TranslationItem>>(jsonContent);
                var dst = new Dictionary<string, TranslationItem>();
                foreach (var item in items)
                {
                    if (!dst.TryAdd(item.Original, item))
                    {
                        Logger.Log($"Find Duplicated Item {item.Key}, content {item.Translation}");
                        continue;
                    }
                }

                CardInfo = dst;
            }

            {
                // 读取 JSON 文件内容
                string jsonContent = File.ReadAllText(
                    Path.Join(modPath, "vocabulary_translated.json"));

                // 反序列化 JSON 字符串为 TranslationItem 结构体的列表
                var items = JsonConvert.DeserializeObject<List<TranslationItem>>(jsonContent);
                var dst = new Dictionary<string, string>();
                foreach (var item in items)
                {
                    if (!dst.TryAdd(item.Original, item.Translation))
                    {
                        Logger.Log($"Find Duplicated Item {item.Key}, content {item.Translation}");
                        continue;
                    }
                }

                VocabularyInfo = dst;
            }
            
            {
                // 读取 JSON 文件内容
                string jsonContent = File.ReadAllText(
                    Path.Join(modPath, "scenario_translated.json"));

                // 反序列化 JSON 字符串为 TranslationItem 结构体的列表
                var items = JsonConvert.DeserializeObject<List<TranslationItem>>(jsonContent);
                var dst = new Dictionary<string, string>();
                foreach (var item in items)
                {
                    if (!dst.TryAdd(item.Original, item.Translation))
                    {
                        Logger.Log($"Find Duplicated Item {item.Key}, content {item.Translation}");
                        continue;
                    }
                }

                ScenarioInfo = dst;
            }
            
            {
                // 读取 JSON 文件内容
                string jsonContent = File.ReadAllText(
                    Path.Join(modPath, "enchant_translated.json"));

                // 反序列化 JSON 字符串为 TranslationItem 结构体的列表
                var items = JsonConvert.DeserializeObject<List<TranslationItem>>(jsonContent);
                var dst = new Dictionary<string, TranslationItem>();
                foreach (var item in items)
                {
                    if (!dst.TryAdd(item.Original, item))
                    {
                        Logger.Log($"Find Duplicated Item {item.Original}, content {item.Translation}");
                        continue;
                    }
                }

                EnchantInfo = dst;
            }
            
            {
                // 读取 JSON 文件内容
                string jsonContent = File.ReadAllText(
                    Path.Join(modPath, "translated_json.json"));

                // 反序列化 JSON 字符串为 TranslationItem 结构体的列表
                var items = JsonConvert.DeserializeObject<List<TranslationItem>>(jsonContent);
                var dst = new Dictionary<string, TranslationItem>();
                foreach (var item in items)
                {
                    if (!dst.TryAdd(item.Original, item))
                    {
                        Logger.Log($"Find Duplicated Item {item.Original}, content {item.Translation}");
                        continue;
                    }
                }

                LangDescription = dst;
            }
            
            {
                // 读取 JSON 文件内容
                string jsonContent = File.ReadAllText(
                    Path.Join(modPath, "entity_translated.json"));

                // 反序列化 JSON 字符串为 TranslationItem 结构体的列表
                var items = JsonConvert.DeserializeObject<List<TranslationItem>>(jsonContent);
                var dst = new Dictionary<string, string>();
                foreach (var item in items)
                {
                    if (!dst.TryAdd(item.Original, item.Translation))
                    {
                        Logger.Log($"Find Duplicated Item {item.Original}, content {item.Translation}");
                        continue;
                    }
                }

                EntityInfo = dst;
            }
            
            {
                // 读取 JSON 文件内容
                string jsonContent = File.ReadAllText(
                    Path.Join(modPath, "vocabulary2_translated.json"));

                // 反序列化 JSON 字符串为 TranslationItem 结构体的列表
                var items = JsonConvert.DeserializeObject<List<TranslationItem>>(jsonContent);
                var dst = new Dictionary<string, string>();
                foreach (var item in items)
                {
                    if (!dst.TryAdd(item.Key, item.Translation))
                    {
                        Logger.Log($"Find Duplicated Item {item.Key} {item.Original}, content {item.Translation}");
                        continue;
                    }
                }

                VocabularyInfo2 = dst;
            }
            
            {
                // 读取 JSON 文件内容
                string jsonContent = File.ReadAllText(
                    Path.Join(modPath, "AchievementAccumlate_translated.json"));

                // 反序列化 JSON 字符串为 TranslationItem 结构体的列表
                var items = JsonConvert.DeserializeObject<List<TranslationItem>>(jsonContent);
                var dst = new Dictionary<string, string>();
                foreach (var item in items)
                {
                    if (!dst.TryAdd(item.Key, item.Translation))
                    {
                        Logger.Log($"Find Duplicated Item {item.Original}, content {item.Translation}");
                        continue;
                    }
                }

                AchievementAccumlateInfo = dst;
            }
            
            {
                // 读取 JSON 文件内容
                string jsonContent = File.ReadAllText(
                    Path.Join(modPath, "AchievementTitle_translated.json"));

                // 反序列化 JSON 字符串为 TranslationItem 结构体的列表
                var items = JsonConvert.DeserializeObject<List<TranslationItem>>(jsonContent);
                var dst = new Dictionary<string, string>();
                foreach (var item in items)
                {
                    if (!dst.TryAdd(item.Key, item.Translation))
                    {
                        Logger.Log($"Find Duplicated Item {item.Original}, content {item.Translation}");
                        continue;
                    }
                }

                AchievementTitleInfo = dst;
            }
        }
        
        
    }
}