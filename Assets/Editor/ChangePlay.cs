using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
public class ChangePlay : EditorWindow
{

    static string[] path;
    static string levelPath;

    [MenuItem("Play Main Scene/Play")]//Play-Stop, But From Prelaunch Scene %0")]
    public static void PlayFromPrelaunchScene()
    {
        if (EditorApplication.isPlaying == true)
        {
            EditorApplication.isPlaying = false;
            return;
        }

        path = EditorApplication.currentScene.Split(char.Parse("/"));
        path[path.Length - 1] = "Temp_" + path[path.Length - 1];
        levelPath = string.Join("/", path);
        EditorApplication.SaveScene(levelPath);

        EditorApplication.SaveCurrentSceneIfUserWantsTo();
        EditorApplication.OpenScene("Assets/Scenes/TeamSelection.unity");
        EditorApplication.isPlaying = true;
    }

    public static void testc()
    {
        EditorApplication.OpenSceneAdditive(levelPath);
    }
}
