using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private LayerMask _whatIsGround;
    public int Damage { get; private set; }

    private void Start()
    {
        _rigidbody.velocity = transform.right * _speed;
        Damage = 20;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.IsTouchingLayers(_whatIsGround))
        {
            Destroy(gameObject);

            Debug.Log(_whatIsGround);
        }
    }
}