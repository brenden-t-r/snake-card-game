
using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShowAllCards : MonoBehaviour
{
    private void Start()
    {
        List<GameObject> cards = GetComponent<Deck>().GetCards();
        foreach (var card in cards)
        {
            Vector3 scale = card.transform.localScale;
            scale.Scale(new Vector3(100f, 100f, 0));
            card.transform.localScale = scale;
            card.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
}
