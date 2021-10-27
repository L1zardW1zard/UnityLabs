using UnityEngine;
using UnityEngine.UI;

public class RangedAlien : MonoBehaviour
{
    [SerializeField] private float _attackRange;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private Rigidbody2D _bullet;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private bool _faceRight;
    [SerializeField] private Slider _slider;
    

    [Header("Animation")]
    [SerializeField] private string _shootAnimationName;
    [SerializeField] private Animator _animator;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position,new Vector3(_attackRange,1, 0));
    }

    private void FixedUpdate()
    {
        CheckIfCanShoot();
    }
    
    private bool CheckIfCanShoot()
    {
        Collider2D player = Physics2D.OverlapBox(transform.position, new Vector2(_attackRange, 1), 0, _whatIsPlayer);
        if (player != null)
        {
            StartShoot(player.transform.position);
            return true;
        }
        return false;
    }

    private void StartShoot(Vector2 playerPosition)
    {
        if (transform.position.x > playerPosition.x && _faceRight || transform.position.x < playerPosition.x && !_faceRight)
        {
            _faceRight = !_faceRight;
            transform.Rotate(0,180,0);
        }
        _animator.SetBool(_shootAnimationName,true);
    }

    public void Shoot()
     {
         Rigidbody2D bullet = Instantiate(_bullet, _muzzle.position, Quaternion.identity);
         bullet.velocity = _projectileSpeed * transform.right;
         _animator.SetBool(_shootAnimationName,false);
         Invoke(nameof(CheckIfCanShoot),1f);
     }
}
