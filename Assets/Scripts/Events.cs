using System.Collections.Generic;
using UnityEngine.Events;

public static class Events
{
    // TURN
    public static readonly EventEndTurn EventEndTurn = new EventEndTurn();

    // HAND
    public static readonly EventDrawCard EventDrawCard = new EventDrawCard();
    public static readonly EventDrawCardsFromDeck EventDrawCardsFromDeck = new EventDrawCardsFromDeck();
    public static readonly EventDiscardHand EventDiscardHand = new EventDiscardHand();
    
    // DISCARD
    public static readonly EventDiscard EventDiscard = new EventDiscard();
    public static readonly EventShuffleDiscard ShuffleDiscard = new EventShuffleDiscard();
    public static readonly EventShuffleDiscardCallback ShuffleDiscardCallback = new EventShuffleDiscardCallback();
    
    // DRAW FROM SNAKE PILE, CHOOSE 1 OF 3
    public static readonly EventChoose3 EventChoose3 = new EventChoose3();
    public static readonly EventChoose3Close EventChoose3Close = new EventChoose3Close();
    
    // PLAY CARD FROM HAND INTO PLAY
    public static readonly EventPlayCard EventPlayCard = new EventPlayCard();
    public static readonly EventPlayCardsChooseSlot EventPlayCardsChooseSlot = new EventPlayCardsChooseSlot();
    
    // GAME DATA STATE UPDATES
    public static readonly EventHandUpdate EventHandUpdate = new EventHandUpdate();
    public static readonly EventDoHandUpdate EventDoHandUpdate = new EventDoHandUpdate();
    public static readonly EventDeckUpdate EventDeckUpdate = new EventDeckUpdate();
    public static readonly EventDiscardUpdate EventDiscardUpdate = new EventDiscardUpdate();
    public static readonly EventHealthUpdate EventHealthUpdate = new EventHealthUpdate();
}

public static class OpponentEvents
{
    // TURN
    public static readonly EventStartTurn EventStartTurn = new EventStartTurn();
    public static readonly EventEndTurn EventEndTurn = new EventEndTurn();
    public static readonly EventDiscardUpdate EventDiscardUpdate = new EventDiscardUpdate();
    public static readonly EventDeckUpdate EventDeckUpdate = new EventDeckUpdate();
    public static readonly EventOpponentPlayCard EventPlayCard = new EventOpponentPlayCard();
}


public class EventStartTurn : UnityEvent{}
public class EventEndTurn : UnityEvent{}
public class EventDiscard : UnityEvent<List<CardBase>>{}
public class EventDiscardHand : UnityEvent{}
public class EventDrawCardsFromDeck : UnityEvent<int>{}
public class EventDoHandUpdate : UnityEvent{}
public class EventHandUpdate : UnityEvent<List<CardBase>>{}
public class EventDeckUpdate : UnityEvent<List<CardBase>>{}
public class EventDiscardUpdate : UnityEvent<List<CardBase>>{}
public class EventHealthUpdate : UnityEvent<float>{}
public class EventOpponentPlayCard : UnityEvent<CardScriptableObject, int>{}