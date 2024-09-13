using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Flipper), typeof(EnemyAttackZone), typeof(PaintAttack))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animation _animation;

    private PaintAttack _paintAttack;
    private EnemyAttackZone _enemyAttackZone;
    private Flipper _flipper;

    private float _horizontalMove;
    private float _targetPosition;
    private int _hitPoint;
    private bool _died;

    private void Awake()
    {
        _paintAttack = GetComponent<PaintAttack>();
        _enemyAttackZone = GetComponent<EnemyAttackZone>();
        _flipper = GetComponent<Flipper>();
    }

    private void Start()
    {
        _horizontalMove = 1f;
        _hitPoint = 100;
        _targetPosition = -5;
    }

    private void Update()
    {
        Walk();

        _animation.Run(_horizontalMove);
        
        DetermineDirection(_horizontalMove);
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

    private void Walk()
    {
        transform.position = Vector2.MoveTowards(transform.position, GetNewVector(), _horizontalMove * Time.fixedDeltaTime);

        SetSpeed();
    }

    private void SetSpeed()
    {
        if (_enemyAttackZone.IsCame)
        {
            _horizontalMove = 0;

            _animation.Run(_horizontalMove);
        }
        else
        {
            _horizontalMove = 1;
        }
    }

    private void DetermineDirection(float horizontalMove)
    {
        if (Mathf.Abs(_targetPosition) - Mathf.Abs(transform.position.x) == 0)
        {
            SetTargetPosition();

            if (transform.position.x >= _targetPosition)
            {
                SetDirection(-horizontalMove);
            }
            else
            {
                SetDirection(horizontalMove);
            }
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

    private void SetTargetPosition()
    {
        if (_enemyAttackZone.IsLocatedInTargetZone)
        {
            _targetPosition = _enemyAttackZone.TargetPosition;
        }
        else
        {
            _targetPosition = GetRandomTarget();
        }
    }

    private void SetDirection(float horizontalMove)
    {
        _flipper.FlipCharacter(horizontalMove);
    }

    private Vector2 GetNewVector()
    {
        return new Vector2(_targetPosition, transform.position.y);
    }

    private int GetRandomTarget(int minPositionX = -10, int maxPositionX = 0)
    {
        return Random.Range(minPositionX, maxPositionX);
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