using UnityEngine;
using UnityEngine.SceneManagement;

// Load a Untiy Scene, allowing for scene linking through the inspector.
public class LoadScene : MonoBehaviour
{
    public UnityEngine.Object scene;

    public void LoadUnityScene()
    {
        SceneManager.LoadScene(scene.name);
    }
}