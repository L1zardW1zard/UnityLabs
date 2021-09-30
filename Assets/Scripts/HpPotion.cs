using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPotion : MonoBehaviour
{
    [SerializeField] private float hpAmount = 25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            if (player.HP < player.MaxHP - hpAmount)
            {
                player.HP += hpAmount;
                Destroy(gameObject);
            }else if (player.HP > player.MaxHP - hpAmount)
            {
                player.HP = player.MaxHP;
                Destroy(gameObject);
            }
        }
    }
}
