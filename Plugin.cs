using BepInEx;
using HarmonyLib;

namespace ShowFollowerJobTitles;

/// <summary>The plugin entry point.</summary>
[BepInPlugin("com.f4iTh.COTL.ShowFollowerJobTitles", "Show Follower Job Titles", "1.0.0")]
public class Plugin : BaseUnityPlugin {
  /// <summary>The Harmony instance.</summary>
  private readonly Harmony _harmony = new("com.f4iTh.COTL.ShowFollowerJobTitles");

  /// <inheritdoc cref="Plugin.Awake" />
  private void Awake() { }

  /// <inheritdoc cref="Plugin.OnEnable" />
  private void OnEnable() {
    this._harmony.PatchAll();
  }

  /// <inheritdoc cref="Plugin.OnDisable" />
  private void OnDisable() {
    this._harmony.UnpatchSelf();
  }
}