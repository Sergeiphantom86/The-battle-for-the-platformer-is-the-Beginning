using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D), typeof(GroundDetector))]

public class Item : MonoBehaviour
{
    public const string Desappearing = nameof(Desappearing);

    [field: SerializeField] public int Value { get; private set; }

    private Rigidbody2D _rigidbody;
    private GroundDetector _isGroundDetector;
    private string _coin;

    public event Action Collected;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _isGroundDetector = GetComponent<GroundDetector>();
        _coin = "coin";
    }

    private void Update()
    {
        TryDisableRigidbody();
    }

    public void Collect(string name)
    {
        if (name == _coin)
        {
            Collected?.Invoke();
        }
      
        gameObject.SetActive(false);
        _rigidbody.isKinematic = false;
    }

    public Vector2 GetRandomPosition(int minPositionX = -14, int maxPositionX = 29, int positionY = 7)
    {
        return new(UnityEngine.Random.Range(minPositionX, maxPositionX), positionY);
    }

    private void TryDisableRigidbody()
    {
        if (_isGroundDetector.PlaceOnGround())
        {
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector2.zero;
        }
    }
}