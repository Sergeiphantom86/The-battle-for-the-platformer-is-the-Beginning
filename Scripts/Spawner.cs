using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Apple _apple;
    [SerializeField] private float _respawnDuration;

    private void Start()
    {
        int quantityAppels = 5;

        for (int i = 0; i <= quantityAppels; i++)
        {
            _apple = Instantiate(_apple, _apple.GetRandomPosition(), quaternion.identity);
        }
    }

    private void OnEnable()
    {
        _apple.Collected += Respawn;
    }


    private void OnDisable()
    {
        _apple.Collected -= Respawn;
    }

    private void Respawn()
    {
        StartCoroutine(SpawnAppleWithDelay());
    }

    private IEnumerator SpawnAppleWithDelay()
    {
        yield return new WaitForSeconds(_respawnDuration);

        _apple.transform.position = _apple.GetRandomPosition();
        _apple.gameObject.SetActive(true);
    }
}