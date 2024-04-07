using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeInLevel : MonoBehaviour
{
    public TimeLeaderboard timeLeaderboard;

    bool levelCompleted = false;
    private float startTime;
    private float levelTime;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI timeCompletionText;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelTime == 0)
        {
            float currentTime = Time.time - startTime;
            string minutes = ((int)currentTime / 60).ToString();
            string seconds = (currentTime % 60).ToString("f2");

            timeText.text = minutes + ":" + seconds; // Add this line
        }
    }

/*
The ToString("f2") format specifier is used to format the seconds as a floating-point number with 2 digits after the decimal point. 
This will give you a nicely formatted time string in the format MM:SS.ss. 
If you want to display the time without the fractional part of the seconds, you can use ToString("00") instead. 
This will give you a time string in the format MM:SS.
*/
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&& !levelCompleted)
        {
            levelTime = Time.time - startTime;

            string minutes = ((int)levelTime / 60).ToString();
            string seconds = (levelTime % 60).ToString("f2");
            Debug.Log("Trigger entered at: " + Time.time + " value: " + minutes + ":" + seconds);

            timeCompletionText.text = "Level completed in: " + minutes + ":" + seconds;

            timeLeaderboard.CheckTime(levelTime);

            // Set levelCompleted to true after adding the time
            levelCompleted = true;
        }
    }
}