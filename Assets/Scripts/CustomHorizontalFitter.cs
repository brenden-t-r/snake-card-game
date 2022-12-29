using System;
using System.Collections.Generic;
using UnityEngine;

// This is a custom implementation similar to the built-in Horizontal Element Group built for a hand of cards.
// On card hover, the card will appear above all other cards and cards next to it will adjust their position a
// bit for easy visualizing.
// Assumes anchors in center for parent and child elements.
public class CustomHorizontalFitter : MonoBehaviour
{
    // For card movement on hover.
    [SerializeField] private float HOVER_Y_OFFSET = 0f;
    private readonly float HOVER_Z_OFFSET = -4.9f;
    
    // Cards on the left-side of your hand are closer to the camera.
    private readonly float CARD_Z_OFFSET = 0.2f;
    
    // Preferred width is how wide a card would like to be if there is extra room.
    private readonly float ELEMENT_PREFERRED_WIDTH = 0.27f;
    
    [SerializeField] private Camera cam;
    private RectTransform rect;
    private List<Transform> elements;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private int lastHoverIndex = -1;
    private float lastHoverZ = -1;

    private void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        elements = new List<Transform>();
        foreach (Transform child in transform)
        {
            elements.Add(child);
        }
        Refresh();
    }

    private void Update()
    {
        if (elements == null || elements.Count < 1) return;

        // Hover display or un-display based on mouse position
        int layerMask = LayerMask.GetMask("Hand");
        Vector2 ray = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, Single.PositiveInfinity, layerMask);
        if (hit.collider)
        {
            OnHoverEnter(hit.collider.gameObject.transform);
        }
        else
        {
            OnHoverExit();
        }
        
        // Recalculate card positions and widths if cards are added or removed from hand
        if (transform.childCount != elements.Count)
        {
            elements = new List<Transform>();
            foreach (Transform child in transform)
            {
                elements.Add(child);
            }
            Refresh();
        }
    }

    // Re-calculate element positions/widths. Will be called when elements are added or removed.
    private void Refresh()
    {
        // Calculate position and width of parent object
        float width = rect.rect.width;
        float startX = transform.position.x - (width / 2);
        float endX = transform.position.x + (width / 2);
        float y = transform.position.y;
        float z = transform.position.z;
        startPosition = new Vector3(startX, y, z);
        endPosition = new Vector3(endX, y, z);
        
        // Determine element width based on available space, using element's preferred width if possible.
        float parentMaxWidth = Math.Abs(endPosition.x - startPosition.x);
        float parentPreferredWidth = elements.Count * ELEMENT_PREFERRED_WIDTH;
        float elementWidth = parentPreferredWidth <= parentMaxWidth
            ? ELEMENT_PREFERRED_WIDTH 
            : parentMaxWidth / elements.Count;
        
        // Find middle element and position in the middle of the hand.
        // Offset all cards to the right by have an element width for an even number of cards.
        // This makes positions look cleaner as you add cards.
        int middleIndex = (elements.Count / 2);
        float offset = elements.Count % 2 == 0 ? elementWidth / 2 : 0;
        elements[middleIndex].localPosition = new Vector3(0 + offset, 0, 0);

        // Loop to the left and then the right of center, setting card positions.
        for (int i = middleIndex - 1, j = 1; i >= 0; i--, j++)
        {
            float x = 0 - (elementWidth * j) + offset;
            elements[i].localPosition = new Vector3(x, 0, CARD_Z_OFFSET * j);
        }
        for (int i = middleIndex + 1, j = 1; i < elements.Count; i++, j++)
        {
            float x = 0 + (elementWidth * j) + offset;
            elements[i].localPosition = new Vector3(x, 0, -CARD_Z_OFFSET * j);
        }
    }

    // On mouse hover, move card in front of adjacent cards and move it upwards.
    private void OnHoverEnter(Transform hit)
    {
        // Find card that was hovered over, and un-hover last hovered element if any.
        // Use the z index to identify card, should be unique
        int index = elements.FindIndex(x => x.localPosition.z.Equals(hit.localPosition.z));
        if (index < 0) return; // TODO: Why is this possible?
        if (lastHoverIndex == index) return;
        OnHoverExit();
        lastHoverIndex = index;
        lastHoverZ = elements[index].localPosition.z;
        
        // Move card in front of other cards and perform hover movement
        elements[index].localPosition = new Vector3(
            elements[index].localPosition.x,
            0 + HOVER_Y_OFFSET, HOVER_Z_OFFSET
        );
        
        // If we are able to use preferred widths we can ignore the displacements below.
        float maxWidth = Math.Abs(endPosition.x - startPosition.x);
        float preferredWidth = elements.Count * ELEMENT_PREFERRED_WIDTH;
        if (preferredWidth <= maxWidth)
        {
            return;
        }

        // Calculate the width of overlap of two cards in the hand. This is based on how much space was available
        // in the parent's width and the number of cards to fit. 
        float offsetWidth = Math.Abs(elements[1].localPosition.x -
                                     (elements[0].localPosition.x + elements[0].localScale.x));

        // Adjust card positions to the left and right of the hovered card, to prevent adjacent cards from being
        // hidden under the hovered card. Cards will be moved to the left or right based on the width of the
        // overlap between two adjacent cards. 
        // for (int i = index-1, j = 1; i >= 0; i--, j++)
        // {
        //     elements[i].localPosition = new Vector3(elements[i].localPosition.x, 
        //         0,
        //         HOVER_Z_OFFSET + (CARD_Z_OFFSET * j));
        // }
        // for (int i = 0, j = 1; i < index; i++, j++)
        // {
        //     float x = elements[i].localPosition.x - (offsetWidth);// + (offsetWidth/2);
        //     elements[i].localPosition = new Vector3(x, 0, HOVER_Z_OFFSET + (CARD_Z_OFFSET * j));
        // }
        for (int i = index+1, j = 1; i < elements.Count; i++, j++)
        {
            float x = elements[i].localPosition.x;
            elements[i].localPosition = new Vector3(x, 0, HOVER_Z_OFFSET + (CARD_Z_OFFSET * j));
        }
    }

    // Reset card position when the mouse stops hovering over a card.
    private void OnHoverExit()
    {
        if (lastHoverIndex == -1) return;
        if (lastHoverIndex != -1)
        {
            elements[lastHoverIndex].localPosition = new Vector3(
                elements[lastHoverIndex].localPosition.x,
                0,
                lastHoverZ
            );
        }
        lastHoverIndex = -1;
        Refresh();
    }

}
