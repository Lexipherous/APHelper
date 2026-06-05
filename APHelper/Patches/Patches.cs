using Il2Cpp;
using MelonLoader;
using UnityEngine;
using APHelper.Functions;

namespace APHelper.Patches;

public class Patches
{
    /// <summary>
    /// Hook for items.
    /// This seems to include: white items, purple items, atrium, fragments. Force Cap items untested.
    /// </summary>
    public static bool CustomPickableItem(Gameplay_PickableItem __instance)
    {
        Melon<APHelperClass>.Logger.Msg("syncerID: " + __instance.syncer.syncerID +
                                        " - areaName: " + __instance.gameObject.scene.name +
                                        " - itemName: " + __instance.targetItem.name +
                                        " - itemQuantity: " + __instance.amount +
                                        " - itemXp: " + __instance.expGainTerm.Length);
        return true;
    }

    /// <summary>
    /// Hook for interactibles.
    /// This seems to include: Overgrown Barrier.
    /// </summary>
    public static bool CustomInteractableAbsorbable(Gameplay_InteractableAbsorbable __instance)
    {
        Melon<APHelperClass>.Logger.Msg($"IA: syncerID: {__instance.syncer.syncerID} - {__instance.name}");
        return true;
    }

    /// <summary>
    /// Hook for interactibles.
    /// This seems to include: Lahav Knight force drops
    /// </summary>
    public static bool CustomObjectAbsorbHandler(Gameplay_ObjectAbsorbHandler __instance)
    {
        Melon<APHelperClass>.Logger.Msg($"OAH: syncerID: {__instance.syncer.syncerID} - Name: {__instance.name} - isFlesh: {__instance.absorbFlesh} - graspText: {__instance.graspText}");
        return true;
    }
}