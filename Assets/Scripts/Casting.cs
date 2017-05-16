using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour {

    private ParticleSystem _wandParticleSystem;
    private bool _isShooting;

	// Use this for initialization
	void Start () {
        _wandParticleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
	}
	
    void Shoot()
    {
        _isShooting = true;
        _wandParticleSystem.Play();
    }
    void StopShoot()
    {
        _isShooting = false;
        _wandParticleSystem.Stop();
    }

    // Update is called once per frame
    void Update() {
        if (!_isShooting && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.Controller.Touch) > 0.5)
        {
            Shoot();
        }
        else if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.Controller.Touch) <= 0.5)
        {
            StopShoot();
        }
	}
}
