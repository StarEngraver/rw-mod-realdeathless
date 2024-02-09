using RimWorld;
using System.Collections.Generic;
using Verse;

namespace RealDeathless
{
  public class Gene_RealDeathless : Gene
  {
    public int tickCounter = 0;

    public override void Notify_PawnDied()
    {
      Map map = pawn.Corpse.Map;
      if (map != null && pawn.Faction.IsPlayer)
      {
        ResurrectionUtility.Resurrect(pawn.Corpse.InnerPawn);
        pawn.health.RemoveAllHediffs();
        pawn.health.AddHediff(HediffMaker.MakeHediff(HediffDefOf.ResurrectionSickness, pawn));
        Find.StoryWatcher.statsRecord.colonistsKilled--;
      }
    }

    public override void Tick()
    {
      base.Tick();

      tickCounter++;

      if (tickCounter == 60000)
      {
        if (pawn.health != null)
        {
          List<Hediff_MissingPart> missParts = pawn.health.hediffSet.GetMissingPartsCommonAncestors();

          // List<Hediff_Injury> hasInjuries = pawn.health.hediffSet.GetInjuriesTendable();

          List<Hediff_Injury> hasInjuries = new List<Hediff_Injury>();

          for (int i = 0; i < pawn.health.hediffSet.hediffs.Count; i++)
          {
            Hediff_Injury recordInjuries = pawn.health.hediffSet.hediffs[i] as Hediff_Injury;
            if (recordInjuries != null)
            {
              hasInjuries.Add(recordInjuries);
            }
          }

          if (missParts.Count > 0)
          {
            for (int i = 0; i < missParts.Count; i++)
            {
              BodyPartRecord bodyPartRecord = missParts[i].Part;
              pawn.health.RestorePart(bodyPartRecord, null, true);
            }
          }

          if (hasInjuries.Count > 0)
          {
            for (int i = 0; i < hasInjuries.Count; i++)
            {
              hasInjuries[i].Severity = hasInjuries[i].Severity - 10;
            }
          }
        }
        tickCounter = 0;
      }
    }
  }
}
