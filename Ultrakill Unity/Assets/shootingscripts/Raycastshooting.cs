using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycastshooting : MonoBehaviour
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

    System.Collections.IEnumerator FireWeapon(Weapon weapon)
    {
        // Create a LineRenderer instance
        LineRenderer line = Instantiate(weapon.lineRendererPrefab);
        line.SetPosition(0, firePoint.position);

        RaycastHit hit;
        Vector3 direction = firePoint.forward;

        if (Physics.Raycast(firePoint.position, direction, out hit, weapon.range))
        {
            // Hit something
            line.SetPosition(1, hit.point);

            // Apply damage if the object has an EnemyHealth component
          /*  EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(weapon.damage);
            }
    */
            // Optionally, instantiate hit effects here
        }
        else
        {
            // Hit nothing, draw the line to the maximum range
            line.SetPosition(1, firePoint.position + direction * weapon.range);
        }

        // Show the line for a brief moment
        yield return new WaitForSeconds(0.02f);
        Destroy(line.gameObject);
    }
}


