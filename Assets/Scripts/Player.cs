using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private  int _playerScore;
    public delegate void CoinPickUp(int addedScore);
    public static event CoinPickUp coinPickUp;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnCoinPickUp(int amount)
    {
        _playerScore += amount;
        coinPickUp?.Invoke(_playerScore);
    }
}
