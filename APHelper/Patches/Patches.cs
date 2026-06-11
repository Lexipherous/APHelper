using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace APHelper.Patches;

public class Patches
{
    /// <summary>
    /// Hook for items.
    /// This seems to include: white items, purple items, atrium, fragments. Force Cap items untested.
    /// </summary>
    public static bool APItemPickup(Gameplay_PickableItem __instance, bool instant) {
        // Replace current item with obsolete hidden item
        var newItem = GetItemById("6c96f712-0211-4c1f-a794-cdb781a574dc");
        __instance.targetItem = newItem;
        
        foreach (var i in __instance.absorbableAdditionals) {
            Melon<APHelperClass>.Logger.Msg(i.name);
        }
        
        if (__instance.pickupType == Gameplay_PickableItem.PickupType.Exp) {__instance.amount = 0;}
        
        Melon<APHelperClass>.Logger.Msg($"ItemPickup Additional: syncerID: {__instance} - Count:{__instance.absorbableAdditionals.Count}");

        SendHandler("ItemPickup", __instance.gameObject.GetComponent<Syncer>().syncerID, __instance.gameObject.scene.name, __instance.gameObject.transform.position);
        return true;
    }

    /// <summary>
    /// Hook for interactibles.
    /// This seems to include: Overgrown Barrier.
    /// </summary>
    public static bool APInteractAbsorb(Gameplay_InteractableAbsorbable __instance) {
        //var newType = RewardType.AP_Pickup;
        __instance.rewardType = (Gameplay_InteractableAbsorbable.RewardType)3;
        
        var newItem = __instance.PickUp();
        SendHandler("InteractAbsorb", __instance.gameObject.GetComponent<Syncer>().syncerID, __instance.gameObject.scene.name, __instance.gameObject.transform.position);
        return true;
    }

    /// <summary>
    /// Hook for interactibles.
    /// This seems to include: Lahav Knight force drops
    /// </summary>
    public static bool APObjectAbsorb(Gameplay_ObjectAbsorbHandler __instance) {
        SendHandler("ObjectAbsorb", __instance.gameObject.GetComponent<Syncer>().syncerID, __instance.gameObject.scene.name, __instance.gameObject.transform.position);
        return true;
    }

    /// <summary>
    /// Hook for Bloodroot and Marah Strand.
    /// This seems to include: Lahav Knight force drops
    /// </summary>
    public static bool APMarahBloodroot(AbsorbableCluster_Event __instance) {
        SendHandler("MarahBloodroot", __instance.gameObject.GetComponent<Syncer>().syncerID, __instance.gameObject.scene.name, __instance.gameObject.transform.position);
        return true;
    }

    /// <summary>
    /// </summary>
    //public static bool AP_NPC(Profile_NPC __instance) {
    //    SendHandler("AP_NPC", __instance.gameObject.GetComponent<Syncer>().syncerID, __instance.gameObject.scene.name, __instance.gameObject.transform.position);
    //    return true;
    //}//

    /// <summary>
    /// </summary>
    public static bool APCheckpoint(Gameplay_CheckpointHandler __instance) {
        SendHandler("Checkpoint", __instance.gameObject.GetComponent<Syncer>().syncerID, __instance.gameObject.scene.name, __instance.gameObject.transform.position);
        return true;
    }

    /// <summary>
    /// </summary>
    public static bool APBeacon(BeaconHandler __instance) {
        SendHandler("Beacon", __instance.gameObject.GetComponent<Syncer>().syncerID, __instance.gameObject.scene.name, __instance.gameObject.transform.position);
        return true;
    }

    public static void SendHandler(string source, long syncerID, string areaName, Vector3 pos) {
        string sendString = $"{source}|[\"{NameToIDName(areaName)}:{syncerID}\"] = new LocationEnum(0, \"{GetAreaFromSceneName(areaName)}\"), // {pos}";
        Melon<APHelperClass>.Logger.Msg(sendString);
        //SendLocation(syncerID);
    }
    
    private static string NameToIDName(string fullSceneName) {
        // Input: "00_Tutorial_01_BAKED_BASE_HARD_HIGH_LAYER 0"
        // Output: "00_Tutorial_01"
        
        if (fullSceneName.Contains("_BAKED"))
        {
            fullSceneName = fullSceneName.Substring(0, fullSceneName.IndexOf("_BAKED"));
        }
    
        return fullSceneName;
    }
    
    private static string GetAreaFromSceneName(string cleanSceneName) {
        var parts = cleanSceneName.Split('_');
        return parts[1];
    }
    
    public static Data_Item GetItemById(string itemId) {
        Hashtable_Items hashtable = Hashtable_Items.getHashtable;
        if (hashtable == null) {
            Melon<APHelperClass>.Logger.Msg("Hashtable_Items.getHashtable is null.");
        }
        Data_Item item = hashtable.GetItemByID(itemId);
        if (item == null) {
            Melon<APHelperClass>.Logger.Msg($"Could not find item with id '{itemId}'.");
        }
        return item;
    }
}