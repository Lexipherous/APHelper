
using HarmonyLib;
using MelonLoader;
using Il2Cpp;
using Il2CppInterop.Runtime;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ResourceManagement.Util;
using UnityEngine.UI;


[assembly: MelonInfo(typeof(APHelper.APHelperClass), "APHelper", "0.1.0", "Lexipherous")]

namespace APHelper {
    public partial class APHelperClass : MelonMod {
        public override void OnInitializeMelon() {
            MethodInfo originalPickableItem = AccessTools.Method(typeof(Gameplay_PickableItem), "Pickup");
            MethodInfo patchPickableItem = AccessTools.Method(typeof(Patches.Patches), "APItemPickup");
            HarmonyInstance.Patch(originalPickableItem, new HarmonyMethod(patchPickableItem));
                
            MethodInfo originalInteractableAbsorbable = AccessTools.Method(typeof(Gameplay_InteractableAbsorbable), "OnInteract");
            MethodInfo patchInteractableAbsorbable = AccessTools.Method(typeof(Patches.Patches), "APInteractAbsorb");
            HarmonyInstance.Patch(originalInteractableAbsorbable, new HarmonyMethod(patchInteractableAbsorbable));
            
            MethodInfo originalObjectAbsorbHandler = AccessTools.Method(typeof(Gameplay_ObjectAbsorbHandler), "OnSelfPullStart");
            MethodInfo patchObjectAbsorbHandler = AccessTools.Method(typeof(Patches.Patches), "APObjectAbsorb");
            HarmonyInstance.Patch(originalObjectAbsorbHandler, new HarmonyMethod(patchObjectAbsorbHandler));
            
            MethodInfo originalAbsorbableCluster = AccessTools.Method(typeof(AbsorbableCluster_Event), "ReleaseContent");
            MethodInfo patchAbsorbableCluster = AccessTools.Method(typeof(Patches.Patches), "APMarahBloodroot");
            HarmonyInstance.Patch(originalAbsorbableCluster, new HarmonyMethod(patchAbsorbableCluster));
            
            // NPC Hook
            //MethodInfo originalProfile_NPC = AccessTools.Method(typeof(Profile_NPC), "getInitialConversationID");
            //MethodInfo patchProfile_NPC = AccessTools.Method(typeof(LocationSend), "AP_NPC");
            //HarmonyInstance.Patch(originalProfile_NPC, new HarmonyMethod(patchProfile_NPC));
            
            // Checkpoint Hook
            MethodInfo originalCheckpointHandler = AccessTools.Method(typeof(Gameplay_CheckpointHandler), "OnInteract");
            MethodInfo patchCheckpointHandler = AccessTools.Method(typeof(Patches.Patches), "APCheckpoint");
            HarmonyInstance.Patch(originalCheckpointHandler, new HarmonyMethod(patchCheckpointHandler));
            
            // Beacon Hook
            MethodInfo originalBeaconHandler = AccessTools.Method(typeof(BeaconHandler), "OnInteract");
            MethodInfo patchBeaconHandler = AccessTools.Method(typeof(Patches.Patches), "APBeacon");
            HarmonyInstance.Patch(originalBeaconHandler, new HarmonyMethod(patchBeaconHandler));
            Melon<APHelperClass>.Logger.Msg("Wohoo! Initialized! :D");
        }
    }
}
