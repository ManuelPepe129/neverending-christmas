using System;
using System.Collections;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float dayDuration = 10.0f;
    private float _dayDuration;
    private float _currentTime;

    private int _totalGifts = 0;
    private int _giftsCollected = 0;

    void Start()
    {
        // TODO: Get objects in scene
    }

    void StartGame()
    {
        // Start timer
        _dayDuration = dayDuration * 60f; // Convert minutes to seconds
        _currentTime = 0f;
        // Spawn gifts (?)
        // Spawn enemies
    }

    private IEnumerator DayTimer()
    {
        while (_currentTime <= _dayDuration)
        {
            _currentTime += Time.deltaTime;
            yield return new WaitForSeconds(1.0f);
        }
    }
    
    public void OnGiftCollected()
    {
        _giftsCollected++;

        if (_giftsCollected == _totalGifts)
        {
            // TODO: Spawn Boss wave
        }
    }
    
    public void OnPlayerDeath(bool isDeath)
    {
        // Play Game Over audio
        // Update UI
        throw new NotImplementedException("OnPlayerDeath event not implemented yet.");
        Time.timeScale = 0; // Freeze the game
    }

    public void OnLevelCompleted()
    {
        // Play Win audio
        throw new NotImplementedException("OnLevelCompleted event not implemented yet.");
        Time.timeScale = 0; // Freeze the game
    }
}