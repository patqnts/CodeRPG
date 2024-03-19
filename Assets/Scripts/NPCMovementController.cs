using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float followRadius = 1.5f; // Radius within which the NPC follows the player
    public Animator animator; // Reference to the animator component

    Rigidbody2D rb; // Reference to the Rigidbody2D component

    private GameObject playerObject;
    [Header("Chapter 1 USE")]
    public Transform targetLocation;

    void Start()
    {
        playerObject = player.gameObject;
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the NPC
        
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PullingScene();
        }
        // Calculate the distance between the NPC and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Face the player at all times
        Vector2 direction = (player.position - transform.position).normalized;
        

        // If player is within the follow radius, stop moving
        if (distanceToPlayer <= followRadius)
        {
            rb.velocity = Vector2.zero;
            animator.Play("KatambayIdle");
            return;
        }
        SetAnimation(direction);
        // Move towards the player
        rb.velocity = direction * 2f;
    }
    public void PullingScene()
    {
        // 2. Carry the player (make it its child object) above the NPC's head
        player.SetParent(transform);
        player.localPosition = new Vector3(0f, 1f, 0f); // Adjust the y position to make it appear above the NPC's head
        player.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        // 3. Move the player to the target location after a delay
        StartCoroutine(MovePlayerToTargetAfterDelay());
    }

    IEnumerator MovePlayerToTargetAfterDelay()
    {
        yield return new WaitForSeconds(1f); // Wait for 3 seconds

        // Move the player to the target location
        if (targetLocation != null)
        {
            // Unparent the player from the NPC
            player = targetLocation;
        }

        StartCoroutine(Uncarry());
    }

    IEnumerator Uncarry()
    {
        yield return new WaitForSeconds(10f);
        
        
        player = playerObject.transform;
        playerObject.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        playerObject.transform.SetParent(null);
        PixelCrushers.DialogueSystem.Sequencer.Message("SceneEnd");
    }




    void SetAnimation(Vector2 direction)
    {
        if (animator != null)
        {
            // Set animation based on direction
            if (direction.magnitude > 0.1f)
            {
                animator.Play("Katambay" + GetDirectionString(direction));
            }
            else
            {
                animator.Play("KatambayIdle");
            }
        }
    }

    string GetDirectionString(Vector2 direction)
    {
        // Determine the direction based on the angle between the direction vector and up vector
        float angle = Vector2.SignedAngle(Vector2.up, direction);
        if (angle >= -45 && angle < 45)
            return "Up";
        else if (angle >= 45 && angle < 135)
            return "Left";
        else if (angle >= -135 && angle < -45)
            return "Right";
        else
            return "Down";
    }
}
