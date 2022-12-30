using System;
using UnityEngine;

public class HandCardPlay : MonoBehaviour
{
    private CardScriptableObject cardScriptableObject;
    
    private void Start()
    {
        cardScriptableObject = GetComponent<Card>().GetCardType();
    }

    private void OnMouseUpAsButton()
    {
        Events.EventPlayCard.Invoke(cardScriptableObject);
        Events.EventPlayCardsChooseSlot.AddListener(PlayCardCallback);
    }

    private void PlayCardCallback(Vector3 _)
    {
        Destroy(gameObject);
    }
}
