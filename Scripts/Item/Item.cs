using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D), typeof(GroundDetector))]

public class Item : MonoBehaviour
{
    public const string Desappearing = nameof(Desappearing);

    [field: SerializeField] public int Value { get; private set; }

    private Rigidbody2D _rigidbody;
    private GroundDetector _isGroundDetector;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _isGroundDetector = GetComponent<GroundDetector>();
    }

    private void Update()
    {
        TryDisableRigidbody();
    }

    public void Collect()
    {
        gameObject.SetActive(false);
        _rigidbody.isKinematic = false;
    }

    public Vector2 GetRandomPosition(int minPositionX = -14, int maxPositionX = 29, int positionY = 11)
    {
        return new(UnityEngine.Random.Range(minPositionX, maxPositionX), positionY);
    }

    private void TryDisableRigidbody()
    {
        if (_isGroundDetector.IsGround())
        {
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector2.zero;
        }
    }
}