using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    [SerializeField] private float _flyRange;
    [SerializeField] private float _attackRange;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed;
    [SerializeField] private bool _faceRight;
    [SerializeField] private Rigidbody2D _projRB;
    [SerializeField] private Transform _projStartingPos;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private LayerMask _whatIsPlayer;

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
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
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
        Gizmos.DrawWireCube(_drawPosition,new Vector3(_flyRange,0.3f,0));
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(_drawPosition - new Vector2(0,_attackRange),new Vector3(_flyRange,4f,0));
    }

    // Update is called once per frame
    void Update()
    {
        if (_faceRight && transform.position.x > _startPosition.x + _flyRange/2)
        {
            Flip();
        }else if (!_faceRight && transform.position.x < _startPosition.x - _flyRange/2)
        {
            Flip();
        }

        CheckIfCanAttack();
    }

    private void CheckIfCanAttack()
    {
        Collider2D player = Physics2D.OverlapBox(_drawPosition - new Vector2(0,_attackRange), new Vector2(_flyRange, 4f), 0, _whatIsPlayer);
        if (player != null && Time.time - _lastAttackTime > _attackCooldown)
        {
            _lastAttackTime = Time.time;
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        Collider2D player = Physics2D.OverlapBox(transform.position, new Vector2(_attackRange, 1), 0, _whatIsPlayer);
        yield return new WaitForSeconds(_attackCooldown);
        Rigidbody2D _proj = Instantiate(_projRB, _projStartingPos.position, Quaternion.identity);
        _proj.velocity = _projectileSpeed * transform.forward;
    }
}
