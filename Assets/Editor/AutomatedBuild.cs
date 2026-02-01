using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class AutomatedBuild
{
    public static void BuildWeb()
    {
        string[] scenes = new string[] { "Assets/Scenes/MainGameScene.unity" };

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = "builds/WebGL",
            target = BuildTarget.WebGL,
            options = BuildOptions.None,
        };

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
