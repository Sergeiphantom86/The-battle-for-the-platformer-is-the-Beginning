using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Coin _coinsPrefab;
    [SerializeField] private Apple _applePrefab;
    [SerializeField] private int _respawnDuration;

    private Coin _coin;
    private WaitForSeconds _wait;

    private void Start()
    {
        _wait = new WaitForSeconds(_respawnDuration);

        CreateApple(_applePrefab);
    }

    private void OnEnable()
    {
        _coin = Instantiate(_coinsPrefab, _coinsPrefab.GetRandomPosition(), quaternion.identity);

        _coin.Collected += Respawn;
    }

    private void OnDisable()
    {
        _coin.Collected -= Respawn;
    }

    private void CreateApple(Apple apple, int quantityApple = 5)
    {
        for (int i = 0; i <= quantityApple; i++)
        {
            Instantiate(apple, _applePrefab.GetRandomPosition(), quaternion.identity);
        }
    }

    private void Respawn()
    {
        StartCoroutine(SpawnCoinWithDelay());
    }

    private IEnumerator SpawnCoinWithDelay()
    {
        yield return _wait;

        _coin.gameObject.SetActive(true);
        _coin.transform.position = _coin.GetRandomPosition();
    }
}