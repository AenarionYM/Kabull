using System.Collections.Generic;
using UnityEngine;

public class CommonMelee : MonoBehaviour
{
    //Objects
    private Animator _animator;
    private Rigidbody2D _rb;
    public Transform pointOne;
    public Transform pointTwo;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    //Variables
    public float speed = 150f;
    private float _enemyHealth;
    private float _chHurt;
    private bool _right = false;
    private int _cos = 0;
    private bool _attack = false;
    private bool _dead = false;
    private bool _hurt = false;
    public float attackRange = 0.5f;
    public float dmg = 50f;
    
    void Awake()
    {
        _enemyHealth = GetComponent<Enemy>().enemyHp;
        _chHurt = _enemyHealth;
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _enemyHealth = GetComponent<Enemy>().enemyHp;
        _animator.SetInteger("AnimState", _cos);
        _animator.SetBool("Attack", _attack);
        _animator.SetBool("Death", _dead);
        _animator.SetBool("Hurt", _hurt);

        if (_enemyHealth> 0)
        {
            if (!_right)
            {
                LCheck();
            }
            else if (_right)
            {
                RCheck();
            }
        }

        if (_chHurt != _enemyHealth && _enemyHealth > 0)
        {
            _hurt = true;
        }

        if (_enemyHealth <= 0)
        {
            _dead = true;
            //GetComponent<CommonMelee>().enabled = false;
        }
        
        _chHurt = _enemyHealth;
    }

    private void LCheck()
    {
        var position = pointOne.position;
        var position1 = pointTwo.position;

        RaycastHit2D hit1 = Physics2D.Raycast(position, Vector2.right, 10);
        RaycastHit2D hit2 = Physics2D.Raycast(position1, Vector2.left, 10);
        
        Debug.DrawRay(position, Vector2.right * 10, Color.magenta);
        Debug.DrawRay(position1, Vector2.left * 10, Color.magenta);
        
        if (hit1.collider != null && hit1.collider.CompareTag("Player"))
        {
            _cos = 2;
            transform.Rotate(0f,180f,0f); 
            _right = true;
        }
        
        if (hit2.collider != null && hit2.collider.CompareTag("Player"))
        {
            _hurt = false;
            _cos = 2;
            _rb.velocity = new Vector2(speed * -1, _rb.velocity.y);
        }
        
        if (hit2.collider != null && hit2.collider.CompareTag("Player") && hit2.distance <= 1)
        {
            _hurt = false;
            _cos = 2;
            _animator.SetTrigger("Attack");
        }
        else
        {
            _attack = false;
        }

        if (hit1.collider == null && hit2.collider == null)
        {
            _cos = 0;
        }
    }
    
    private void RCheck()
    {
        var position = pointOne.position;
        var position1 = pointTwo.position;

        RaycastHit2D hit1 = Physics2D.Raycast(position, Vector2.left, 10);
        RaycastHit2D hit2 = Physics2D.Raycast(position1, Vector2.right, 10);
        
        Debug.DrawRay(position, Vector2.left * 10, Color.magenta);
        Debug.DrawRay(position1, Vector2.right * 10, Color.magenta);
        
        if (hit1.collider != null && hit1.collider.CompareTag("Player"))
        {
            _cos = 2;
            transform.Rotate(0f,180f,0f); 
            _right = false;
        }

        if (hit2.collider != null && hit2.collider.CompareTag("Player"))
        {
            _cos = 2;
            _hurt = false;
            _rb.velocity = new Vector2(speed, _rb.velocity.y);
        }
        
        if (hit2.collider != null && hit2.collider.CompareTag("Player") && hit2.distance <= 1)
        {
            _cos = 2;
            _animator.SetTrigger("Attack");
            _hurt = false;
        }
        else
        {
            _attack = false;
        }
        
        if (hit1.collider == null && hit2.collider == null)
        {
            _cos = 0;
        }
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D playerin in hitEnemies)
        {
            if (playerin.name == "Player")
            {
                _animator.SetTrigger("Attack");
            }
        }
    }

    public void DealDmg()
    {
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        foreach (Collider2D playerin in hitEnemies)
        {
            Player player = playerin.GetComponent<Player>();
            if (playerin.name == "Player")
            {
                player.PlayerTakeDmg(dmg);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
}
    
    
    
