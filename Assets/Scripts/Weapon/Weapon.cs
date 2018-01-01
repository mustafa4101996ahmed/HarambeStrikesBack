using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Weapon : MonoBehaviour {

    private Animator anim;
    private AudioSource _AudioSource;

    public float range = 100f;
    public int bulletsPerMag = 30;  // Bullets per each magazine
    public int bulletsLeft = 60;   // Total bullets we have 

    public int currentBullets;      // The current bullets in our magazine

    public enum ShootMode { Auto, Semi }
    public ShootMode shootingMode;

    public Transform shootPoint;    // The point from which the bulled leaves the muzzle
    public GameObject hitParticles;
    public GameObject bulletImpact;

    public ParticleSystem muzzleFlash;  // Muzzle Flash
    public AudioClip shootSound;
    public AudioClip ammoFinished;

    public float fireRate = 0.1f;   // The delay between each shot
    public int damage = 20;

    float fireTimer;
    private bool isReloading;
    private bool isAiming;
    private bool shootInput;

    private Vector3 originalPosition;
    public Vector3 aimPosition;
    public float aodSpeed = 8;

    int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.

    public Text currentAmmo;
    public Text totalAmmo;

    void Start () 
	{

        shootableMask = LayerMask.GetMask("Shootable");

        anim = GetComponent<Animator>();
        _AudioSource = GetComponent<AudioSource>();

        currentBullets = bulletsPerMag;
        originalPosition = transform.localPosition;

        
    }
	
	void Update () 
	{
        currentAmmo.text = currentBullets.ToString();
        totalAmmo.text = bulletsLeft.ToString();

        switch (shootingMode)
        {
            case ShootMode.Auto:
                shootInput = Input.GetButton("Fire1");
                break;

            case ShootMode.Semi:
                shootInput = Input.GetButtonDown("Fire1");
                break;
        }

		if(shootInput)
        {
            if (currentBullets > 0)
                Fire(); //Execute Fire function when we press left mouse button
            else if (bulletsLeft > 0)
                DoReload();
            else if (currentBullets <= 0 && bulletsLeft <= 0)
            {
                if (fireTimer < fireRate)
                    return;
                else
                {
                    Debug.Log("Ammo is finished");
                    PlayAmmoFinishedSound();
                    fireTimer = 0.0f; //Reset FireTimer
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(currentBullets < bulletsPerMag && bulletsLeft > 0)
                DoReload();
        }

        if(fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }

        AimDownSights();
    }

    void FixedUpdate ()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        isReloading = info.IsName("Reload");
        anim.SetBool("Aim", isAiming);
        //if (info.IsName("Fire")) anim.SetBool("Fire", false);
    }

    private void AimDownSights()
    {
        if (Input.GetButton("Fire2") && !isReloading)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * aodSpeed);
            isAiming = true;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * aodSpeed);
            isAiming = false;
        }
    }

    private void Fire ()
    {
        if (fireTimer < fireRate || currentBullets <= 0 || isReloading)
        {
            return;
        }

        RaycastHit hit;

        if(Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range, shootableMask))
        {
            Debug.Log(hit.transform.name + " has been shot!");

            GameObject hitParticleEffect = Instantiate(hitParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            //GameObject bulletHole = Instantiate(bulletImpact, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));

            Destroy(hitParticleEffect, 1f);
           // Destroy(bulletHole, 2f);

            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

            // If the EnemyHealth component exist...
            if (enemyHealth != null)
            {
                // ... the enemy should take damage.
                enemyHealth.TakeDamage(damage, hit.point);
            }
        }

        anim.CrossFadeInFixedTime("Fire", 0.01f);   // Play the fire animation
        muzzleFlash.Play(); // Show Muzzle flash
        PlayShootSound(); // Play Shooting Sound Effect

        //anim.SetBool("Fire", true);
        currentBullets--; // Deduct 1 bullet
        fireTimer = 0.0f; //Reset FireTimer
        currentAmmo.text = currentBullets.ToString();
    }

    public void Reload ()
    {
        Debug.Log("Reload successful");
        if (bulletsLeft <= 0) return;

        int bulletsToLoad = bulletsPerMag - currentBullets;
        int bulletsToDeduct = (bulletsLeft >= bulletsToLoad) ? bulletsToLoad : bulletsLeft;

        bulletsLeft -= bulletsToDeduct;
        currentBullets += bulletsToDeduct;

        currentAmmo.text = currentBullets.ToString();
        totalAmmo.text = bulletsLeft.ToString();
    }

    private void DoReload()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        if (isReloading) return;
        Debug.Log("isReloading");
        
        anim.CrossFadeInFixedTime("Reload", 0.01f);
        Reload();
    }

    private void PlayShootSound ()
    {
        _AudioSource.PlayOneShot(shootSound);
    }

    private void PlayAmmoFinishedSound()
    {
        _AudioSource.PlayOneShot(ammoFinished);
        
    }
}


