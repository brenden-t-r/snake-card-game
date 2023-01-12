using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

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
        // TODO: Toggle relevant UI features
        Events.EventDrawCardsFromDeck.Invoke(NUM_CARDS_TO_DRAW);
    }

    private void StartOpponentTurn() {
        OpponentEvents.EventStartTurn.Invoke();
    }
    
    private void EndTurn() {
        Debug.Log("GameController:EndTurn");
        // TODO: Toggle relevant UI features
        // TODO: Resolve actions on board
        Events.EventDiscardHand.Invoke();
        DoEndTurn();
    }

    private void EndOpponentTurn()
    {
        Debug.Log("GameController::EndOpponentTurn");
        // TODO: Resolve actions on board
        DoEndTurn();
    }

    private void DoEndTurn()
    {
        CheckEndGameConditions();
        playerInControl = playerInControl == PLAYER_PROTAGONIST ? PLAYER_OPPONENT : PLAYER_PROTAGONIST;
        StartTurn();
    }

    private void CheckEndGameConditions()
    {
        // TODO: Implement
        if (GameData.instance.playerHealth <= 0)
        {
            Debug.Log("Game over; player loses");
        } else if (GameData.instance.opponentHealth <= 0)
        {
            Debug.Log("Game over; player wins!");
        }
        else return;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
