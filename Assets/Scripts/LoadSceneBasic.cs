using UnityEngine;
using UnityEngine.SceneManagement;

// Load a Untiy Scene, allowing for scene string naming through the inspector.
public class LoadSceneBasic : MonoBehaviour
{
    public string sceneString;

    public void LoadUnitySceneBasic()
    {
        SceneManager.LoadScene(sceneString);
    }
}