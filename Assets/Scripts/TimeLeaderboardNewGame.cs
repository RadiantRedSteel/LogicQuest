using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLeaderboardNewGame : MonoBehaviour
{
    private List<float> leaderboardTimes;
    // Start is called before the first frame update
    void Start()
    {
        leaderboardTimes = new List<float>();

        // Check if the game has been started for the first time
        if (!PlayerPrefs.HasKey("GameStarted"))
        {
            // If it is the first time, set the placeholders and save them
            leaderboardTimes = new List<float> { 10f, 12f, 13f, 15f, 20f };
            for (int i = 0; i < leaderboardTimes.Count; i++)
            {
                PlayerPrefs.SetFloat("LeaderboardTime" + i, leaderboardTimes[i]);
            }

            // Set the GameStarted key so that this block of code won't be executed again
            PlayerPrefs.SetInt("GameStarted", 1);
            PlayerPrefs.Save();
            Debug.Log("GS LB Entry added at: " + Time.time);
        }
    }
}
