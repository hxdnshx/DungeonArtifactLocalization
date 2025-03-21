﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace catrice.DungeonArtifactTrans
{
    public static class FontFix
    {
        // From https://github.com/LocalizeLimbusCompany/LocalizeLimbusCompany
        public static List<TMP_FontAsset> tmpchinesefonts = new List<TMP_FontAsset>();
        public static List<string> tmpchinesefontnames = new List<string>();
        #region 字体
        public static bool AddChineseFont(string path)
        {
            if (File.Exists(path))
            {
                var AllAssets = AssetBundle.LoadFromFile(path).LoadAllAssets();
                foreach (var Asset in AllAssets)
                {
                    var TryCastFontAsset = Asset as TMP_FontAsset;
                    if (TryCastFontAsset)
                    {
                        UnityEngine.Object.DontDestroyOnLoad(TryCastFontAsset);
                        TryCastFontAsset.hideFlags |= HideFlags.HideAndDontSave;
                        tmpchinesefonts.Add(TryCastFontAsset);
                        tmpchinesefontnames.Add(TryCastFontAsset.name);
                        Logger.Log($"Loaded Font {TryCastFontAsset.name}");
                        return true;
                    }
                }
            }
            return false;
        }
        
        // 定义支持的字体名称集合
        private static readonly HashSet<string> ExactFontNames = new HashSet<string>
        {
            "ZenAntique-Regular SDF",
            "Pretendard-Regular SDF",
            "LiberationSans SDF",
            "Mikodacs SDF",
            "BebasKai SDF",
            "Anton-Regular SDF",
            //"Nexus_6-FREE SDF",
            "JetRunnerR-Regular SDF"
        };

        // 定义支持的字体前缀集合
        private static readonly List<string> FontNamePrefixes = new List<string>
        {
            "HinaMincho-Regular",
            "HigashiOme-Gothic-C",
            "SCDream"
        };
        
        public static bool GetChineseFont(string fontname, out TMP_FontAsset fontAsset)
        {
            fontAsset = null;
            if (tmpchinesefonts.Count == 0)
            {
                Logger.Log("Failed to Find chinese font.");
                return false;
            }
            // 使用 HashSet 和前缀检查替代复杂的逻辑判断
            if (ExactFontNames.Contains(fontname) || 
                FontNamePrefixes.Exists(prefix => fontname.StartsWith(prefix)))
            {
                fontAsset = tmpchinesefonts[0];
                return true;
            }
            
            Logger.Log($"Unmatched Font {fontname}");
            //if (fontname == "ZenAntique-Regular SDF" || fontname.StartsWith("HinaMincho-Regular") || fontname.StartsWith("HigashiOme-Gothic-C") || fontname == "Pretendard-Regular SDF" || fontname.StartsWith("SCDream") || fontname == "LiberationSans SDF" || fontname == "Mikodacs SDF" || fontname == "BebasKai SDF")
            return false;
        }

        //static FieldInfo fi = null;
        public static TMP_FontAsset GetFontAsset(TMP_Text txt)
        {
            return txt.font;
            /*
            if (fi == null)
                fi = AccessTools.Field(typeof(TMP_Text), "m_fontAsset");
            if (fi != null)
                return fi.GetValue(txt) as TMP_FontAsset;
            return null;
            */
        }
        
        public static bool IsChineseFont(TMP_FontAsset fontAsset)
            => tmpchinesefontnames.Contains(fontAsset.name);
        //[HarmonyPatch(typeof(TMP_Text), nameof(TMP_Text.font), MethodType.Setter)]
        //[HarmonyPrefix]
        private static bool set_font(TMP_Text __instance, ref TMP_FontAsset value)
        {
            if (IsChineseFont(GetFontAsset(__instance)))
                return false;
            string fontname = GetFontAsset(__instance).name;
            if (GetChineseFont(fontname, out TMP_FontAsset font))
                value = font;
            return true;
        }
        //[HarmonyPatch(typeof(TMP_Text), nameof(TMP_Text.fontMaterial), MethodType.Setter)]
        //[HarmonyPrefix]
        private static void set_fontMaterial(TMP_Text __instance, ref Material value)
        {
            if (IsChineseFont(GetFontAsset(__instance)))
                value = GetFontAsset(__instance).material;
        }
        
        [HarmonyPatch(typeof(TMP_Text), nameof(TMP_Text.text), MethodType.Setter)]
        [HarmonyPostfix]
        private static void set_text(TMP_Text __instance, ref string value)
        {
            try
            {
                if (!IsChineseFont(GetFontAsset(__instance)))
                {
                    string fontname = GetFontAsset(__instance).name;
                    if (GetChineseFont(fontname, out TMP_FontAsset font))
                    {

                        Logger.Log($"Replaced font {__instance.font.name}");
                        __instance.font = font;
                        __instance.SetAllDirty();
                        __instance.ForceMeshUpdate(true);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log("Encountered error:" + e.Message);
            }
        }
        
        
        #endregion
        
        public static void Init()
        {
            var modPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fontPath = Path.Join(modPath, "tmpchinesefont");
            if (!AddChineseFont(fontPath))
            {
                Logger.Log("You Not Have Chinese Font, Please Read GitHub Readme To Download\n你没有下载中文字体,请阅读GitHub的Readme下载");
            }
        }
    }
}