﻿using UnityEngine;

public class GameController : MonoBehaviour {
    
    // TODO: Not implemented
	
    int playerInControl;
	
    private readonly int PLAYER_PROTAGONIST = 0;
    private readonly int PLAYER_OPPONENT = 1;
    private readonly int NUM_CARDS_TO_DRAW = 5;

    private void OnEnable() {
        playerInControl = PLAYER_PROTAGONIST;
        //Events.EndTurn.AddListener(EndTurn);
        StartTurn();
    }
    
    private void StartTurn() {
        if (playerInControl == PLAYER_PROTAGONIST) {
            StartPlayerTurn();
        }
        else {
            StartOpponentTurn();
        }
    }

    private void EndTurn() {
        // Disable relevant UI features
        // Resolve actions on board
        // If either player's health reaches 0, end game.
        playerInControl = playerInControl == 1 ? 0 : 1;
        StartTurn();
    }

    private void StartPlayerTurn() {
        // Enable relevant UI features
        // Draw X cards
        // Allow player to make 1 purchase
        // Allow player to put cards into play
        // Wait until player clicks to end turn
        // Trigger end turn 
        
        Events.EventDrawCardsFromHand.Invoke(NUM_CARDS_TO_DRAW);
    }

    private void StartOpponentTurn() {
        // Draw X cards
        //
    }



}
