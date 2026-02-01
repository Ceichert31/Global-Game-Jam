using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class AutomatedBuild
{
    public static void BuildWeb()
    {
        string[] scenes = new string[]
        {
            "Assets/Scenes/TitleScreen.unity",
            "Assets/Scenes/MainGameScene.unity",
            "Assets/Scenes/GameOverScreen.unity",
        };

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = "builds/WebGL",
            target = BuildTarget.WebGL,
            options = BuildOptions.None,
        };

        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Disabled;
        PlayerSettings.WebGL.decompressionFallback = true;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);

        if (report.summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"BUILD SUCCEEDED | Size: {report.summary.totalSize} bytes");
        }
        else
        {
            Debug.LogError("BUILD FAILED");
            EditorApplication.Exit(-1);
        }
    }
}
