using UnityEngine;

[RequireComponent(typeof(Flipper), typeof(EnemyAttackZone))]

public class EnemyMover : MonoBehaviour
{
    private EnemyAttackZone _enemyAttackZone;
    private Flipper _flipper;
    private float _targetPosition;

    public float HorizontalMove { get; private set; }

    private void Awake()
    {
        _enemyAttackZone = GetComponent<EnemyAttackZone>();
        _flipper = GetComponent<Flipper>();
        _targetPosition = -5;
        HorizontalMove = 1f;
    }

    private void Update()
    {
        Walk(GetNewVector());
        SetSpeed();
        DetermineDirection();
    }

    private void Walk(Vector2 vector2)
    {
        transform.position = Vector2.MoveTowards(transform.position, vector2, HorizontalMove * Time.fixedDeltaTime);
    }

    private void DetermineDirection()
    {
        if (Mathf.Abs(_targetPosition) - Mathf.Abs(transform.position.x) == 0)
        {
            SetTargetPosition();

            if (transform.position.x >= _targetPosition)
            {
                SetDirection(-HorizontalMove);
            }
            else
            {
                SetDirection(HorizontalMove);
            }
        }
    }

    private void SetSpeed()
    {
        if (_enemyAttackZone.IsCame)
        {
            HorizontalMove = 0;
        }
        else
        {
            HorizontalMove = 1;
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

    private int GetRandomTarget(int minPositionX = -10, int maxPositionX = 0)
    {
        return Random.Range(minPositionX, maxPositionX);
    }
}