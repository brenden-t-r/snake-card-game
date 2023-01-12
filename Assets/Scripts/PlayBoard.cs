using UnityEngine;

public class PlayBoard : MonoBehaviour
{
    [SerializeField] private GameObject slotsPlayer;
    [SerializeField] private GameObject slotsOpponent;
    [SerializeField] private GameObject cardInPlayPrefab;
    private CardScriptableObject cardToPlay;
    
    private void Start()
    {
        Events.EventPlayCard.AddListener(PlayCardEvent);
        Events.EventPlayCardsChooseSlot.AddListener(PlayChooseSlotEvent);
        OpponentEvents.EventPlayCard.AddListener(OpponentPlayCardEvent);
    }

    private void PlayCardEvent(CardScriptableObject cardScriptableObject)
    {
        cardToPlay = cardScriptableObject;
        slotsPlayer.SetActive(true);
    }

    private void PlayChooseSlotEvent(Vector3 slotPos)
    {
        slotsPlayer.SetActive(false);
        DoPlayCard(cardToPlay, slotPos);
        cardToPlay = null;
    }

    private void OpponentPlayCardEvent(CardScriptableObject cardScriptableObject, int slotIndex)
    {
        Vector3 pos = slotsOpponent
            .GetComponentsInChildren<Transform>()[slotIndex+1] // 0 index is parent game object
            .position;
        DoPlayCard(cardScriptableObject, pos);
    }

    private void DoPlayCard(CardScriptableObject type, Vector3 slotPos)
    {
        GameObject card = Instantiate(cardInPlayPrefab, slotPos, Quaternion.identity, transform);
        CardInPlay cardInPlay = card.GetComponent<CardInPlay>();
        cardInPlay.SetType(type);
        cardInPlay.Initialize();   
    }
}
