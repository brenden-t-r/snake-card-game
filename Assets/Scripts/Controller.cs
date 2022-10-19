using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller
{
    public void ShowAllCards()
    {
        SceneManager.LoadScene("Scenes/CardDisplayDemoScene", LoadSceneMode.Additive);
    }
}