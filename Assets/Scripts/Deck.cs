using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<CardScriptableObject> cardTypes;
    [SerializeField] private List<GameObject> cards;
    [SerializeField] private GameObject prefabCard;

    private void Start()
    {
        // Create prefab for each card in cardTypes
        foreach (CardScriptableObject type in cardTypes)
        {
            GameObject card = Instantiate(prefabCard, transform);
            card.transform.localRotation = Quaternion.Euler(0, -180f, 0);
            Card cardScript = card.GetComponent<Card>();
            cardScript.SetType(type);
            cardScript.Initialize();
            cards.Add(card);
        }

        foreach (GameObject card in cards)
        {
            Debug.Log(card.name);
        }
    }
}
