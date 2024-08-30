using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using HarmonyLib;
using ShowFollowerJobTitles.Common.Extensions;

namespace ShowFollowerJobTitles.Common.Patches
{
    [HarmonyPatch]
    public class FollowerInformationBoxPatches
    {
        private static MethodBase TargetMethod()
        {
            return typeof(FollowerInformationBox).GetMethod("ConfigureImpl", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [HarmonyPostfix]
        public static void ConfigureImpl([SuppressMessage("ReSharper", "InconsistentNaming")] FollowerInformationBox __instance)
        {
            if (__instance == null || __instance.FollowerRole == null || __instance.followBrain == null || __instance.followBrain.Info == null)
                return;

            // Check if the follower is not a child (assuming children don't have jobs)
            if (__instance.followBrain.Info.CursedState != Thought.Child)
            {
                FollowerRole role = __instance.followBrain.Info.FollowerRole;
                string jobTitle = GetJobTitle(role);
                if (!string.IsNullOrEmpty(jobTitle))
                {
                    __instance.FollowerRole.text += $" | {jobTitle}";
                }
            }
        }

        private static string GetJobTitle(FollowerRole role)
        {
            return role switch
            {
                FollowerRole.Worshipper => "Worshipper",
                FollowerRole.Lumberjack => "Lumberjack",
                FollowerRole.Farmer => "Farmer",
                FollowerRole.Monk => "Monk",
                FollowerRole.StoneMiner => "Stone Miner",
                FollowerRole.Builder => "Builder",
                FollowerRole.Forager => "Forager",
                FollowerRole.Chef => "Chef",
                FollowerRole.Janitor => "Janitor",
                FollowerRole.Refiner => "Refiner",
                FollowerRole.Undertaker => "Undertaker",
                FollowerRole.Bartender => "Bartender",
                FollowerRole.Worker => "Worker",
                _ => string.Empty
            };
        }
    }
}
