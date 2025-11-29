using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] private int sceneBuildIndex;

    public void StartGame()
    {
        StartCoroutine(LoadSceneCoroutine());
    }
    
    private IEnumerator LoadSceneCoroutine()
    {
        var asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex);
        while (asyncLoad is { isDone: false })
        {
            yield return null;
        }
    }
}
