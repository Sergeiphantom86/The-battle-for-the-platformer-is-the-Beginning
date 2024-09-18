using UnityEngine;

[RequireComponent(typeof(PaintAttack))]

public class PlayerHealth : MonoBehaviour
{
    private PaintAttack _paintAttack;
    private int _hitPoint = 200;

    private void Start()
    {
        _paintAttack = GetComponent<PaintAttack>();
    }

    public void ApplyDamage(int damage)
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

    public bool ApplyTreatment(int health)
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