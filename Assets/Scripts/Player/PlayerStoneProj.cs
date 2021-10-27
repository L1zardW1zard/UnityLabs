using System;
using System.Collections;
using UnityEngine;

public class PlayerStoneProj : MonoBehaviour
{
    [SerializeField] private float _destroyAfterTime;
    [SerializeField] private int _damage;
    [SerializeField] private int _knockbackPower;
    [SerializeField] private LayerMask _whatIsEnemy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownTimer());
    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        IDamagable enemyObject = other.gameObject.GetComponent<IDamagable>();
        
        if (enemyObject != null)
        {
            if (other.gameObject.GetComponent<Player>() == null)
            {
                enemyObject.TakeDamage(_damage,_knockbackPower,transform.position.x);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(_destroyAfterTime);
        Destroy(gameObject);
    }
}