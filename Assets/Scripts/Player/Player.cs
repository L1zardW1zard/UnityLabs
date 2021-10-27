using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : LivingEntity
{
    public float Stamina = 100f;
    public float MaxStamina = 100f;
    private  int playerScore;
    public bool regenReady = true;
    public bool isScrollFound;
    public delegate void CoinPickUp(int addedScore);
    public static event CoinPickUp coinPickUp;


    // Start is called before the first frame update
    void Start()
    {
        //gameController = GetComponent<GameController>();
        InvokeRepeating(nameof(PassiveRegeneration),2f,2f);
    }

    public void OnCoinPickUp(int amount)
    {
        playerScore += amount;
        coinPickUp?.Invoke(playerScore);
    }
    
    private void PassiveRegeneration()
    {
        if (currentHp != MaxHP || Stamina != MaxStamina)
        {
            if (currentHp <= MaxHP - 5)
            {
                    currentHp += 5;
            }
            else if (currentHp < MaxHP && currentHp > 95)
            {
                    currentHp = MaxHP;
            }
            
            if (Stamina <= MaxStamina - 10)
            {
                Stamina += 10;
            }
            else if (Stamina < MaxStamina && Stamina > 90)
            {
                Stamina = MaxStamina;
            }
        }
    }
}
