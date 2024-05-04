using TMPro;
using UnityEngine;

public class LoadTextFileRes : MonoBehaviour
{
    public TMP_Text textTarget; // Output target
    public string fileLoadName; // Don't include .txt in name
    // Start is called before the first frame update
    void Start()
    {
        // Loads from Assets/Resources
        TextAsset myTextFile = (TextAsset)Resources.Load(fileLoadName);
        string myText = myTextFile.text;
        textTarget.text = myText;
        Debug.Log(fileLoadName + ".txt" + " loaded!");
    }
}