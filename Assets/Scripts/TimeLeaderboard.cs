using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLeaderboard : MonoBehaviour
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
        else
        {
            // If it's not the first time, load the leaderboard times from PlayerPrefs
            for (int i = 0; i < 5; i++)
            {
                leaderboardTimes.Add(PlayerPrefs.GetFloat("LeaderboardTime" + i));
                //Debug.Log("Start LB Entry added at: " + Time.time + " index: " + i + " value: " + PlayerPrefs.GetFloat("LeaderboardTime" + i, float.MaxValue));
            }
        }
    }

    public void CheckTime(float time)
    {
        Debug.Log("CheckTime(float time) has been called!");
        // Check if the time is faster than any of the leaderboard times
        for (int i = 0; i < leaderboardTimes.Count; i++)
        {
            if (time < leaderboardTimes[i])
            {
                // If it is, insert the time at this position and remove the last time
                Debug.Log("Insert Entry added at: " + time + " index: " + i + " value: " + PlayerPrefs.GetFloat("LeaderboardTime" + i, float.MaxValue));

                leaderboardTimes.Insert(i, time);
                leaderboardTimes.RemoveAt(leaderboardTimes.Count - 1);

                // Save the new leaderboard times
                for (int j = 0; j < leaderboardTimes.Count; j++)
                {
                    PlayerPrefs.SetFloat("LeaderboardTime" + j, leaderboardTimes[j]);
                    Debug.Log("CT Entry added at: " + Time.time + " index: " + j + " value: " + PlayerPrefs.GetFloat("LeaderboardTime" + j, float.MaxValue));
                }
                PlayerPrefs.Save();
                break;
            }
        }
    }
}
