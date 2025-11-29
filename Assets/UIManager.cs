using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text giftText;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("GiftsCollected")) return;
        var giftsCollected = PlayerPrefs.GetInt("GiftsCollected");
        giftText.text = $"{giftsCollected}/3 gifts collected";
    }
}
