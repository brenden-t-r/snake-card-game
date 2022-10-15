using System;
using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandCard : MonoBehaviour
{

    [SerializeField] private float HOVER_OVER_CARD_MOVEMENT_DISTANCE = 1.0f;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float HOVER_Z_POSITION = -4f;
    private float zPosition;
    private LayoutElement _layoutElement;
    private float _minWidth;

    public void Start()
    {
        zPosition = transform.localPosition.z;
        _layoutElement = GetComponent<LayoutElement>();
        _minWidth = _layoutElement.minWidth;
    }

    public void OnMouseEnter()
    {
        Debug.Log("onMouseEnter");
        zPosition = transform.localPosition.z;
        transform.localPosition = new Vector3(
            transform.localPosition.x, 
            transform.localPosition.y + HOVER_OVER_CARD_MOVEMENT_DISTANCE, HOVER_Z_POSITION
        );
        //_layoutElement.minWidth = 2.4f;
        // Vector3 end = new Vector3(
        //     transform.localPosition.x, 
        //     transform.localPosition.y + HOVER_OVER_CARD_MOVEMENT_DISTANCE, transform.localPosition.z
        // );
        // StartCoroutine(MoveOverSpeed(transform, end, speed));
    }

    public void OnMouseExit()
    {
        Debug.Log("onMouseExit");
        transform.localPosition = new Vector3(
            transform.localPosition.x, 
            transform.localPosition.y - HOVER_OVER_CARD_MOVEMENT_DISTANCE, zPosition
        );
       // _layoutElement.minWidth = _minWidth;
        // Vector3 end = new Vector3(
        //     transform.localPosition.x, 
        //     transform.localPosition.y - HOVER_OVER_CARD_MOVEMENT_DISTANCE, transform.localPosition.z
        // );
        // StartCoroutine(MoveOverSpeed(transform, end, speed));
    }
    
    public static IEnumerator MoveOverSpeed(Transform objectToMove, Vector3 end, float speed){
        // speed should be 1 unit per second
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame ();
        }
    }
}
