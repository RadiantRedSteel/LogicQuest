using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;// so we can click on UI elements
using UnityEngine.TestTools;
using UnityEngine.InputSystem;// so we can use mouse and keyboard
using UnityEngine.SceneManagement; // for loading and restarting game
using System; // for conversion between integers and text if needed.
using UnityStandardAssets._2D;


public class PlayModeTestScript
{
    Mouse mouse;
    Keyboard keyboard;

    [SetUp]
    public void Setup()
    {
        Debug.Log("Loading scene...");
        SceneManager.LoadScene("Level01Scene");
    }

    [TearDown]  // THIS SIGNALS THAT IT IS THE TEARDOWN AND NOT A TEST
    public void Teardown()
    {
        SceneManager.LoadScene("Level01Scene");
    }

    [UnityTest]
    public IEnumerator FindThePlayer()
    {
        yield return new WaitForSeconds(0.1f);
    }

    [UnityTest]
    public IEnumerator PlayerMovement()
    {
        yield return new WaitForSeconds(0.1f);
    }

    [UnityTest]
    public IEnumerator GameOverOnKillzoneCollision()
    {
        yield return new WaitForSeconds(0.1f);
    }

    [UnityTest]
    public IEnumerator GameWinOnFinishCollision()
    {
        yield return new WaitForSeconds(0.1f);
    }

    [UnityTest]
    public IEnumerator TimerUIUpdating()
    {
        yield return new WaitForSeconds(0.1f);
    }

    [UnityTest]
    public IEnumerator ReturnToMainMenuClick()
    {
        yield return new WaitForSeconds(0.1f);
    }
}