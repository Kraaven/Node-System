using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PathComponent PathPrefab;

    public static PathComponent Path;

    public static NodeScript Selection1;

    public static NodeScript Selection2;
    // Start is called before the first frame update
    void Start()
    {
        Path = PathPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string Code = "";
            Code += "#include<iostream>\n";
            Code += "using namespace std;\n";
            Code += "int main(){\n";

            NodeScript head = GameObject.Find("Start").GetComponent<NodeScript>();
            Debug.Log("Starting node traversal from: " + head.gameObject.name);

            while (head != null)
            {
                Debug.Log("Visiting node: " + head.gameObject.name);

                if (head.TryGetComponent<CodeAction>(out CodeAction codeAction))
                {
                    Debug.Log("Found CodeAction component on: " + head.gameObject.name);
                    Code += codeAction.CodeActivity() + "\n";
                }
                else
                {
                    Debug.Log("No CodeAction component found on: " + head.gameObject.name);
                    Code += "\n";
                }

                head = head.Next;
            }

            string endmsg;
            endmsg = "--------------------";
            Code+= $"cout<<\" {endmsg} \"<<endl;";
            endmsg = "This Code is to ensure the Window stays Open.... (Press Enter to exit)";
            Code+= $"cout<<\" {endmsg} \"<<endl;";
            Code += "char test;";
            Code += "cin>>test;";
            Code += "}\n";

            File.WriteAllText(Application.dataPath + "/test.cpp", Code);
            Debug.Log("Code written to file: " + Application.dataPath + "/test.cpp");
        }
    }

    public static void SetAsFirstSelection(NodeScript S1)
    {
        Selection1 = null;
        Selection2 = null;

        Selection1 = S1;
    }

    public static void SetSecondSelection(NodeScript S2)
    {
        if (Selection1 != null)
        {
            Selection2 = S2;
            Selection1.ConnectionPath = Instantiate(Path).Init(Selection1,Selection2);
            Selection1.ConnectionPath.Setcolor(Color.red);
            Selection1.Next = Selection2;
            Selection2.Previous = Selection1;

            Selection1.RightButton.SetActive(false);
            Selection2.LeftButton.SetActive(false);
            Selection2.LeftCancel.SetActive(true);
            Selection1.UpdatePaths();
        }
    }

    
}
