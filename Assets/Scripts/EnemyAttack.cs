using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public float TimeBetweenAttacks = 0.5f;
    public int AttackDamge = 10;

    private Animator _animator;
    private GameObject _player;
    private PlayerHealth _playerHealth;
    private bool _playerInRange;
    private float _timer;
    private EnemyHealth _enemyHealth;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<PlayerHealth>();
        _animator = GetComponent<Animator>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            _playerInRange = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
        {
            _playerInRange = false;
        }
    }
    // Update is called once per frame
    void Update ()
    {
        _timer += Time.deltaTime;

        if (_timer >= TimeBetweenAttacks && _playerInRange && _enemyHealth.CurrentHealth > 0)
        {
            Attack();
        }

        if (_playerHealth.CurrentHealth <= 0)
        {
            _animator.SetTrigger("PlayerDead");
        }
	}

    private void Attack()
    {
        _timer = 0f;

        if (_playerHealth.CurrentHealth > 0)
        {
            _playerHealth.TakeDamage(AttackDamge);
            _animator.SetTrigger("Attack");
        }
    }
}
