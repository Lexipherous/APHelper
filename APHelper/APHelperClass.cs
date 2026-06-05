
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
            MethodInfo patchPickableItem = AccessTools.Method(typeof(Patches.Patches), "CustomPickableItem");
            HarmonyInstance.Patch(originalPickableItem, new HarmonyMethod(patchPickableItem));
            
            MethodInfo originalInteractableAbsorbable = AccessTools.Method(typeof(Gameplay_InteractableAbsorbable), "OnInteract");
            MethodInfo patchInteractableAbsorbable = AccessTools.Method(typeof(Patches.Patches), "CustomInteractableAbsorbable");
            HarmonyInstance.Patch(originalInteractableAbsorbable, new HarmonyMethod(patchInteractableAbsorbable));
            
            MethodInfo originalObjectAbsorbHandler = AccessTools.Method(typeof(Gameplay_ObjectAbsorbHandler), "OnSelfPullStart");
            MethodInfo patchObjectAbsorbHandler = AccessTools.Method(typeof(Patches.Patches), "CustomObjectAbsorbHandler");
            HarmonyInstance.Patch(originalObjectAbsorbHandler, new HarmonyMethod(patchObjectAbsorbHandler));
            Melon<APHelperClass>.Logger.Msg("[APHelper] Initialized! :D");
        }
    }
}
