using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public void ShowAllCards()
    {
        SceneManager.LoadScene("Scenes/CardDisplayDemoScene");
    }

    public void EndTurn()
    {
        Events.EventEndTurn.Invoke();
    }
}