using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrintNode : MonoBehaviour, CodeAction
{

    public TMP_InputField text;
    public string CodeActivity()
    {
        Debug.Log("CodeActivity called, text: " + text.text);
        return $"cout<<\"{text.text}\"<<endl;";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
