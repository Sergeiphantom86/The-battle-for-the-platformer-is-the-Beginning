using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;
    [SerializeField] Rigidbody2D _bullet;
    public int Demage { get; private set; }

    private void Start()
    {
        _bullet.velocity = transform.right * _speed;
        Demage = 20;
    }
}