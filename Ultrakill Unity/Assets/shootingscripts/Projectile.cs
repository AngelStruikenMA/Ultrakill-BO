using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 5f; // Time in seconds before the projectile is destroyed automatically

    void Start()
    {
        // Destroy the projectile after 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }
void Update()
{

}
    void OnCollisionEnter(Collision collision)
    {
        // Destroy the projectile upon collision with any object
        Destroy(gameObject);
    }
}
