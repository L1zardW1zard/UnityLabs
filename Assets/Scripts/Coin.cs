using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] Text coinText; // Не должно тут быть. Надо сделать что-то типо UIHandler'a 
    void Start()
    {
        Player.coinPickUp += OnPlayerCoinsChanged; // Не должно тут быть. Надо сделать что-то типо UIHandler'a 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            Debug.Log("Score Changed");
            Destroy(gameObject);
            player.OnCoinPickUp(1);
        }
    }

    private void OnPlayerCoinsChanged(int newCoins) => coinText.text = "Score: " + newCoins.ToString(); // Не должно тут быть. Надо сделать что-то типо UIHandler'a 
}
