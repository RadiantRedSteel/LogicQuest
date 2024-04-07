using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
#endif

public class ExitGame : MonoBehaviour
{
    public void ExitBuild()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #elif UNITY_WEBGL
			// Insert your GitHub Project Link within the string
            Application.OpenURL("https://github.com/RadiantRedSteel/LogicQuest");
        #else
            Application.Quit();
        #endif
    }
}