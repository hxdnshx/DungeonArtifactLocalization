using BepInEx;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
namespace catrice.DungeonArtifactTrans
{
    [BepInPlugin(GUID, "DungeonArtifactTrans", "1.0.0")]
    public class EntryPoint : BaseUnityPlugin
    {
        public const string GUID = "com.catrice.DungeonArtifactTrans";
        private void Awake()
        {
            catrice.DungeonArtifactTrans.Logger.LogInstance = this.Logger;
            var harmony = new Harmony(GUID);
            
            
            catrice.DungeonArtifactTrans.Logger.Log("Start Init");
            
            TranslationDB.Init();
            FontFix.Init();

            catrice.DungeonArtifactTrans.Logger.Log("AddPanties Patched.");



            harmony.PatchAll();
            harmony.PatchAll(typeof(FontFix));
        }
    }
}