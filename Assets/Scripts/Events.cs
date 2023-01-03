
using System;using System.Collections.Generic;
using UnityEngine.Events;

public static class Events
{
    public static readonly EventDrawCard EventDrawCard = new EventDrawCard();
    public static readonly EventDrawCardsFromHand EventDrawCardsFromHand = new EventDrawCardsFromHand();
    public static readonly EventShuffleDiscard ShuffleDiscard = new EventShuffleDiscard();
    public static readonly EventShuffleDiscardCallback ShuffleDiscardCallback = new EventShuffleDiscardCallback();
    public static readonly EventChoose3 EventChoose3 = new EventChoose3();
    public static readonly EventChoose3Close EventChoose3Close = new EventChoose3Close();
    public static readonly EventPlayCard EventPlayCard = new EventPlayCard();
    public static readonly EventPlayCardsChooseSlot EventPlayCardsChooseSlot = new EventPlayCardsChooseSlot();
    
    public static readonly EventHandUpdate EventHandUpdate = new EventHandUpdate();

    public static readonly EventDoHandUpdate EventDoHandUpdate = new EventDoHandUpdate();
    public static readonly EventDeckUpdate EventDeckUpdate = new EventDeckUpdate();
    // TODO
    public static readonly EventDiscardUpdate EventDiscardUpdate = new EventDiscardUpdate();
    public static readonly EventHealthUpdate EventHealthUpdate = new EventHealthUpdate();
}

public class EventDrawCardsFromHand: UnityEvent<int>{}
public class EventDoHandUpdate : UnityEvent{}
public class EventHandUpdate : UnityEvent<List<CardBase>>{}
public class EventDeckUpdate : UnityEvent<List<CardBase>>{}
public class EventDiscardUpdate : UnityEvent<List<CardBase>>{}
public class EventHealthUpdate : UnityEvent<float>{}
