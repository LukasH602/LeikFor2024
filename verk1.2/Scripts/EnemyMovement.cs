using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    // Vísun í umbreytingu leikmannsins.
    public Transform player;

    // Vísun í NavMeshAgent hlutan fyrir leiðsagnartækni.
    private NavMeshAgent navMeshAgent;

    // Start er kallað fyrir fyrstu ramma uppfærsluna.
    void Start()
    {
        // Fá og vista NavMeshAgent hlutann sem tengist þessu hlutverki.
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update er kallað einu sinni á hverju ramma.
    void Update()
    {
        // Ef vísun er að leikmanninum...
        if (player != null)
        {
            // Setja áfangastað óvinarins á núverandi stöðu leikmannsins.
            navMeshAgent.SetDestination(player.position);
        }
    }
}
