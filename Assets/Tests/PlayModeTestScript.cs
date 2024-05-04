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
using TMPro;
using UnityEngine.InputSystem.LowLevel;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UIElements;

public class PlayModeTestScript
{
    [SetUp]
    public void Setup()
    {
        //CrossPlatformInputManager.SwitchActiveInputMethod(CrossPlatformInputManager.ActiveInputMethod.Hardware);
    }

    [TearDown]  // THIS SIGNALS THAT IT IS THE TEARDOWN AND NOT A TEST
    public void Teardown()
    {
        //SceneManager.LoadScene("IntroScene");
    }

    [UnityTest]
    public IEnumerator PlayButtonOpensGameScene()
    {
        // Wait for the scene to load
        SceneManager.LoadScene("IntroScene");
        yield return new WaitForSeconds(1f);

        // Find the Play button
        GameObject playButton = GameObject.Find("startGameButton");

        // Assert that the button is not null
        Assert.IsNotNull(playButton, "Play button not found in Intro scene");

        // Simulate a button click
        playButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();

        // Wait for the scene transition
        yield return new WaitForSeconds(1f);

        // Assert that the current scene is now the Game scene
        Assert.AreEqual("Level01Scene", SceneManager.GetActiveScene().name, "Game scene not loaded after clicking Play button");
        Debug.Log("Level01Scene loaded after clicking Play");
        yield return new WaitForSeconds(0.1f);
    }

    [UnityTest]
    public IEnumerator ExitButtonTest()
    {
        // Wait for the scene to load
        SceneManager.LoadScene("IntroScene");
        yield return new WaitForSeconds(1f);

        // Use the mock quitter for testing
        MockApplicationQuitter quitter = new MockApplicationQuitter();

        // Find the Exit button
        GameObject exitButton = GameObject.Find("exitGameButton");
        Assert.IsNotNull(exitButton, "Exit Game button not found in same object");

        // Get the ExitGame script attached to the exit button
        ExitGame exitGame = exitButton.GetComponent<ExitGame>();

        // Replace the real quitter with the mock one
        exitGame.SetQuitter(quitter);

        // Simulate a button click
        exitButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();

        // Wait for scene transition
        yield return new WaitForSeconds(.5f);

        // Assert that game closes
        Assert.IsTrue(quitter.HasQuit, "Application did not stop playing after clicking Exit Game button");
        Debug.Log("Mock exit test successful");
        yield return new WaitForSeconds(0.1f);
    }


    [UnityTest]
    public IEnumerator FindThePlayer()
    {
        // Wait for the scene to load
        SceneManager.LoadScene("Level01Scene");
        yield return new WaitForSeconds(1f);

        // Find the CharacterRobotBoy in the scene
        GameObject player = GameObject.Find("CharacterRobotBoy");
        Assert.IsNotNull(player, "CharacterRobotBoy object not found");
        Debug.Log("Player found");
        yield return new WaitForSeconds(0.1f);
    }

    // Could not figure out how to make this work with Standard Assets CrossPlatformInput
    // Test fails on my machine
    [UnityTest]
    public IEnumerator PlayerMovement()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        CrossPlatformInputManager.SwitchActiveInputMethod(CrossPlatformInputManager.ActiveInputMethod.Hardware);

        // Wait for the scene to load
        SceneManager.LoadScene("Level01Scene");
        yield return new WaitForSeconds(1f);

        // Find the CharacterRobotBoy in the scene
        GameObject player = GameObject.Find("CharacterRobotBoy");
        Assert.IsNotNull(player, "CharacterRobotBoy object not found");
        Debug.Log("Player found");

        // Get the player's initial position
        Vector3 initialPosition = player.transform.position;

        // Simulate 'D' key press
        InputSystem.QueueStateEvent(keyboard, new KeyboardState(Key.D));
        InputSystem.Update();

        yield return new WaitForSeconds(1f);

        // Simulate 'D' key release
        InputSystem.QueueStateEvent(keyboard, new KeyboardState());
        InputSystem.Update();

        // Check if the player's position has changed in the positive x direction
        Assert.Greater(player.transform.position.x, initialPosition.x, "Player did not move right when 'D' was pressed");
        Debug.Log("Player can move right");
        yield return new WaitForSeconds(0.1f);
    }


    [UnityTest]
    public IEnumerator GameOverOnKillzoneCollision()
    {
        // Wait for the scene to load
        SceneManager.LoadScene("Level01Scene");
        yield return new WaitForSeconds(1.5f);

        // Find the Killzone in the scene
        GameObject deathObject = GameObject.Find("Killzone");
        Assert.IsNotNull(deathObject, "Killzone object not found");

        // Find the CharacterRobotBoy in the scene
        GameObject player = GameObject.Find("CharacterRobotBoy");
        Assert.IsNotNull(player, "CharacterRobotBoy object not found");

        // Find the timeText and store time before teleport
        GameObject timeText = GameObject.Find("timeText");
        Assert.IsNotNull(timeText, "timeText object not found");
        string beforeTime = timeText.GetComponent<TextMeshProUGUI>().text;
        Debug.Log("Before Time: " + beforeTime);

        // Teleport player to killzone
        player.transform.position = deathObject.transform.position;
        yield return new WaitForSeconds(1f);

        // Find the timeText and store time after reset 
        GameObject timeText2 = GameObject.Find("timeText");
        Assert.IsNotNull(timeText2, "timeText object not found");
        string afterTime = timeText2.GetComponent<TextMeshProUGUI>().text;
        Debug.Log("After Time: " + afterTime);

        // Convert time1 to seconds
        string[] splitTime1 = beforeTime.Split(':');
        float seconds1 = float.Parse(splitTime1[0]) * 60 + float.Parse(splitTime1[1]);

        // Convert time2 to seconds
        string[] splitTime2 = afterTime.Split(':');
        float seconds2 = float.Parse(splitTime2[0]) * 60 + float.Parse(splitTime2[1]);

        // Compare times
        Assert.GreaterOrEqual(seconds1, seconds2, SceneManager.GetActiveScene().name, "Time did not reset.");
        Debug.Log("Timer reset due to game over");
        yield return new WaitForSeconds(0.1f);
    }

    [UnityTest]
    public IEnumerator GameWinOnFinishCollision()
    {
        // Wait for the scene to load
        SceneManager.LoadScene("Level01Scene");
        yield return new WaitForSeconds(1.5f);

        // Find the CharacterRobotBoy in the scene
        GameObject player = GameObject.Find("CharacterRobotBoy");
        Assert.IsNotNull(player, "CharacterRobotBoy object not found");

        // Find the FinishLine in the scene
        GameObject finishLine = GameObject.Find("FinishLine");
        Assert.IsNotNull(finishLine, "FinishLine object not found");

        // We don't want to record a fastest time since we are teleporting the player
        // Soooo let's speed up time! A decent solution for how my scripts work.
        Time.timeScale = 5.0f;
        yield return new WaitForSeconds(4.0f);
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(0.1f);

        // Teleport player to finish line
        player.transform.position = finishLine.transform.position;
        yield return new WaitForSeconds(1.0f);

        // Find the timeCompletionText in the scene
        GameObject timeCompletionText = GameObject.Find("timeCompletionText");
        string timeCompletionString = timeCompletionText.GetComponent<TextMeshProUGUI>().text;

        // Assert that timeCompletionText starts with "Level completed in:"
        Assert.IsTrue(timeCompletionString.StartsWith("Level completed in:"), "The text does not start with 'Level completed in:");
        Debug.Log("Level completed");
        yield return new WaitForSeconds(0.1f);
    }
}