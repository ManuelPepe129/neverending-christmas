using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private ParticleSystem collectedEffect;
    [SerializeField] private AudioSource giftAudioSource;

    private GameManager _gameManager;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
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
        if (giftAudioSource)
            giftAudioSource.Play();
        _gameManager.OnGiftCollected();

        yield return new WaitForSeconds(0.5f);
        
        Destroy(gameObject);
    }
}