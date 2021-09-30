using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //private GameController gameController;
    public float HP = 30f;
    public float MaxHP = 100f;
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
        if (HP != MaxHP || Stamina != MaxStamina)
        {
            if (HP <= MaxHP - 5)
            {
                    HP += 5;
            }
            else if (HP < MaxHP && HP > 95)
            {
                    HP = MaxHP;
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
