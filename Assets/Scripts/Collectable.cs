using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private ParticleSystem collectedEffect;

    private GameManager _gameManager;
    private AudioSource _audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _gameManager != null)
        {
            StartCoroutine(TriggerSequence());
        }
    }

    private IEnumerator TriggerSequence()
    {
        if (collectedEffect)
            collectedEffect.Play();
        if (_audioSource)
            _audioSource.Play();
        _gameManager.OnGiftCollected();

        yield return new WaitForSeconds(0.5f);
        
        Destroy(gameObject);
    }
}