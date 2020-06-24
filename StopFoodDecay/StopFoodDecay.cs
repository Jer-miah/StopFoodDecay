using BepInEx;
using HarmonyLib;

namespace StopFoodDecay
{
    [BepInPlugin(ID, NAME, VERSION)]
    public class MyMod : BaseUnityPlugin
    {
        const string ID = "com.Jeremiah.StopFoodDecay";
        const string NAME = "Stop Food Decay";
        const string VERSION = "2.1";

        internal void Awake()
        {
            var harmony = new Harmony(ID);
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(Item), "OnAwake")]
    class Item_OnAwake_Patch
    {
        //private static readonly BepInEx.Logging.ManualLogSource PatchLogger = BepInEx.Logging.Logger.CreateLogSource("StopFoodDecay.Item_OnAwake_Patch");

        [HarmonyPostfix]
        public static void Postfix(ref Item __instance)
        {
            if (__instance && (__instance.IsFood || (string.Equals(__instance.TypeDisplay, "Food") && __instance.IsPerishable)))
            {
                Item.Destroy(__instance.GetComponent("Perishable"));
            }
        }
    }
}