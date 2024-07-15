using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathComponent : MonoBehaviour
{
    public RectTransform P1;

    public RectTransform P2;

    private Vector3[] points;

    public LineRenderer line;

    public static List<PathComponent> Paths = new List<PathComponent>();
    // Start is called before the first frame update
    void Start()
    {
        points = new Vector3[2];
        line = GetComponent<LineRenderer>();
        Paths.Add(this);
    }
    void Update()
    {
        
    }

    public PathComponent(NodeScript Origin, NodeScript Destination)
    {
        P1 = Origin.transform.GetChild(1).GetComponent<RectTransform>();
        P2 = Destination.transform.GetChild(0).GetComponent<RectTransform>();
    }
    
    public PathComponent Init(NodeScript Origin, NodeScript Destination)
    {
        P1 = Origin.transform.GetChild(1).GetComponent<RectTransform>();
        P2 = Destination.transform.GetChild(0).GetComponent<RectTransform>();

        return this;
    }

    public void UpdateLine()
    {
        points[0] = P1.TransformPoint(P1.position);
        points[1] = P2.TransformPoint(P2.position);
        
        line.SetPositions(points);
    }
}
