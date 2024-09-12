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
            bool fullHealth = _player.IncreaseAmountOfHealth(apple.Value);

            if (fullHealth)
            {
                _bag.AddFirstAidKit(apple.Value);
            }
            
            apple.Collect();
        }
    }
}