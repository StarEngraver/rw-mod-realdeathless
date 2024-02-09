using HarmonyLib;
using System.Reflection;
using UnityEngine;
using Verse;

namespace RealDeathless
{
    public class RealDeathless : Mod
    {
        public static Harmony harmonyInstance;

        public RealDeathless(ModContentPack modContent) : base(modContent)
        {
            GetSettings<Utilities_ModSettings>();
            harmonyInstance = new Harmony("StarEngraver.RealDeathless");
            harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
        }

        public override string SettingsCategory()
        {
            return "ModRealDeathless".Translate();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Utilities_ModSettings.DoWindowContents(inRect);
        }

    }

    public class Utilities_ModSettings : ModSettings
    {
        public static bool setting_RD_Patch_ShouldBeDead = true;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref setting_RD_Patch_ShouldBeDead, "setting_RD_Patch_ShouldBeDead", true, true);
        }

        public static void DoWindowContents(Rect inRect)
        {
            Listing_Standard settingListingStandard = new Listing_Standard();
            settingListingStandard.Begin(inRect);
            settingListingStandard.Label("stringRD_Setting_Global".Translate());
            settingListingStandard.CheckboxLabeled("stringRD_Patch_ShouldBeDead".Translate(), ref setting_RD_Patch_ShouldBeDead, "stringRD_Patch_ShouldBeDead_Hint".Translate());
            settingListingStandard.End();
        }
    }
}
