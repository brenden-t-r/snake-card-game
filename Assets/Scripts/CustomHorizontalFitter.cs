using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomHorizontalFitter : MonoBehaviour
{
    [SerializeField] private Camera camera;
    private Vector3 startPosition;
    private Vector3 middlePosition;
    private Vector3 endPosition;
    private float stretchAmount = 0;
    private float elementMinWidth = 0.2f;
    public float elementPreferredWidth = 0.27f;
    private float elementHoverWidth = 2f;
    private float zOffset = 0.2f;
    private List<Transform> elements;
    private int numElements;

    private RectTransform rect;
    private readonly float HOVER_Y_OFFSET = 0.5f;
    private readonly float HOVER_Z_OFFSET = -4.9f;
    
    // Assuming anchor in center for parent and elements
    private void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        elements = new List<Transform>();
        foreach (Transform child in transform)
        {
            elements.Add(child);
        }
        Debug.Log(elements.Count);
        numElements = elements.Count;
        Refresh();
    }

    private void Update()
    {

        if (elements == null || elements.Count < 1)
        {
            Debug.Log("skip");
            return;
        }
        
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;
           // Debug.Log(objectHit.name);
            
            OnHoverEnter(objectHit);
            
            // Do something with the object that was hit by the raycast.
        }
        else
        {
            Debug.Log("Do unhover");
            OnHoverExit();
        }
        
        // Debug.Log(numElements + " - " + elements.Count);
        if (transform.childCount != elements.Count)
        {
            rect = gameObject.GetComponent<RectTransform>();
            elements = new List<Transform>();
            foreach (Transform child in transform)
            {
                elements.Add(child);
            }
            numElements = elements.Count;
            Refresh();
        }
    }

    private void Refresh()
    {
        Debug.Log("refresh");
        float width = rect.rect.width;
        float startX = transform.position.x - (width / 2);
        float endX = transform.position.x + (width / 2);
        float y = transform.position.y;
        float z = transform.position.z;
        startPosition = new Vector3(startX, y, z);
        endPosition = new Vector3(endX, y, z);
        middlePosition = transform.position;
        
        int numElements = elements.Count;
        float maxWidth = Math.Abs(endPosition.x - startPosition.x);
        float preferredWidth = numElements * elementPreferredWidth;

        float elementWidth = preferredWidth <= maxWidth
            ? elementPreferredWidth 
            : maxWidth / numElements;
        
        int middle = (numElements / 2);

        float offset = numElements % 2 == 0 ? elementWidth / 2 : 0;
        
        elements[middle].localPosition = new Vector3(0+offset, 0, 0);

        for (int i = middle-1, j = 1; i >= 0; i--, j++)
        {
            float x = 0 - (elementWidth * j) + offset;
            elements[i].localPosition = new Vector3(x, 0, -zOffset * j);
        }
        for (int i = middle+1, j = 1; i < numElements; i++, j++)
        {
            float x = 0 + (elementWidth * j) + offset;
            elements[i].localPosition = new Vector3(x, 0, zOffset * j);
        }
    }

    private int indexOfLastHover = -1;
    private float zIndexOfLastHover = -1;
    
    private void OnHoverEnter(Transform hit)
    {
        int index = elements.FindIndex(x => x.name.Equals(hit.name)); // TODO: Probably use pos instead
        Debug.Log(index);
        if (indexOfLastHover == index) return;
        // if (indexOfLastHover != -1)
        // {
        //     elements[indexOfLastHover].localPosition = new Vector3(
        //         elements[indexOfLastHover].localPosition.x,
        //         0,
        //         zIndexOfLastHover
        //     );
        // }
        OnHoverExit();
        indexOfLastHover = index;
        zIndexOfLastHover = elements[index].localPosition.z;
        elements[index].localPosition = new Vector3(
            elements[index].localPosition.x,
            0 + HOVER_Y_OFFSET, HOVER_Z_OFFSET
        );
        
        float maxWidth = Math.Abs(endPosition.x - startPosition.x);
        float preferredWidth = elements.Count * elementPreferredWidth;
        // float offsetWidth = preferredWidth <= maxWidth
        //     ? 0
        //     : (elementPreferredWidth/2);

        if (preferredWidth <= maxWidth)
        {
            return;
        }

        float offsetWidth = 0;
        float elementWidth = maxWidth / elements.Count;

        offsetWidth = Math.Abs(elements[1].localPosition.x -
                               (elements[0].localPosition.x + elements[0].localScale.x));
        
        

        for (int i = index-1, j = 1; i >= 0; i--, j++)
        {
            float x = elements[i].localPosition.x - (offsetWidth);
            elements[i].localPosition = new Vector3(x, 0, HOVER_Z_OFFSET + (zOffset * j));
        }
        for (int i = index+1, j = 1; i < numElements; i++, j++)
        {
            float x = elements[i].localPosition.x + (offsetWidth);
            elements[i].localPosition = new Vector3(x, 0, HOVER_Z_OFFSET + (zOffset * j));
        }
        
        // Adjust all cards to right to have position.x + ((hoverWidth-elementWidth)/2)
    }

    private void OnHoverExit()
    {
        if (indexOfLastHover == -1) return;
        if (indexOfLastHover != -1)
        {
            elements[indexOfLastHover].localPosition = new Vector3(
                elements[indexOfLastHover].localPosition.x,
                0,
                zIndexOfLastHover
            );
        }
        indexOfLastHover = -1;
        Refresh();
    }

}
