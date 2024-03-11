using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using HarmonyLib;
using TMPro;

namespace ShowFollowerJobTitles.Common.Patches;

/// <summary>A class containing patches for <see cref="UIFollowerName" />.</summary>
[HarmonyPatch]
public class UIFollowerNamePatches {
  /// <summary>Gets the target method that should be patched.</summary>
  private static MethodBase TargetMethod() {
    return typeof(UIFollowerName).GetMethod("SetText", BindingFlags.NonPublic | BindingFlags.Instance);
  }

  /// <summary>The patch method for <see cref="UIFollowerName.SetText" />.</summary>
  /// <param name="__instance">The <see cref="UIFollowerName" /> instance.</param>
  [HarmonyPostfix]
  public static void SetText([SuppressMessage("ReSharper", "InconsistentNaming")] UIFollowerName __instance) {
    FieldInfo nameTextFieldInfo = typeof(UIFollowerName).GetField("nameText", BindingFlags.NonPublic | BindingFlags.Instance);
    FieldInfo followerFieldInfo = typeof(UIFollowerName).GetField("follower", BindingFlags.NonPublic | BindingFlags.Instance);
    if (nameTextFieldInfo == null || followerFieldInfo == null)
      return;

    TMP_Text nameText = (TMP_Text)nameTextFieldInfo.GetValue(__instance);
    Follower follower = (Follower)followerFieldInfo.GetValue(__instance);
    string roleIcon = FontImageNames.IconForRole(follower.Brain.Info.FollowerRole);
    int roleIconIndex = nameText.text.IndexOf(roleIcon, StringComparison.Ordinal);

    // do not display role for old people
    if (follower.Brain.Info.OldAge || follower.Brain.HasThought(Thought.OldAge)) {
      // remove role string after becoming old
      if (roleIconIndex > 0)
        nameText.text = nameText.text.Remove(roleIconIndex, roleIcon.Length - 1);

      return;
    }

    // job title is already applied or should not be applied
    if (string.IsNullOrWhiteSpace(roleIcon) || roleIconIndex > 0)
      return;

    nameText.text += $" ({roleIcon})";
  }
}