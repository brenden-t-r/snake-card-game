using UnityEditor;
using UnityEngine;

public class PlayBoard : MonoBehaviour
{
    [SerializeField] private GameObject slots;
    [SerializeField] private GameObject cardInPlayPrefab;
    private CardScriptableObject cardToPlay;
    
    private void Start()
    {
        Events.EventPlayCard.AddListener(PlayCardEvent);
        Events.EventPlayCardsChooseSlot.AddListener(PlayChooseSlotEvent);
    }

    private void PlayCardEvent(CardScriptableObject cardScriptableObject)
    {
        cardToPlay = cardScriptableObject;
        slots.SetActive(true);
    }

    private void PlayChooseSlotEvent(Vector3 slotPos)
    {
        slots.SetActive(false);
        GameObject card = Instantiate(cardInPlayPrefab, slotPos, Quaternion.identity, transform);
        CardInPlay cardInPlay = card.GetComponent<CardInPlay>();
        cardInPlay.SetType(cardToPlay);
        cardInPlay.Initialize();
        cardToPlay = null;
    }
}
