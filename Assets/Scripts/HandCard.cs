using UnityEngine;

public class HandCard : MonoBehaviour
{
    [SerializeField] private float HOVER_MOVEMENT_DISTANCE = 1.0f;
    [SerializeField] private float HOVER_Z_POSITION = -4f;
    private float z;

    public void Start()
    {
        z = transform.localPosition.z;
    }

    public void OnMouseEnter()
    {
        Vector3 pos = transform.localPosition;
        z = pos.z;
        transform.localPosition.Set(
            pos.x, pos.y + HOVER_MOVEMENT_DISTANCE, HOVER_Z_POSITION
        );
    }

    public void OnMouseExit()
    {
        transform.localPosition = new Vector3(
            transform.localPosition.x, 
            transform.localPosition.y - HOVER_MOVEMENT_DISTANCE, z
        );
    }
}
