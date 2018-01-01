using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private Rigidbody rb;
    int coinPoints = 5;
    int healthPoints = 25;
    int ammoPoints = 30;
    AudioSource itemAudio;
    public AudioClip coinPickup;
    public AudioClip healthPickup;
    public AudioClip ammoPickup;
    PlayerHealth life;
    public Weapon weapon;

    void Start ()
    {
        itemAudio = GetComponent<AudioSource>();
        life = GetComponent<PlayerHealth>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag ("Coin"))
        {
            other.gameObject.SetActive(false);

            ScoreManager.score += coinPoints;

            itemAudio.clip = coinPickup;
            itemAudio.Play();
        }

        else if (other.gameObject.CompareTag("Health"))
        {
            other.gameObject.SetActive(false);

            life.currentHealth += healthPoints;

            itemAudio.clip = healthPickup;
            itemAudio.Play();
        }

        else if (other.gameObject.CompareTag("Ammo"))
        {
            other.gameObject.SetActive(false);

            weapon.bulletsLeft += ammoPoints;

            itemAudio.clip = ammoPickup;
            itemAudio.Play();
        }
    }
}

