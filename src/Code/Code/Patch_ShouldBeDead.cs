using HarmonyLib;
using Verse;

namespace RealDeathless
{
    [StaticConstructorOnStartup]
    public class Patch_ShouldBeDead
    {
        static Patch_ShouldBeDead()
        {
            if (Utilities_ModSettings.setting_RD_Patch_ShouldBeDead)
            {
                RealDeathless.harmonyInstance.Patch(AccessTools.Method(typeof(Pawn_HealthTracker), "ShouldBeDead"), new HarmonyMethod(typeof(Patch_ShouldBeDead), nameof(Prefix_ShouldBeDead)));
            }
        }

        static bool Prefix_ShouldBeDead(Pawn_HealthTracker __instance, ref bool __result)
        {
            Pawn pawn = Traverse.Create(__instance).Field("pawn").GetValue<Pawn>();

            if (pawn.IsColonistPlayerControlled && pawn.genes != null && pawn.genes.HasGene(DefDatabase<GeneDef>.GetNamed("RealDeathless")))
            {
                __result = false;
            }
            else __result = true;
            return __result;
        }
    }
}
