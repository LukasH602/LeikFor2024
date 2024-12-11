using System;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public static event Action OnCollected;

    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
