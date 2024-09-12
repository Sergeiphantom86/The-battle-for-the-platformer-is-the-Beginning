using System.Collections;
using UnityEngine;

public class ColorChangeWhenAttack : MonoBehaviour
{
    public void ChangeColor(int indexOfChildObject = 0)
    {
        StartCoroutine(ReturnDefaultColor(indexOfChildObject));
    }

    private IEnumerator ReturnDefaultColor(int indexOfChildObject)
    {
        float colorChangeDelay = 0.01f;

        WaitForSeconds wait = new(colorChangeDelay);

        transform.GetChild(indexOfChildObject).GetComponent<SpriteRenderer>().color = Color.red;

        yield return wait;

        transform.GetChild(indexOfChildObject).GetComponent<SpriteRenderer>().color = Color.white;
    }
}