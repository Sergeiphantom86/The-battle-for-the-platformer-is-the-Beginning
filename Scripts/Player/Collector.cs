using UnityEngine;

[RequireComponent(typeof(Player))]

public class Collector : MonoBehaviour
{
    private Bag _bag;
    private Player _player;

    private void Awake()
    {
        _bag = new Bag();
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Apple apple))
        {
            bool fullHealth = _player.TakeTreatment(apple.Value);

            if (fullHealth == false)
            {
                apple.Collect(apple.name);
            }
        }

        if(other.TryGetComponent(out Coin coin))
        {
            coin.Collect(nameof(coin));
            _bag.AddCoin(coin.Value);
        }
    }
}