using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public int DamagePerShot = 50;
    public float TimeBetweenBullets = 1.0f;
    public float range = 100f;

    private float _timer;
    private Ray _shootRay;
    private RaycastHit _shootHit;
    private int _shootableMask;
    private ParticleSystem _wandParticles;
    private LensFlare _wandFlare;
    private float _effectsDisplayTime = 0.3f;
    private Transform _rightHandAnchor;
    private AudioSource _wandSound;

    private float _timeDelayDefault = 0.3f;
    private float _timeDelayShoot;
    private bool _canShoot;

    [SerializeField]
    OculusHapticsController rightControllerHaptics;

    private void Awake()
    {
        _shootableMask = LayerMask.GetMask("Shootable");
        _wandParticles = GameObject.Find("WandParticleSystem").GetComponent<ParticleSystem>();
        _wandFlare = GameObject.Find("WandFlare").GetComponent<LensFlare>();
        _wandSound = GameObject.Find("WandSound").GetComponent<AudioSource>();
        _rightHandAnchor = GameObject.Find("RightHandAnchor").GetComponent<Transform>();

        _timeDelayShoot = 0f;
        _canShoot = false;

        _wandFlare.brightness = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.Controller.Touch) > 0.5 && _timer >= TimeBetweenBullets)
        {
            _wandSound.Play();
            _canShoot = true;           
        }

        if (_timer >= TimeBetweenBullets * _effectsDisplayTime)
        {
            DisableEffects();
        }

        if (_timer >= TimeBetweenBullets)
        {
            _wandFlare.brightness = 0.3f;
        }

        if (_canShoot)
        {
            if (_timeDelayShoot >= _timeDelayDefault)
            {
                Shoot();
                _timeDelayShoot = 0f;
                _canShoot = false;
                _wandFlare.brightness = 0;
                rightControllerHaptics.Vibrate(VibrationForce.Hard);
            }

            _timeDelayShoot += Time.deltaTime;
        }
    }

    public void DisableEffects()
    {
        _wandParticles.Stop();
    }

    private void Shoot()
    {
        _timer = 0f;

        _wandParticles.Stop();
        _wandParticles.Play();

        _shootRay.origin = transform.position;
        _shootRay.direction = transform.forward;

        if (Physics.Raycast(_shootRay, out _shootHit, range, _shootableMask))
        {
            EnemyHealth enemyHealth = _shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {           
                enemyHealth.TakeDamage(DamagePerShot, _shootHit.point);
                _shootHit.rigidbody.AddForce(transform.forward * 200);        
            }
        }
    }
}
