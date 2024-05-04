using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public interface IApplicationQuitter
{
    void Quit();
}

#if UNITY_EDITOR
public class EditorApplicationQuitter : IApplicationQuitter
{
    public void Quit()
    {
        EditorApplication.isPlaying = false;
    }
}
#endif

public class RuntimeApplicationQuitter : IApplicationQuitter
{
    public void Quit()
    {
        Application.Quit();
    }
}

public class WebGLApplicationQuitter : IApplicationQuitter
{
    public void Quit()
    {
        Application.OpenURL("https://github.com/RadiantRedSteel/LogicQuest");
    }
}

public class MockApplicationQuitter : IApplicationQuitter
{
    public bool HasQuit { get; private set; }

    public void Quit()
    {
        HasQuit = true;
    }
}

public class ExitGame : MonoBehaviour
{
    private IApplicationQuitter quitter;

    private void Awake()
    {
        #if UNITY_EDITOR
            quitter = new EditorApplicationQuitter();
        #elif UNITY_WEBGL
            quitter = new WebGLApplicationQuitter();
        #else
            quitter = new RuntimeApplicationQuitter();
        #endif
    }

    public void SetQuitter(IApplicationQuitter newQuitter)
    {
        quitter = newQuitter;
    }

    public void ExitBuild()
    {
        quitter.Quit();
    }
}
