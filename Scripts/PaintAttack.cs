using System.Collections;
using UnityEngine;

public class PaintAttack : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void ChangeColor()
    {
        if (_spriteRenderer != null)
        {
            StartCoroutine(ReturnDefaultColor());
        }
    }

    private IEnumerator ReturnDefaultColor()
    {
        float colorChangeDelay = 0.01f;

        WaitForSeconds wait = new(colorChangeDelay);

        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = Color.red;

            yield return wait;

            _spriteRenderer.color = Color.white;
        }
    }
}