using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Staminabar : MonoBehaviour
{
    private Image _staminaBar;

    public float currentStamina;
    private Player player;

    private void Start()
    {
        _staminaBar = GetComponent<Image>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        currentStamina = player.Stamina;
        _staminaBar.fillAmount = currentStamina / player.MaxStamina;
    }
}
