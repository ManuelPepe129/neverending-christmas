using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTimerText : MonoBehaviour
{
    private TMP_Text _text;
    private GameManager _gameManager;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        _gameManager = GameObject.FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        var remainingTime = _gameManager.GetRemainingTime();
        var minutes = (int)remainingTime / 60;
        var seconds = (int)remainingTime % 60;
        _text.SetText($"{minutes}:{seconds}");
    }
}