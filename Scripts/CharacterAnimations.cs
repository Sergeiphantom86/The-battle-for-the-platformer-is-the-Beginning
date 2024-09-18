using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private const string Speed = nameof(Speed);
    private const string Shot = nameof(Shot);
    private const string Died = nameof(Died);

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