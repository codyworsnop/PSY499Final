using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

    public int StartingHealth = 100;
    public int CurrentHealth;
    public float SinkSpeed = 2.5f;
    public int ScoreValue = 10;
    public AudioClip SpiderDyingClip;

    private Animator _animator;
    private CapsuleCollider _capsuleCollider;
    private bool _isDead;
    private bool _isSinking;
    private bool _isSinkTimerRunning;
    private float _timeBeforeSinkTimer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _timeBeforeSinkTimer = 8.0f;
        _isSinkTimerRunning = false;

        CurrentHealth = StartingHealth;
    }

    // Update is called once per frame
    void Update ()
    {
        if (_isSinkTimerRunning)
        {
            _timeBeforeSinkTimer -= Time.deltaTime;

            if (_timeBeforeSinkTimer <= 0)
            { 
                _isSinking = true;
                Destroy(gameObject, 4f);
            }
        }     

		if (_isSinking)
        {
            transform.Translate(-Vector3.up * SinkSpeed * Time.deltaTime);
        }
	}

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (_isDead)
        {
            return;
        }

        CurrentHealth -= amount;

        //hitParticles.transform.position = hitpoint; 
        //hitparticles.play();

        if (CurrentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        //remove from spider list
        GameObject.Find("EnemyManager").GetComponent<EnemyManager>().CurrentSpiders.Remove(gameObject);
        var dyingSound = GetComponent<AudioSource>();

        dyingSound.clip = SpiderDyingClip;
        dyingSound.loop = false;
        dyingSound.Play();

        GetComponent<BoxCollider>().isTrigger = false;
        _isDead = true;
        _animator.SetTrigger("Dead");
    }

    public void StepSound()
    {
      //  AudioSource stepSource = GameObject.Find("StepSoundSource").GetComponent<AudioSource>();
       // AudioSource gruntSource = GameObject.Find("GruntSoundSource").GetComponent<AudioSource>();

        if (Random.Range(0, 30) <= 1)
        {
          //  gruntSource.Play();
        }

        //stepSource.pitch = Random.Range(.4f, 1.6f);
        //stepSource.volume = Random.Range(.4f, 1.6f);

        //stepSource.Play();
    }

    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        _isSinkTimerRunning = true;
    }
}
