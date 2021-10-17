using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] private float _walkRange;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed;
    [SerializeField] private bool _faceRight;
    [SerializeField] private int _damage;
    [SerializeField] private int _knockbackPower;
    [SerializeField] private float _attackCooldown;

    private Vector2 _startPosition;
    private int _direction = 1;

    private float _lastAttackTime;
    private Vector2 _drawPosition
    {
        get
        {
            if (_startPosition == Vector2.zero)
                return transform.position;
            else
                return _startPosition;
        }
    }
    
    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        if (_faceRight && transform.position.x > _startPosition.x + _walkRange/2)
        {
            Flip();
        }else if (!_faceRight && transform.position.x < _startPosition.x - _walkRange/2)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = Vector2.right * _speed * _direction;
    }

    private void Flip()
    {
        _faceRight = !_faceRight;
        transform.Rotate(0,180,0);
        _direction *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(_drawPosition,new Vector3(_walkRange,0.5f,0));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Player _player = other.gameObject.GetComponent<Player>();
        if (_player != null && Time.time - _lastAttackTime > _attackCooldown)
        {
            //Debug.Log("Collided with Player");
            _lastAttackTime = Time.time;
            _player.TakeDamage(_damage,_knockbackPower,transform.position.x);
        }
    }
}
