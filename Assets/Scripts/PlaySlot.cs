using System;
using UnityEngine;

public class PlaySlot : MonoBehaviour
{
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Sprite sprite;
    private SpriteRenderer spriteRenderer;
    private float OPACITY = 1.0f;
    private float HOVER_OPACITY = 1.0f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        Debug.Log("enter " + gameObject.name);
        spriteRenderer.sprite = hoverSprite;
        spriteRenderer.color = new Color(1, 1, 1, HOVER_OPACITY);
    }

    private void OnMouseExit()
    {
        Debug.Log("exit");
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = new Color(1, 1, 1, OPACITY);
    }

    private void OnMouseUpAsButton()
    {
        Events.EventPlayCardsChooseSlot.Invoke(transform.position);
    }
}
