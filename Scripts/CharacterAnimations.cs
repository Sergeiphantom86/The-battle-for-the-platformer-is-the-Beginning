using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    public const string Speed = nameof(Speed);
    public const string Shot = nameof(Shot);
    public const string Died = nameof(Died);

    [SerializeField] private Animator _animator;

    public void Run(float direction)
    {
        _animator.SetFloat(Speed, Mathf.Abs(direction));
    }

    public void Die(bool died)
    {
        _animator.SetBool(Died, died);
    }

    public void Shoot(bool isFire)
    {
        _animator.SetBool(Shot, isFire);
    }
}