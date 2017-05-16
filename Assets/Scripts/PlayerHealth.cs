using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int StartingHealth = 100;
    public int CurrentHealth;
    public Image DamageImage;
    public float FlashSpeed = 5f;
    public Color FlashColour = new Color(1f, 0f, 0f, 0.1f);

    //PlayerMovement playerMovement;
    private bool _isDead;
    private bool _damaged;

    private void Awake()
    {

        CurrentHealth = StartingHealth;
    }
    // Update is called once per frame
    void Update ()
    {
		if (_damaged)
        {
            DamageImage.color = FlashColour;
        }
        else
        {
            DamageImage.color = Color.Lerp(DamageImage.color, Color.clear, FlashSpeed * Time.deltaTime);     
        }

        _damaged = false;
	}

    public void TakeDamage(int amount)
    {
        _damaged = true;
        CurrentHealth -= amount;

        if (CurrentHealth <= 0 && !_isDead)
        {
            Death();
        }
    }

    private void Death()
    {
        _isDead = true;

        

    }
}
