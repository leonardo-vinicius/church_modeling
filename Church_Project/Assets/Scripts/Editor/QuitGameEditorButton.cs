using UnityEditor;
using UnityEngine;

public class QuitGameEditorButton : MonoBehaviour
{
    [MenuItem("Custom/Quit Game")]
    static void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
