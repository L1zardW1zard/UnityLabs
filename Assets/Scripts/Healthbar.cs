using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Image _healthBar;

    public float currentHealth;
    private Player player;

    private void Start()
    {
        _healthBar = GetComponent<Image>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        currentHealth = player.HP;
        _healthBar.fillAmount = currentHealth / player.MaxHP;
    }
}
