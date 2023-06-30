using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using System;
using MoreMountains.CorgiEngine;

public class ProjectileWeaponCP : ProjectileWeapon
{

    [MMInspectorGroup("Knockback", true)]
    public float knockbackSpeed = 10f;
    public float knockbackDistance = 2f;

    private float initialKnockbackDistance; // Store the initial knockback distance
    private bool hasAbilityKeyBeenPressed = false;




    //Currently not implemented
    protected override void Update()
    {

        base.Update();


        // Doesn't implement properly due to corgi engine having its own input logic
        if (Input.GetAxis("Player1_WeaponAbility") > 0 && !hasAbilityKeyBeenPressed)
        {
            
            
           // WeaponUse();
           // ApplyKnockbackToPlayer();
           // hasAbilityKeyBeenPressed = true;

        }



    }

    
    protected override void WeaponUse()
    {
        base.WeaponUse();

        ApplyKnockbackToPlayer(); // Need to call this on different input button
    }






    // Apply knockback to the player
    private void ApplyKnockbackToPlayer()
    {
        GameObject playerCharacter = GameObject.FindGameObjectWithTag("Player"); // Find the player character by tag
        if (playerCharacter != null)
        {
            Collider2D playerCollider = playerCharacter.GetComponent<Collider2D>(); // Get the player's collider

            Vector2 playerCenter = playerCollider.bounds.center; // Get the center position of the player character

            //Logic for mouse pos knockback
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 knockbackDirection = playerCenter - mousePosition;
            Vector2 normalizedKnockbackDirection = knockbackDirection.normalized;

            //Logic for Right Analog stick knockback
            //Vector2 analogInput = new Vector2(Input.GetAxis("RightStickHorizontal"), Input.GetAxis("RightStickVertical"));
            //Vector2 normalizedKnockbackDirection = new Vector2(-analogInput.x, analogInput.y).normalized; // Invert the y-component



            float initialKnockbackDistance = knockbackDistance; // Store the initial knockback distance

            // Perform the raycast and knockback
            RaycastHit2D[] hits = new RaycastHit2D[1];
            int platformLayerMask = LayerMask.GetMask("Platforms"); // Set the layer mask to include only the "Platform" layer
            int numHits = Physics2D.RaycastNonAlloc(playerCenter, normalizedKnockbackDirection, hits, Mathf.Abs(knockbackDistance), platformLayerMask);

            // Calculate the end position of the raycast
            Vector2 raycastEnd = playerCenter + normalizedKnockbackDirection * Mathf.Abs(knockbackDistance);

            // Visualize the raycast using the initial knockback distance
            Debug.DrawRay(playerCenter, normalizedKnockbackDirection * Mathf.Abs(initialKnockbackDistance), Color.red, 1f);

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
            // Reset knockback to initial knockback for future calls
            knockbackDistance = initialKnockbackDistance;
        }
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

        //hasAbilityKeyBeenPressed = false;
    }
}
