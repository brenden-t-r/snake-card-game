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
    [SerializeField] private bool isOpponent; // Hacky
    private int deckCount = 0;
    private int discardCount = 0;
    
    private void Start()
    {
        if (isOpponent)
        {
            OpponentEvents.EventDeckUpdate.AddListener(x => deckCount = x.Count);
            OpponentEvents.EventDiscardUpdate.AddListener(x => discardCount = x.Count);
        }
        else
        {
            Events.EventDeckUpdate.AddListener(x => deckCount = x.Count);
            Events.EventDiscardUpdate.AddListener(x => discardCount = x.Count);
        }
    }

    private void Update()
    {
        textHealth.text = "" + Math.Round(healthMeter.value * 100) + "/100";
        deckPileCount.text = deckCount.ToString();
        discardPileCount.text = discardCount.ToString();
        // TODO: Move this out of Update
        if (isOpponent)
        {
            GameData.instance.opponentHealth = healthMeter.value * 100;
        }
        else {
            GameData.instance.playerHealth = healthMeter.value * 100;
        }
    }
}
