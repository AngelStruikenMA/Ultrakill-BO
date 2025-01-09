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

    [Header("Hit Effect")]
    public GameObject hitEffect; // Assign your particle effect prefab here

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
        StartCoroutine(FireWeapon(weapon));

        if (weapon.shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(weapon.shootSound);
        }

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

            // === Spawn a particle effect at the hit point ===
            if (hitEffect != null)
            {
                // Instantiate the effect so it faces outward from the surface
                GameObject effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                // Destroy the effect after a short time to prevent clutter
                Destroy(effect, 2f);
            }

            // === Optionally apply damage if you have an EnemyHealth script ===
            /*
            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(weapon.damage);
            }
            */
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
