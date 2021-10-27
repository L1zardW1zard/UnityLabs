using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.UI;

public class LivingEntity : MonoBehaviour, IDamagable
{
    public int MaxHP;
    public int currentHp;
    public float lastTimeHit;
    private Rigidbody2D _rigidbody2D;
    private readonly string animatorHurtName = "Hurt";
    [SerializeField] private Animator _animator;
    [SerializeField] private Slider _slider;
    
    private int CurrentHeath
    {
        get => currentHp;
        set
        {
            currentHp = value;
            if (_slider!=null) _slider.value = value;
        } 
    }

    private void Start()
    {
        if (_slider!=null) _slider.maxValue = MaxHP;
        CurrentHeath = MaxHP;
    }

    public void TakeDamage(int damage,float knockback = 0,float enemyPosX = 0)
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        CurrentHeath -= damage;
        lastTimeHit = Time.time;
        
        if (CurrentHeath <= 0)
        {
            Die();
            return;
        }

        if (animatorHurtName != "")
        {
            if (_animator.GetBool(animatorHurtName)) 
                return;
            _animator.SetBool(animatorHurtName,true);
        }
        
        if (knockback != 0)
        {
            int direction = transform.position.x > enemyPosX ? 1 : -1;
            _rigidbody2D.AddForce(new Vector2(direction * knockback * 2,knockback*1.5f));
        }
    }

    public void HealDamage(int heal)
    {
        CurrentHeath += heal;
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
}
