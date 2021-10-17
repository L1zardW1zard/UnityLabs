using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _destroyAfterTime;
    [SerializeField] private int _damage;
    [SerializeField] private int _knockbackPower;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownTimer());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Player _player = other.gameObject.GetComponent<Player>();
        Destroy(gameObject);
        if (_player != null)
        {
            _player.TakeDamage(_damage,_knockbackPower,transform.position.x);
        }
    }

    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(_destroyAfterTime);
        Destroy(gameObject);
    }
}
