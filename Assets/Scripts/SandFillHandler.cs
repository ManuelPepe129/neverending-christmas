using System;
using UnityEngine;
using UnityEngine.UI;

public class SandFillHandler : MonoBehaviour
{
    private Image _image;
    
    private GameManager _gameManager;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.fillAmount = 1;
    }

    private void Start()
    {
        _gameManager = GameObject.FindFirstObjectByType<GameManager>();
    }

    private void Update()
    {
        _image.fillAmount = _gameManager.GetNormalizedTime();
    }
}
