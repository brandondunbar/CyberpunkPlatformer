using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using System;
using MoreMountains.CorgiEngine;

public class ProjectileWeaponCP : ProjectileWeapon
{


    private WeaponAimCP weaponAimCP; // Reference to the WeaponAim class

    public override void Initialization()
    {
        base.Initialization();

        weaponAimCP = GetComponent<WeaponAimCP>(); // Get the WeaponAim component from the same GameObject or assign it manually

       
    }



    // Override the SpawnProjectile method
    public override GameObject SpawnProjectile(Vector3 spawnPosition, int projectileIndex, int totalProjectiles, bool triggerObjectActivation = true)
    {
        GameObject projectile = base.SpawnProjectile(spawnPosition, projectileIndex, totalProjectiles, triggerObjectActivation);

        // Apply knockback to the player
        if (Owner != null)
        {
            GameObject playerCharacter = GameObject.FindGameObjectWithTag("Player"); // Find the player character by tag
            if (playerCharacter != null)
            {
                Vector2 playerCenter = playerCharacter.transform.position; // Get the center position of the player character
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 knockbackDirection = playerCenter - mousePosition;
                Vector2 normalizedKnockbackDirection = knockbackDirection.normalized;
                float knockbackDistance = 2f; // Adjust this value to control the knockback distance
                float knockbackSpeed = 10f; // Adjust this value to control the speed of the knockback

                // Perform the raycast from the center of the player character
                RaycastHit2D[] hits = new RaycastHit2D[1];
                int platformLayerMask = LayerMask.GetMask("Platforms"); // Set the layer mask to include only the "Platform" layer
                int numHits = Physics2D.RaycastNonAlloc(playerCenter, normalizedKnockbackDirection, hits, Mathf.Abs(knockbackDistance), platformLayerMask);

                // Calculate the end position of the raycast
                Vector2 raycastEnd = playerCenter + normalizedKnockbackDirection * Mathf.Abs(knockbackDistance);

                // Visualize the raycast
                Debug.DrawRay(playerCenter, normalizedKnockbackDirection * Mathf.Abs(knockbackDistance), Color.red, 1f);

                if (numHits > 0)
                {
                    // If the raycast hits a collider, adjust the knockback distance
                    if (hits[0].distance < Mathf.Abs(knockbackDistance))
                    {
                        // If the distance to the collider is less than the original knockback distance, set knockback distance to 0
                        knockbackDistance = 0f;
                    }
                    else
                    {
                        knockbackDistance = -hits[0].distance;
                    }
                }

                StartCoroutine(PerformKnockback(normalizedKnockbackDirection, knockbackDistance, knockbackSpeed));
            }
        }

        return projectile;
    }







    // Coroutine for smooth knockback movement
    private IEnumerator PerformKnockback(Vector2 direction, float distance, float speed)
    {
        float remainingDistance = Mathf.Abs(distance);
        while (remainingDistance > 0f)
        {
            float moveAmount = Mathf.Min(remainingDistance, speed * Time.deltaTime);
            Owner.transform.Translate(direction * moveAmount);
            remainingDistance -= moveAmount;
            yield return null;
        }
    }
}
