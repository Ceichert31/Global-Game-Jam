using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class AutomatedBuild
{
    public static void BuildWeb()
    {
        string[] scenes = new string[] { "Assets/Scenes/SampleScene.unity" };

        string buildPath = "builds/WebGL.html";

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = buildPath,
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
