using UnityEngine;

public class GameController : MonoBehaviour {
    
    // TODO: Not implemented
	
    int playerInControl;
	
    private readonly int PLAYER_PROTAGONIST = 0;
    private readonly int PLAYER_OPPONENT = 1;
    private readonly int NUM_CARDS_TO_DRAW = 5;

    private void Start()
    {
        Events.EventEndTurn.AddListener(EndTurn);
        OpponentEvents.EventEndTurn.AddListener(EndOpponentTurn);
    }

    private void OnEnable() {
        playerInControl = PLAYER_PROTAGONIST;
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
    
    private void StartPlayerTurn() {
        // Draw X cards
        // Enable relevant UI features
        // Allow player to make 1 purchase
        // Allow player to put cards into play
        // Wait until player clicks to end turn
        // Trigger end turn 
        Events.EventDrawCardsFromDeck.Invoke(NUM_CARDS_TO_DRAW);
    }

    private void EndTurn() {
        Debug.Log("GameController:EndTurn");
        // Disable relevant UI features
        // Discard hand
        // Resolve actions on board
        // If either player's health reaches 0, end game.
        Events.EventDiscardHand.Invoke();
        
        playerInControl = playerInControl == 1 ? 0 : 1;
        StartTurn();
    }
    
    private void StartOpponentTurn() {
        OpponentEvents.EventStartTurn.Invoke();
    }

    private void EndOpponentTurn()
    {
        Debug.Log("GameController::EndOpponentTurn");
        // If either player's health reaches 0, end game.
        playerInControl = playerInControl == 1 ? 0 : 1;
        StartTurn();
    }

}
