using System.Collections;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;     // Drag your bullet prefab here
    public float fireRate = 2f;         // Shots per second
    public Transform firePoint;         // Where bullets spawn (child object or null)

    void Start()
    {
        if (firePoint == null)
            firePoint = transform;  // Use enemy position if no fire point

        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            // Find player and shoot toward him
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                BulletMovement bulletScript = bullet.GetComponent<BulletMovement>();
                if (bulletScript == null)
                    bulletScript = bullet.AddComponent<BulletMovement>();

                // Shoot toward player
                bulletScript.Init(player.transform.position, 8f);

                // Set bullet damage to 1
                DamageDealer bulletDamage = bullet.GetComponent<DamageDealer>();
                if (bulletDamage != null)
                    bulletDamage.SetDamageByWave(0);  // 1 damage
            }

            yield return new WaitForSeconds(1f / fireRate);
        }
    }
}