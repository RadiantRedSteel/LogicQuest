using UnityEngine;
using TMPro;

public class TimeLeaderboardDisplay : MonoBehaviour
{
    public TextMeshProUGUI leaderboardText;

    // Start is called before the first frame update
    void Start()
    {
        string leaderboard = "";

        // Load the leaderboard times from PlayerPrefs
        for (int i = 0; i < 5; i++)
        {
            float time = PlayerPrefs.GetFloat("LeaderboardTime" + i, float.MaxValue);
            string minutes = ((int)time / 60).ToString();
            string seconds = (time % 60).ToString("f2");

            leaderboard += (i + 1) + ". " + minutes + ":" + seconds + "\n";
        }

        leaderboardText.text = leaderboard;
    }
}