using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Weapon))]

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private CharacterAnimations _animation;

    private PaintAttack _paintAttack;
    private PlayerHealth _playerHealth;
    private InputReader _inputReader;
    private Weapon _weapon;

    private void Start()
    {
        _paintAttack = GetComponent<PaintAttack>(); 
        _playerHealth = GetComponent<PlayerHealth>();
        _inputReader = GetComponent<InputReader>();
        _weapon = GetComponent<Weapon>();
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != 0)
        {
            _playerMover.Move(_inputReader.Direction);
        }

        _animation.Run(_inputReader.Direction);
        _animation.Shoot(_weapon.IsFire);

        if (_inputReader.GetIsJump())
        {
            _playerMover.Jump();
        }
    }

    public void TakeDamage(int damage)
    {
        _paintAttack.ChangeColor();
        _playerHealth.ApplyDamage(damage);
    }

    public bool TakeTreatment(int health)
    {
        return _playerHealth.ApplyTreatment(health);
    }
}