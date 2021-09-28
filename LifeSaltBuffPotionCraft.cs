using System;
using BepInEx;
using HarmonyLib;
using catgocri;
using UnityEngine;


namespace catgocri.LifeSaltBuff.PotionCraft
{  
    // Reflection function is from RoboPhred https://github.com/RoboPhred
    public static class Reflection
    {
        public static void SetPrivateField<T>(object instance, string fieldName, T value)
        {
            var prop = instance.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            prop.SetValue(instance, value);
        }
    }

    [HarmonyPatch(typeof(SaltLife), "OnCauldronDissolve")]
    public static class MyPatch1 
    {
        static void Prefix(SaltLife __instance)
        {
            Reflection.SetPrivateField(__instance, "healthToAdd", 0.02f);
        } 
    }

    [BepInPlugin("net.catgocri.PotionCraft.LifeSaltBuff", "Life Salt is 5x Stronger PotionCraft", "1.0")]
    public class LifeSaltBuffPlugin : BaseUnityPlugin
    {   
      void Awake()
      {
        Debug.Log(@"[LifeSaltBuff]: Loaded");
        var harmony = new Harmony("net.catgocri.PotionCraft.LifeSaltBuff");
        harmony.PatchAll();
      }
    }
}