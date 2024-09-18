using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PaintAttack))]

public class Enemy : EnemyMover
{
    [SerializeField] private CharacterAnimations _animation;
    
    private PaintAttack _paintAttack;

    private int _hitPoint;
    private bool _died;

    private void Awake()
    {
        _paintAttack = GetComponent<PaintAttack>();
    }

    private void Start()
    {
        _hitPoint = 100;
    }

    private void Update()
    {
        _animation.Run(HorizontalMove);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
        {
            Destroy(bullet.gameObject);
            TakeDamage(bullet.Damage);

            _paintAttack.ChangeColor();
        }
    }

    private void TakeDamage(int damage)
    {
        _hitPoint -= damage;

        if (_hitPoint <= 0)
        {
            _died = true;

            StartCoroutine(DelayBeforeDeleting());
        }
    }

    private IEnumerator DelayBeforeDeleting()
    {
        float delay = 0.5f;
        
        WaitForSeconds wait = new (delay);

        _animation.Die(_died);

        yield return wait;

        gameObject.SetActive(false);
    }
}