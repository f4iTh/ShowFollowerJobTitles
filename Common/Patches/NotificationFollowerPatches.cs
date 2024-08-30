using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using HarmonyLib;
using TMPro;

namespace ShowFollowerJobTitles.Common.Patches;

/// <summary>A class containing patches for <see cref="NotificationFollower" />.</summary>
[HarmonyPatch]
public class NotificationFollowerPatches {
  /// <summary>Gets the target method that should be patched.</summary>
  private static MethodBase TargetMethod() {
    return typeof(NotificationFollower).GetMethod("Localize", BindingFlags.NonPublic | BindingFlags.Instance);
  }

  /// <summary>The patch method for <see cref="NotificationFollower.Localize" />.</summary>
  /// <param name="__instance">The <see cref="NotificationFollower" /> instance.</param>
  [HarmonyPostfix]
  public static void Localize([SuppressMessage("ReSharper", "InconsistentNaming")] NotificationFollower __instance) {
    FieldInfo descriptionFieldInfo = typeof(NotificationFollower).GetField("_description", BindingFlags.NonPublic | BindingFlags.Instance);
    FieldInfo followerInfoFieldInfo = typeof(NotificationFollower).GetField("_followerInfo", BindingFlags.NonPublic | BindingFlags.Instance);
    FieldInfo notificationTypeFieldInfo = typeof(NotificationFollower).GetField("_type", BindingFlags.NonPublic | BindingFlags.Instance);
    if (descriptionFieldInfo == null || followerInfoFieldInfo == null || notificationTypeFieldInfo == null)
      return;

    FollowerInfo followerInfo = (FollowerInfo)followerInfoFieldInfo.GetValue(__instance);
    TextMeshProUGUI description = (TextMeshProUGUI)descriptionFieldInfo.GetValue(__instance);
    NotificationCentre.NotificationType notificationType = (NotificationCentre.NotificationType)notificationTypeFieldInfo.GetValue(__instance);

    // ignore if the notification wasn't about becoming old
    if (notificationType != NotificationCentre.NotificationType.BecomeOld || description == null)
      return;

    string roleIcon = FontImageNames.IconForRole(followerInfo.FollowerRole);
    if (string.IsNullOrWhiteSpace(roleIcon))
      return;

    description.text += $" ({roleIcon})";
  }
}
