using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    [SerializeField] private List<CardScriptableObject> cardTypes;
    [SerializeField] private List<FoodCardScriptableObject> foodTypes;

    private List<CardBase> deck = new();
    private List<CardBase> hand = new();
    private List<CardBase> discard = new();
    private List<CardBase> inPlay = new();
    private CardScriptableObject[] inPlaySlots = new CardScriptableObject[6];

    private void Start()
    {
        OpponentEvents.EventStartTurn.AddListener(TakeTurn);
        
        // Initialize deck
        int i = 0;
        foreach (CardScriptableObject type in cardTypes)
        {
            deck.Add(type);
            i += 1;
        }
    }

    private void TakeTurn()
    {
        Draw(5);
        MakePurchase();
        PlayCardIfAble();
        Discard();
        OpponentEvents.EventDeckUpdate.Invoke(deck);
        OpponentEvents.EventDiscardUpdate.Invoke(discard);
        OpponentEvents.EventEndTurn.Invoke();
    }
    
    private void Draw(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (deck.Count == 0)
            {
                deck = discard.FindAll(x => true);
                Utils.ShuffleFisherYates(deck);
                discard = new List<CardBase>();
            }
            CardBase card = deck[deck.Count-1];
            deck = deck.Take(deck.Count - 1).ToList();
            hand.Add(card);
        }
    }

    private void MakePurchase()
    {
        // Randomized purchase of snake or food
        int rng = Random.Range(0, foodTypes.Count);
        if (rng == foodTypes.Count)
        {
            // Draw snake
            int rng2 = Random.Range(0, cardTypes.Count - 1);
            CardScriptableObject snake = cardTypes[rng2];
            hand.Add(snake);
            Debug.Log("Opponent purchases " + snake.title);
        }
        else
        {
            // Draw food
            FoodCardScriptableObject food = foodTypes[rng];
            hand.Add(food);
            Debug.Log("Opponent purchases " + food.foodType);
        }

    }

    private void PlayCardIfAble()
    {
        if (inPlay.Count == inPlaySlots.Length)
        {
            return; // No available slots
        }
        
        List<FoodCardScriptableObject> food = hand
            .FindAll(x => x.GetCardType().Equals(CardBase.CardType.FOOD))
            .Select(x => (FoodCardScriptableObject)x).ToList();
        List<CardScriptableObject> snakes = hand
            .FindAll(x => x.GetCardType().Equals(CardBase.CardType.SNAKE))
            .Select(x => (CardScriptableObject)x).ToList();
        int foodAmph = food.FindAll(x => x.foodType == FoodCardScriptableObject.FoodType.AMPHIBIAN).Count;
        int foodRep = food.FindAll(x => x.foodType == FoodCardScriptableObject.FoodType.REPTILE).Count;
        int foodSmM = food.FindAll(x => x.foodType == FoodCardScriptableObject.FoodType.SMALL_MAMMAL).Count;
        int foodLgM = food.FindAll(x => x.foodType == FoodCardScriptableObject.FoodType.LARGE_MAMMAL).Count;
        foreach (var card in snakes)
        {
            int haveTotal = 0;
            if (card.foodAmphibian) haveTotal += foodAmph;
            if (card.foodReptile) haveTotal += foodRep;
            if (card.foodSmallMammal) haveTotal += foodSmM;
            if (card.foodLargeMammal) haveTotal += foodLgM;
            if (haveTotal >= card.foodRequirement)
            {
                // Play card in first available slot
                for (int i = 0; i < inPlaySlots.Length; i++)
                {
                    if (inPlaySlots[i] == null)
                    {
                        OpponentEvents.EventPlayCard.Invoke(card, i);
                        inPlay.Add(card);
                        inPlaySlots[i] = card;
                        return;
                    }
                    
                }
            }
        }
    }

    private void Discard()
    {
        foreach (var card in hand)
        {
            discard.Add(card);
        }
        hand = new List<CardBase>();
    }
}