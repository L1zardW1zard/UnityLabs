using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsEnemy;
    [Header("Melee")]
    [SerializeField] private Transform _meleeAttackPoint;
    [SerializeField] private float _meleeAttackRange;
    [SerializeField] private int _meleeAttackDamage;
    [SerializeField] private float _meleeAttackCooldown;
    [Header("Ranged")]
    [SerializeField] private Transform _rangedAttackPoint;
    [SerializeField] private Rigidbody2D _proj;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _rangedAttackCooldown;

    [Header("Other")]
    [SerializeField]
    private MousePosition mousePointer;
    private Vector3 _mousePointerPosition;
    private Player _player;
    
    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animatorTriggerName;

    private float lastMeleeAttackTime;
    private float lastRangedAttackTime;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    void Update()
    {
        _mousePointerPosition = mousePointer.mouseWorldPosition;
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(Time.time - lastMeleeAttackTime > _meleeAttackCooldown) MeleeAttack();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(Time.time - lastRangedAttackTime > _rangedAttackCooldown) RangedAttack();
        }
    }

    private void MeleeAttack()
    {
        lastMeleeAttackTime = Time.time;
        _animator.SetTrigger(_animatorTriggerName);

        Collider2D[] enemiesHit =  Physics2D.OverlapCircleAll(_meleeAttackPoint.position, _meleeAttackRange, _whatIsEnemy);

        foreach (Collider2D enemy in enemiesHit)
        {
            enemy.GetComponent<Enemy>().TakeDamage(_meleeAttackDamage);
        }
    }

    private void RangedAttack()
    {
        if (_player.Stamina >= 15f)
        {
            lastRangedAttackTime = Time.time;
            Rigidbody2D proj = Instantiate(_proj, _rangedAttackPoint.position, Quaternion.identity);
            proj.velocity = _projectileSpeed * transform.right;
            _player.Stamina -= 15f;
        }
        //_animator.SetBool(_shootAnimationName,false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_meleeAttackPoint.position,_meleeAttackRange);
    }
}
