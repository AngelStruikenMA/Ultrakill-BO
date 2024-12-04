using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [System.Serializable]
    public class Weapon
    {
        public string weaponName;
        public GameObject projectilePrefab;
        public float fireRate;
        public float projectileSpeed;
        public int ammo;
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
        GameObject projectile = Instantiate(weapon.projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * weapon.projectileSpeed;

        if (weapon.shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(weapon.shootSound);
        }

        weapon.ammo--;
        Debug.Log(weapon.weaponName + " Ammo: " + weapon.ammo);
    }
}
