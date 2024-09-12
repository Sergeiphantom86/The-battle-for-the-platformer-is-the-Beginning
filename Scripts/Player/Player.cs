using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Weapon), typeof(ColorChangeWhenAttack))]

public class Player : MonoBehaviour
{
    public const string Speed = nameof(Speed);
    public const string Shot = nameof(Shot);
    public const string Died = nameof(Died);

    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private Animator _animator;

    private InputReader _inputReader;
    private ColorChangeWhenAttack _colorChangeWhenAttack;
    private Weapon _weapon;

    private int _hitPoint = 200;

    private void Start()
    {
        _colorChangeWhenAttack = GetComponent<ColorChangeWhenAttack>();
        _inputReader = GetComponent<InputReader>();
        _weapon = GetComponent<Weapon>();
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != 0)
        {
            _playerMover.Move(_inputReader.Direction);
        }

        PlayAnimation();

        if (_inputReader.GetIsJump())
        {
            _playerMover.Jump();
        }
    }

    public void TakeDemage(int demage)
    {
        if (_hitPoint > 0)
        {
            _hitPoint -= demage;
            _colorChangeWhenAttack.ChangeColor();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public bool IncreaseAmountOfHealth(int health)
    {
        if (_hitPoint < 200)
        {
            _hitPoint += health;
            
            return false;
        }
        else
        {
            return true;
        }
    }

    private void PlayAnimation()
    {
        _animator.SetBool(Shot, _weapon.IsFire);
        _animator.SetFloat(Speed, Mathf.Abs(_inputReader.Direction));
    }
}