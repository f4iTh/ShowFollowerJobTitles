using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using HarmonyLib;

namespace ShowFollowerJobTitles.Common.Patches;

/// <summary>A class containing patches for the <see cref="FollowerInformationBox" />.</summary>
[HarmonyPatch]
public class FollowerInformationBoxPatches {
  /// <summary>Gets the target method that should be patched.</summary>
  private static MethodBase TargetMethod() {
    return typeof(FollowerInformationBox).GetMethod("ConfigureImpl", BindingFlags.NonPublic | BindingFlags.Instance);
  }

  /// <summary>The patch method for <see cref="FollowerInformationBox.ConfigureImpl" />.</summary>
  /// <param name="__instance">The <see cref="FollowerInformationBox" /> instance.</param>
  [HarmonyPostfix]
  public static void ConfigureImpl([SuppressMessage("ReSharper", "InconsistentNaming")] FollowerInformationBox __instance) {
    if (__instance.FollowerRole == null || __instance.FollowerInfo.OldAge || __instance.FollowerInfo.HasThought(Thought.OldAge))
      return;

    string roleIcon = FontImageNames.IconForRole(__instance.followBrain.Info.FollowerRole);
    if (string.IsNullOrWhiteSpace(roleIcon))
      return;

    __instance.FollowerRole.text += $" | {roleIcon}";
  }
}