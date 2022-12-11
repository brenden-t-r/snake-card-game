using UnityEngine;
using UnityEngine.EventSystems;

public class HandCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float HOVER_MOVEMENT_DISTANCE = 1.0f;
    [SerializeField] private float HOVER_MOVEMENT_X_DISTANCE = 0f;
    [SerializeField] private float HOVER_Z_POSITION = -4f;
    private float z;

    public void Start()
    {
        z = transform.localPosition.z;
    }

    public void OnMouseEnter()
    {
        z = transform.localPosition.z;
        transform.localPosition = new Vector3(
            transform.localPosition.x + HOVER_MOVEMENT_X_DISTANCE, 
            transform.localPosition.y + HOVER_MOVEMENT_DISTANCE, HOVER_Z_POSITION
        );
    }

    public void OnMouseExit()
    {
        transform.localPosition = new Vector3(
            transform.localPosition.x - HOVER_MOVEMENT_X_DISTANCE, 
            transform.localPosition.y - HOVER_MOVEMENT_DISTANCE, z
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExit();
    }
}
