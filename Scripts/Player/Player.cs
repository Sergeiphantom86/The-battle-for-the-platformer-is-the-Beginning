using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Weapon), typeof(PaintAttack))]

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private CharacterAnimations _animation;

    private InputReader _inputReader;
    private PaintAttack _paintAttack;
    private Weapon _weapon;

    private int _hitPoint = 200;

    private void Start()
    {
        _paintAttack = GetComponent<PaintAttack>();
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
        if (_hitPoint > 0)
        {
            _hitPoint -= damage;
            _paintAttack.ChangeColor();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public bool TakeTreatment(int health)
    {
        int maxHealth = 200;
        
        if (_hitPoint < maxHealth)
        {
            _hitPoint += health;
            
            return false;
        }
        else
        {
            return true;
        }
    }
}