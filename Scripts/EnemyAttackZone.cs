using UnityEngine;
using System.Collections;

public class EnemyAttackZone : MonoBehaviour
{
    [SerializeField] private float _radiusCircle;
    [SerializeField] private LayerMask _objecktSelectionMask;
    [SerializeField] private AttackPoint _attackPoint;

    private Coroutine _coroutine;

    private int _damage = 10;
    private float _damageDelay = 1f;

    public float TargetPosition { get; private set; }
    public bool IsLocatedInTargetZone { get; private set; }
    public bool IsCame { get; private set; }

    private void Update()
    {
        FindInAttackRadius();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (player != null)
            {
                IsCame = true;

                _coroutine = StartCoroutine(DelayDamage(player));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            IsCame = false;
            
            if (_coroutine != null)
            {
                StopAllCoroutines();
            }
        }
    }

    private void FindInAttackRadius()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(_attackPoint.transform.position, _radiusCircle, _objecktSelectionMask);

        if (hitPlayer != null)
        {
            TargetPosition = hitPlayer.transform.position.x;
            
            IsLocatedInTargetZone = true;
        }
        else
        {
            IsLocatedInTargetZone = false;
        }
        
        
    }

    private IEnumerator DelayDamage(Player player)
    {
        WaitForSeconds wait = new(_damageDelay);

        while (player != null)
        {
            player.TakeDemage(_damage);

            yield return wait;
        }
    }
}