using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FixedJoystick joystick;
    public float moveSpeed = 5f;
    private Vector2 movementInput;
    private Animator animator;
    public CircleCollider2D circleCollider;
    private Rigidbody2D rb;

    private void Start()
    {
        joystick = FindObjectOfType<FixedJoystick>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 movement = new Vector2(movementInput.x, movementInput.y);
        movement.Normalize();

        rb.velocity = movement * moveSpeed;

        UpdateAnimatorParameters(movement);
    }

    private void UpdateAnimatorParameters(Vector2 movement)
    {
        animator.SetFloat("x", movement.x);
        animator.SetFloat("y", movement.y);
    }

    public void OnInteractionButtonClick()
    {
        StartCoroutine(EnableDisableCollider());
    }

    private IEnumerator EnableDisableCollider()
    {
        circleCollider.enabled = true;
        yield return new WaitForSeconds(1f);
        circleCollider.enabled = false;
    }

    private void OnDisable()
    {
        // Set velocity to zero when the script is disabled
        rb.velocity = Vector2.zero;
    }
}
