using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Flipper), typeof(EnemyAttackZone), typeof(ColorChangeWhenAttack))]

public class Enemy : MonoBehaviour
{
    public const string IsAttack = nameof(IsAttack);
    public const string Speed = nameof(Speed);
    public const string Died = nameof(Died);

    [SerializeField] private Animator _animator;

    private ColorChangeWhenAttack _colorChangeWhenAttack;
    private EnemyAttackZone _enemyAttackZone;
    private Flipper _flipper;

    private float _horizontalMove;
    private float _targetPosition;
    private int _hitPoint;
    private bool _died;

    private void Awake()
    {
        _colorChangeWhenAttack = GetComponent<ColorChangeWhenAttack>();
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

        _animator.SetFloat(Speed, Mathf.Abs(_horizontalMove));
        
        DetermineDirection(_horizontalMove);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
        {
            Destroy(bullet.gameObject);
            TakeDamage(bullet.Demage);

            _colorChangeWhenAttack.ChangeColor();
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

            _animator.SetFloat(Speed, Mathf.Abs(_horizontalMove));
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

        _animator.SetBool(Died, _died);

        yield return wait;

        gameObject.SetActive(false);
    }
}