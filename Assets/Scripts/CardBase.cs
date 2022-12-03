using UnityEngine;

public interface CardBase
{
     public CardType GetCardType();
     
     public enum CardType
     {
          SNAKE=0,
          FOOD=1
     }
}
