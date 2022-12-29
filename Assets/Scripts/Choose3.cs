using System.Collections.Generic;
using UnityEngine;

public class Choose3 : MonoBehaviour
{
    [SerializeField] private CardCodexScriptableObject cardCodex;
    [SerializeField] private GameObject container;
    [SerializeField] private Card slot1;
    [SerializeField] private Card slot2;
    [SerializeField] private Card slot3;
    
    private void Start()
    {
        Events.EventChoose3.AddListener(Init);
        Events.EventChoose3Close.AddListener(Close);
    }

    private void Init()
    {
        // TODO Animation
        Utils.ShuffleFisherYates(cardCodex.cards);
        slot1.SetType(cardCodex.cards[0]);
        slot2.SetType(cardCodex.cards[1]);
        slot3.SetType(cardCodex.cards[2]);
        slot1.Initialize();
        slot2.Initialize();
        slot3.Initialize();
        container.SetActive(true);
    }

    private void Close()
    {
        container.SetActive(false);
    }
}
