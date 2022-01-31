/*#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class ForcePlay
{
    static ForcePlay()
    {
        EditorApplication.playModeStateChanged += LoadDefaultScene;
    }

    static void LoadDefaultScene(PlayModeStateChange state)
    {
        if(state == PlayModeStateChange.ExitingEditMode)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }

        if(state == PlayModeStateChange.EnteredPlayMode)
        {
            if (EditorSceneManager.GetActiveScene().buildIndex == 0) return;
            EditorSceneManager.LoadScene(0);
        }
    }
}
#endif*/