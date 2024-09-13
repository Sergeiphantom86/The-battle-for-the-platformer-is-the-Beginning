using System;
using UnityEngine;

[RequireComponent(typeof(GroundDetector), typeof(Rigidbody2D))]

public class Coin : MonoBehaviour
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

    public Vector2 GetRandomPosition(int minPositionX = -6, int maxPositionX = 32, int positionY = 15)
    {
        return new(UnityEngine.Random.Range(minPositionX, maxPositionX), positionY);
    }

    private void DisableRigidbody()
    {
        if (_checkGrounded.PlaceOnGround())
        {
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector2.zero;
        }
    }
}