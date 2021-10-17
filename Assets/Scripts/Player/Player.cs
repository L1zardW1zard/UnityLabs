using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    //private GameController gameController;
    public float currentHp = 30f;
    public float MaxHP = 100f;
    public float Stamina = 100f;
    public float MaxStamina = 100f;
    private  int playerScore;
    public  float _lastTimeHit;
    private Rigidbody2D playerRB;
    private Animator _animator;
    private readonly string animatorHurtName = "Hurt";

    public bool regenReady = true;
    public bool isScrollFound;
    public delegate void CoinPickUp(int addedScore);
    public static event CoinPickUp coinPickUp;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //gameController = GetComponent<GameController>();
        InvokeRepeating(nameof(PassiveRegeneration),2f,2f);
        playerRB = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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

    public void TakeDamage(int damage,float knockback = 0,float enemyPosX = 0)
    {
        if (_animator.GetBool(animatorHurtName)) 
            return;

        currentHp -= damage;
        regenReady = false;
        _animator.SetBool(animatorHurtName,true);
        if (currentHp <= 0)
        {
            gameObject.SetActive(false);
        }

        if (knockback != 0)
        {
            _lastTimeHit = Time.time;
            int direction = transform.position.x > enemyPosX ? 1 : -1;
            playerRB.AddForce(new Vector2(direction * knockback * 2,knockback/2));
        }
    }
}
