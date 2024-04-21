using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class LoadTextFile : MonoBehaviour
{
    public string textAsset; // MyText.txt
    public TextMeshProUGUI textTarget;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadText()); 
    }
    
        IEnumerator LoadText()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, textAsset);
        Debug.Log("Loading:" + textAsset);
        Debug.Log("Filepath:" + filePath);


        using (UnityWebRequest www = UnityWebRequest.Get(filePath))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                textTarget.text = www.downloadHandler.text;
                // Or retrieve results as binary data
                //byte[] results = www.downloadHandler.data;
            }
        }
    }
}