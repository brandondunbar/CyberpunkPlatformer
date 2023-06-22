using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using MoreMountains.Tools;




public class CharacterSlide : CharacterCrouch
{
    [SerializeField] float slideSpeed = 1f;


    protected Vector2 facingDirection;
    protected bool sliding = false;



  
    

    protected override void HandleInput()
    {
        if (_verticalInput < -_inputManager.Threshold.y)
        {
           if(sliding) { return; }
            
            ActivateSlide();
            
        }
    }


  



    /// <summary>
    /// Every frame, we check if we're crouched and if we still should be
    /// </summary>
    public override void ProcessAbility()
    {
        base.ProcessAbility();
        
    }



    protected void ActivateSlide()
    {
        if (!sliding)
        {
            StartCoroutine(Slide());

        }

    }


    IEnumerator Slide()
    {
        if (sliding) { yield break; }
        sliding = true;
        float startTime = Time.time;
        //Get the direction player is facing
        GetFacingDirection();
       
        while (Time.time - startTime < 1f)  // Code to be executed during the 1-second loop
        {
            Crouch();
            Debug.Log("Looping...");
            // Apply movement in the facing direction
            transform.Translate(facingDirection * slideSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame

        }

        // Code to be executed after the loop completes
        Debug.Log("Loop finished");
        sliding = false;
        ExitCrouch();
    }
    


    protected void GetFacingDirection()
    {
        // Get the horizontal input axis (e.g., -1 for left, 1 for right)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Set the facing direction based on the horizontal input
        if (horizontalInput < 0)
        {
            facingDirection = Vector2.left;
        }
        else if (horizontalInput > 0)
        {
            facingDirection = Vector2.right;
        }
    }






}
