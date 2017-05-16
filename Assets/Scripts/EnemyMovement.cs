using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    Transform player;
    private PlayerHealth _playerHealth;
    private EnemyHealth _enemyHealth;
    private NavMeshAgent _nav;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = player.GetComponent<PlayerHealth>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (_enemyHealth.CurrentHealth > 0 && _playerHealth.CurrentHealth > 0)
        {
            _nav.SetDestination(player.position);
        }
        else
        {
            _nav.enabled = false; 
        }
	}
}
