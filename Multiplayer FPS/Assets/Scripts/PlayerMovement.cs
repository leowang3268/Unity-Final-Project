using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Animator
[RequireComponent(typeof(Animator))]


public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 3f;
    public float gravity = -9.81f * 4f;
    public float jumpHeight = 9f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

	// Componenet caching
	private Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal"); // movement in horizontal axis
        float z = Input.GetAxis("Vertical"); // movement in vertical axis

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

		// Animate movement
		animator.SetFloat("ForwardVelocity", z);

    }
}
