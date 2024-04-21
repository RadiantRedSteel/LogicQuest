using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadTextFileRes : MonoBehaviour
{
    public TMP_Text textTarget;
    // Start is called before the first frame update
    void Start()
    {
        TextAsset myTextFile = (TextAsset)Resources.Load("MyText");
        string myText = myTextFile.text;
        textTarget.text = myText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
