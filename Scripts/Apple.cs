using UnityEngine;

[RequireComponent(typeof(GroundDetector), typeof(Rigidbody2D))]

public class Apple : MonoBehaviour
{
    public const string Desappearing = nameof(Desappearing);

    [SerializeField] private int _value;

    private GroundDetector _groundDetector;
    private Rigidbody2D _rigidbody;

    public int Value => _value;

    private void Start()
    {
        _groundDetector = GetComponent<GroundDetector>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        DisableRigidbody();
    }

    public void Collect()
    {
        gameObject.SetActive(false);
        _rigidbody.isKinematic = false;
    }

    private void DisableRigidbody()
    {
        if (_groundDetector.PlaceOnGround())
        {
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector2.zero;
        }
    }

    public Vector2 GetRandomPosition(int minPositionX = -14, int maxPositionX = 29, int positionY = 5)
    {
        return new(Random.Range(minPositionX, maxPositionX), positionY);
    }
}