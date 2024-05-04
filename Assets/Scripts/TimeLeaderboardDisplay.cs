using UnityEngine;
//using TMPro;
using System.IO; // Required for file operations

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TimeLeaderboardDisplay : MonoBehaviour
{
    //public TextMeshProUGUI leaderboardText;

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

        //leaderboardText.text = leaderboard;


#if UNITY_EDITOR // Only runs in the Unity Editor
        // File saving to your machine isn't really possible on a WebGL build.
        // You can only access your resource folder like this within the editor.
        // This will be used to record the fastest times

        string fileName = "leaderboard"; // Change to match your file.
        string path = "Assets/Resources/" + fileName + ".txt";

        if (File.Exists(path)) {
            File.WriteAllText(path, leaderboard); // Second argument should be your string
            Debug.Log(fileName + ".txt saved to: " + path);
        }
        else {
            Debug.Log(fileName + ".txt does not exist, creating file.");
            string dataToBeWritten = leaderboard;

            // Write some text to the leaderboard.txt file
            StreamWriter writer = new StreamWriter(path, true); // append is true
            writer.WriteLine(dataToBeWritten);
            writer.Close();

            // Print the text from the file
            Debug.Log("Done writing " + fileName + ".txt");

        }
        // Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
        TextAsset asset = (TextAsset)Resources.Load(fileName);// NEED TO CAST AS A TEXT ASSET SO IT CAN BE USED. TRY WITHOUT.
#endif
        /*
        This is what would display on my TestScene.
        Mess around with deleting your text file to see what happens!
        1. 0:3.00
        2. 0:3.40
        3. 0:3.47
        4. 0:3.99
        5. 0:4.02
        */
    }
}