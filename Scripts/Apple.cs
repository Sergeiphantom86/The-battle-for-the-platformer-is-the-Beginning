using System;
using UnityEngine;

[RequireComponent(typeof(GroundDetector), typeof(Rigidbody2D))]

public class Apple : MonoBehaviour
{
    public const string Desappearing = nameof(Desappearing);

    [SerializeField] private int _value;

    private GroundDetector _checkGrounded;
    private Rigidbody2D _rigidbody;

    public event Action Collected;

    public int Value => _value;

    private void Start()
    {
        _checkGrounded = GetComponent<GroundDetector>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        DisableRigidbody();
    }

    public void Collect()
    {
        Collected?.Invoke();
        gameObject.SetActive(false);
        _rigidbody.isKinematic = false;
    }

    private void DisableRigidbody()
    {
        if (_checkGrounded.LocatedOnGround())
        {
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector2.zero;
        }
    }

    public Vector2 GetRandomPosition()
    {
        int positionY = 5;
        int minPositionX = -14;
        int maxPositionX = 29;
        
        return new(UnityEngine.Random.Range(minPositionX, maxPositionX), positionY);
    }
}