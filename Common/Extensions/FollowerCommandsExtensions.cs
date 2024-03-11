namespace ShowFollowerJobTitles.Common.Extensions;

/// <summary>A class containing extension methods for the <see cref="FollowerCommands" /> enum.</summary>
public static class FollowerCommandsExtensions {
  /// <summary>Whether the <see cref="FollowerCommands" /> is a <see cref="FollowerRole" /> command.</summary>
  /// <param name="followerCommand">The follower command.</param>
  public static bool IsFollowerRoleCommand(this FollowerCommands followerCommand) {
    return followerCommand
      is FollowerCommands.WorshipAtShrine
      or FollowerCommands.CutTrees
      or FollowerCommands.Farmer_2
      or FollowerCommands.Study
      or FollowerCommands.ClearRubble
      or FollowerCommands.Build
      or FollowerCommands.ForageBerries
      or FollowerCommands.Cook_2
      or FollowerCommands.Janitor_2
      or FollowerCommands.Refiner_2
      or FollowerCommands.Undertaker
      or FollowerCommands.Brew;
  }

  /// <summary>Gets a <see cref="FollowerRole" /> based on the value of <see cref="FollowerCommands" />.</summary>
  /// <param name="followerCommand">The follower command.</param>
  public static FollowerRole FollowerCommandToRole(this FollowerCommands followerCommand) {
    return followerCommand switch {
      FollowerCommands.WorshipAtShrine => FollowerRole.Worshipper,
      FollowerCommands.CutTrees => FollowerRole.Lumberjack,
      FollowerCommands.Farmer_2 => FollowerRole.Farmer,
      FollowerCommands.Study => FollowerRole.Monk,
      FollowerCommands.ClearRubble => FollowerRole.StoneMiner,
      FollowerCommands.Build => FollowerRole.Builder,
      FollowerCommands.ForageBerries => FollowerRole.Forager,
      FollowerCommands.Cook_2 => FollowerRole.Chef,
      FollowerCommands.Janitor_2 => FollowerRole.Janitor,
      FollowerCommands.Refiner_2 => FollowerRole.Refiner,
      FollowerCommands.Undertaker => FollowerRole.Undertaker,
      FollowerCommands.Brew => FollowerRole.Bartender,
      _ => FollowerRole.Worker
    };
  }
}