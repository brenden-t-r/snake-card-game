using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Slider healthMeter;
    [SerializeField] private TMP_Text textHealth;
    [SerializeField] private TMP_Text discardPileCount;
    [SerializeField] private TMP_Text deckPileCount;
    private int deckCount = 0;
    private int discardCount = 0;

    private void Start()
    {
        // Events.EventDrawCard.AddListener(_ => deckCount += 1); //Add
        // Events.EventPlayCardsChooseSlot.AddListener(_ => deckCount -= 1); //Remove
        Events.EventDeckUpdate.AddListener(x => deckCount = x.Count);
        Events.EventDiscardUpdate.AddListener(x => discardCount = x.Count);
    }

    private void Update()
    {
        textHealth.text = "" + Math.Round(healthMeter.value * 100) + "/100";
        deckPileCount.text = deckCount.ToString();
        discardPileCount.text = discardCount.ToString();
    }
}
