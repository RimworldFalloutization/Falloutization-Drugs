using HarmonyLib;
using RimWorld.QuestGen;

namespace Falloutization.Drugs.Patches;

/// <summary>
/// Replaces Penoxycyline with FCP Radaway in the list of items beggars can request.
/// When FCP-Chems is not present, falls back to vanilla Penoxycyline.
/// </summary>
[HarmonyPatch(typeof(QuestNode_Root_Beggars), "get_AllowedThings")]
public static class BeggarsAllowedThingsPatch
{
    private static ThingDef _radaway;
    private static ThingDef Radaway => _radaway ??= DefDatabase<ThingDef>.GetNamedSilentFail("FCP_Chem_Radaway");

    [HarmonyPostfix]
    public static void Postfix(ref IEnumerable<ThingDef> __result)
    {
        ThingDef radaway = Radaway;
        if (radaway == null) return;
        __result = __result.Select(def => def == ThingDefOf.Penoxycyline ? radaway : def);
    }
}
