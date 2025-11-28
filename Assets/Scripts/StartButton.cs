using UnityEngine;
using UnityEngine.SceneManagement; // serve per cambiare scena

public class StartButton : MonoBehaviour
{
    public string gameSceneName; // nome della scena di gioco

    public void StartGame()
    {
        Debug.Log($"Loading level {gameSceneName}");
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneName);
    }
}
