using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particleshooting : MonoBehaviour 
{
    [System.Serializable]
    public class Weapon
    {
        public string weaponName;
        public float fireRate;
        public float range;
        public int ammo;
        public float damage;
        public LineRenderer lineRendererPrefab;
        public AudioClip shootSound;
    }

    public Weapon[] weapons;
    public Transform firePoint;
    public AudioSource audioSource;

    [Header("Hit Effects")]
    public GameObject hitEffect;       // Default particle effect for non-enemies
    public GameObject enemyHitEffect;  // Particle effect for enemies

    [Header("Gun Animation")]
    public Animator gunAnimator;       // Reference to your gun's Animator

    private int currentWeaponIndex = 0;
    private float nextFireTime = 0f;

    
    void Update()
    {
        HandleWeaponSwitch();
        HandleShooting();
    }

    void HandleWeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
            Debug.Log("Switched to: " + weapons[currentWeaponIndex].weaponName);
        }
    }

    void HandleShooting()
    {
        Weapon weapon = weapons[currentWeaponIndex];

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && weapon.ammo > 0)
        {
            Shoot(weapon);
            nextFireTime = Time.time + 1f / weapon.fireRate;
        }
    }

    void Shoot(Weapon weapon)
    {
        // ====== Trigger the Gun's Shoot Animation ======
        if (gunAnimator != null)
        {
            // Set the "Shoot" trigger in your Animator Controller
            gunAnimator.SetTrigger("Shoot");
        }

        // ====== Continue with Shooting Logic ======
        StartCoroutine(FireWeapon(weapon));

        // Play shoot sound (if assigned)
        if (weapon.shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(weapon.shootSound);
        }

        // Reduce Ammo
        weapon.ammo--;
        Debug.Log(weapon.weaponName + " Ammo: " + weapon.ammo);
    }

    IEnumerator FireWeapon(Weapon weapon)   
    {
        // Create (instantiate) a LineRenderer instance
        LineRenderer line = Instantiate(weapon.lineRendererPrefab);
        line.SetPosition(0, firePoint.position);

        RaycastHit hit;
        Vector3 direction = firePoint.forward;

        if (Physics.Raycast(firePoint.position, direction, out hit, weapon.range))
        {
            // Hit something
            line.SetPosition(1, hit.point);

            // Check if the object is tagged "Enemy"
            if (hit.collider.CompareTag("Enemy"))
            {
                // Spawn enemy-specific hit effect if assigned
                if (enemyHitEffect != null)
                {
                    GameObject effect = Instantiate(enemyHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(effect, 2f);
                }

                // Destroy the enemy GameObject immediately
                Destroy(hit.collider.gameObject);
            }
            else
            {
                // Not an enemy, spawn the default hit effect
                if (hitEffect != null)
                {
                    GameObject effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(effect, 2f);
                }
            }
        }
        else
        {
            // No hit: draw the line to the maximum range
            line.SetPosition(1, firePoint.position + direction * weapon.range);
        }

        // Show the line for a brief moment
        yield return new WaitForSeconds(0.02f);
        Destroy(line.gameObject);
    }
}
