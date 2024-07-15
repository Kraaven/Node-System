using System.Collections;
using System.Collections.Generic;
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
            Selection1.Next = Selection2;
            Selection2.Previous = Selection1;

            Selection1.RightButton.SetActive(false);
            Selection2.LeftButton.SetActive(false);
            Selection2.LeftCancel.SetActive(true);
            Selection1.UpdatePaths();
        }
    }
}
