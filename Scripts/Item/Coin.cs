using UnityEngine;
using System;

[RequireComponent(typeof(GroundDetector), typeof(Rigidbody2D))]

public class Coin : Item
{
    public event Action Collected;

    public void CollectCoin()
    {
        Collected?.Invoke();
    }
}