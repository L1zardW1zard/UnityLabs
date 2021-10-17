using System;
using UnityEngine;
using UnityEngine.UI;

public class Jumper : MonoBehaviour
{
    [SerializeField] private int _maxHp;
    [SerializeField] private int _damage;
    [SerializeField] private int _knockbackPower;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField]private bool _faceRight;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _enemySystem;
    private int _currentHp;

    private int CurrentHp
    {
        get => _currentHp;
        set
        {
            _currentHp = value;
            _slider.value = value;
        }
    }

    [Header("Animation")]
    [SerializeField] private string _jumpStartAnimationName;
    [SerializeField] private string _jumpAnimationName;
    [SerializeField] private string _isGroundedAnimationName;
    [SerializeField] private Animator _animator;

    private Rigidbody2D _rigidbody;
    private int _direction = 1;
    private float _lastAttackTime;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position,new Vector3(_attackRange,1, 0));
    }

    private void Start()
    {
        _slider.maxValue = _maxHp;
        CurrentHp = _maxHp;
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _lastAttackTime = Time.time;
    }

    private void FixedUpdate()
    {
        if (CheckIfCanStand())
        {
            CheckIfCanAttack();
        }
    }
    

    private bool CheckIfCanAttack()
    {
        Collider2D player = Physics2D.OverlapBox(transform.position, new Vector2(_attackRange, 1), 0, _whatIsPlayer);
        if (player != null && Time.time - _lastAttackTime > _attackCooldown) // 
        {
            _lastAttackTime = Time.time;
            StartJumpAttack(player.transform.position);
            return true;
        }
        return false;
    }

    private bool CheckIfCanStand()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround))
        {
            _animator.SetBool(_jumpStartAnimationName,false);
            _animator.SetBool(_jumpAnimationName,false);
            _animator.SetBool(_isGroundedAnimationName,true);
            return true;
        }
        _animator.SetBool(_isGroundedAnimationName,false);
        return false;
    }

    private void StartJumpAttack(Vector2 playerPosition)
    {
        if (transform.position.x > playerPosition.x && _faceRight || transform.position.x < playerPosition.x && !_faceRight)
        {
            _faceRight = !_faceRight;
            transform.Rotate(0,180,0);
            _direction *= -1;
        }

        if (CheckIfCanStand() ) //&& Time.time - _lastAttackTime > _attackCooldown
        {
            _animator.SetBool(_jumpStartAnimationName, true);
        }
    }

    public void JumpAttack()
    {
        _animator.SetBool(_isGroundedAnimationName,false);
        _animator.SetBool(_jumpStartAnimationName,false);
        _animator.SetBool(_jumpAnimationName,true);

        _rigidbody.AddForce(new Vector2(_direction*_attackRange*1000,_attackRange*500));
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(_damage,_knockbackPower,transform.position.x);
            Debug.Log("Collided with Player");
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHp -= damage;
        if(CurrentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
