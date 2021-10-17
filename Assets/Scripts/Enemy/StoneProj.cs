using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneProj : MonoBehaviour
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
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Destroy(gameObject);
            player.TakeDamage(_damage,_knockbackPower,transform.position.x);
        }
    }

    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(_destroyAfterTime);
        Destroy(gameObject);
    }
}
