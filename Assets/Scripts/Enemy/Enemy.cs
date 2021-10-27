using UnityEngine;
using UnityEngine.UI;

public class Enemy : LivingEntity
{
    private void Start()
    {
        currentHp = MaxHP;
    }
}
