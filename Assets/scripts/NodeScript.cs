using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeScript : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private UsedButton PressedButton;

    public static List<NodeScript> Nodes = new List<NodeScript>();
    public static List<RectTransform> NodeTransforms = new List<RectTransform>();

    private Vector2 startPosition;
    private Vector2 startMousePosition;

    [Header("Top Connectors")] 
    public NodeScript Previous;
    public NodeScript Next;
    public PathComponent ConnectionPath;

    [Header("Buttons")] 
    public GameObject LeftButton;
    public GameObject RightButton;
    public GameObject LeftCancel;
    
    

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    private void Start()
    {
        Nodes.Add(this);
        NodeTransforms.Add(rectTransform);
        LeftCancel.SetActive(false);

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        PressedButton = eventData.button switch
        {
            PointerEventData.InputButton.Left => UsedButton.Left,
            PointerEventData.InputButton.Right => UsedButton.Right,
            PointerEventData.InputButton.Middle => UsedButton.Middle,
            _ => UsedButton.Left
        };

        startPosition = rectTransform.anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, canvas.worldCamera, out startMousePosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (PressedButton == UsedButton.Left)
        {
            Vector2 currentMousePosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, canvas.worldCamera, out currentMousePosition))
            {
                Vector2 difference = currentMousePosition - startMousePosition;
                rectTransform.anchoredPosition = startPosition + difference;
            }
        }

        if (Next != null)
        {
            UpdatePaths();  
        }

        if (Previous != null)
        {
            Previous.UpdatePaths();
        }


    }

    public void DeleteConnection()
    {
        Destroy(Previous.ConnectionPath.gameObject);
        Previous.RightButton.SetActive(true);
        LeftCancel.SetActive(false);
        LeftButton.SetActive(true);
        
        
        Previous.Next = null;
        Previous.ConnectionPath = null;
        Previous = null;
    }

    public void RequestConnection()
    {
        GameManager.SetAsFirstSelection(this);
    }

    public void RecieveConnection()
    {
        if (GameManager.Selection1 != this)
        {
            GameManager.SetSecondSelection(this);
        }
    }



    public void OnEndDrag(PointerEventData eventData)
    {
        // Optional: Implement any logic needed when dragging ends.
    }

    public void UpdatePaths()
    {
        if (ConnectionPath != null )
        {
            ConnectionPath.UpdateLine();
        }
        
    }
}

public enum UsedButton
{
    Left,
    Right,
    Middle
}