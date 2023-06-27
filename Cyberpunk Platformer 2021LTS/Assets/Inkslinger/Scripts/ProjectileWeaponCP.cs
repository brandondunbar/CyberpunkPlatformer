using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using System;
using MoreMountains.CorgiEngine;

public class ProjectileWeaponCP : ProjectileWeapon
{



    // Override the SpawnProjectile method
    public override GameObject SpawnProjectile(Vector3 spawnPosition, int projectileIndex, int totalProjectiles, bool triggerObjectActivation = true)
    {
        GameObject projectile = base.SpawnProjectile(spawnPosition, projectileIndex, totalProjectiles, triggerObjectActivation);

        // Apply knockback to the player
        if (Owner != null)
        {
            Vector3 knockbackDirection = spawnPosition - Owner.transform.position;
            Vector3 normalizedKnockbackDirection = -knockbackDirection.normalized; // Multiply by -1 to invert the direction
            float knockbackDistance = 2f; // Adjust this value to control the knockback distance
            float knockbackSpeed = 10f; // Adjust this value to control the speed of the knockback

            RaycastHit hit;
            if (Physics.Raycast(Owner.transform.position, normalizedKnockbackDirection, out hit, Mathf.Abs(knockbackDistance)))
            {
                // If the raycast hits a collider, adjust the knockback distance
                knockbackDistance = -hit.distance;
            }

            StartCoroutine(PerformKnockback(normalizedKnockbackDirection, knockbackDistance, knockbackSpeed));
        }

        return projectile;
    }




    //Generated from chatgpt- makes the movement smoother
    private IEnumerator PerformKnockback(Vector3 direction, float distance, float speed)
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
