namespace Chickensoft.GodotEnv.Features.Godot.Models;

using Chickensoft.GodotEnv.Common.Clients;
using Chickensoft.GodotEnv.Common.Utilities;
using global::GodotEnv.Common.Utilities;

public class MacOS : Unix {
  public MacOS(
    ISystemInfo systemInfo,
    IFileClient fileClient,
    IComputer computer,
    IVersionStringConverter versionStringConverter
  )
    : base(systemInfo, fileClient, computer, versionStringConverter) { }

  public override string GetInstallerNameSuffix(DotnetSpecificGodotVersion version) {
    var hasUniversalSuffix =
      version.Number.Major > 3 ||
        (
          version.Number.Major == 3 &&
          version.Number.Minor > 3 &&
          version.Number.Patch > 2
        );
    var universalSuffix = hasUniversalSuffix ? ".universal" : ".64";

    return $"{(version.IsDotnet ? "_mono" : "")}_{(version.Number.Major == 3 ? "osx" : "macos")}{universalSuffix}";
  }

  public override void Describe(ILog log) => log.Info("🍏 Running on macOS");

  public override string GetRelativeExtractedExecutablePath(
    DotnetSpecificGodotVersion version
  ) => $"Godot{(version.IsDotnet ? "_mono" : "")}.app/Contents/MacOS/Godot";

  public override string GetRelativeGodotSharpPath(
    DotnetSpecificGodotVersion version
  ) => "Godot_mono.app/Contents/Resources/GodotSharp";
}
