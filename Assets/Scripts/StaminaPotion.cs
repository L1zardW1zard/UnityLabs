using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPotion : MonoBehaviour
{
    [SerializeField] private float staminaRecoveryAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            if (player.Stamina < player.MaxStamina - staminaRecoveryAmount)
            {
                player.Stamina += staminaRecoveryAmount;
                Destroy(gameObject);
            }else if (player.Stamina > player.MaxStamina - staminaRecoveryAmount)
            {
                player.Stamina = player.MaxStamina;
                Destroy(gameObject);
            }
        }
    }
}
