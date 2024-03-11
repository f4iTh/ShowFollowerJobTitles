using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Lamb.UI.FollowerInteractionWheel;
using ShowFollowerJobTitles.Common.Extensions;
using TMPro;

namespace ShowFollowerJobTitles.Common.Patches;

/// <summary>A class containing patches for <see cref="UIFollowerWheelInteractionItem" /></summary>
[HarmonyPatch]
public class UIFollowerWheelInteractionItemPatches {
  /// <summary>The patch method for <see cref="UIFollowerWheelInteractionItem.Configure" />.</summary>
  /// <param name="__instance">The <see cref="UIFollowerWheelInteractionItem" /> instance.</param>
  /// <param name="follower">The follower.</param>
  /// <param name="commandItem">The command item.</param>
  [HarmonyPostfix]
  [HarmonyPatch(typeof(UIFollowerWheelInteractionItem), nameof(UIFollowerWheelInteractionItem.Configure))]
  public static void Configure([SuppressMessage("ReSharper", "InconsistentNaming")] UIFollowerWheelInteractionItem __instance, ref Follower follower, ref CommandItem commandItem) {
    if (commandItem == null || !commandItem.Command.IsFollowerRoleCommand())
      return;

    FieldInfo titleFieldInfo = typeof(UIFollowerWheelInteractionItem).GetField("_title", BindingFlags.NonPublic | BindingFlags.Instance);
    FieldInfo textInfo = typeof(UIFollowerWheelInteractionItem).GetField("_text", BindingFlags.NonPublic | BindingFlags.Instance);
    if (titleFieldInfo == null || textInfo == null)
      return;

    FollowerRole followerRole = commandItem.Command.FollowerCommandToRole();
    int jobCount = DataManager.Instance.Followers.Count(followerInfo => followerInfo.FollowerRole == followerRole);
    string title = (string)titleFieldInfo.GetValue(__instance);
    TextMeshProUGUI text = (TextMeshProUGUI)textInfo.GetValue(__instance);

    // highlight current role
    if (followerRole == follower.Brain.Info.FollowerRole) {
      // highlight the role icon in yellow (if available)
      if (commandItem.IsAvailable(follower))
        text.text = $"<color=yellow>{text.text}</color>";

      // highlight the role title in yellow
      titleFieldInfo.SetValue(__instance, $"<color=yellow>{title} ({jobCount})</color>");
      return;
    }

    // handle unavailable roles 
    if (!commandItem.IsAvailable(follower)) {
      // builder seems to work differently; add +1 count to unselectable roles other than builder
      titleFieldInfo.SetValue(__instance, $"{title} ({jobCount}{(followerRole == FollowerRole.Builder ? "" : " + 1")})");
      return;
    }

    titleFieldInfo.SetValue(__instance, $"{title} ({jobCount} + 1)");
  }
}