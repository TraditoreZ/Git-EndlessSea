using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class playerAnimManager : NetworkBehaviour
{
    CharacterController controller;
    public float speed;
    public float walkspeed;
    public float runSpeed;
    public float jumpSpeed;
    public float gravity;
    public bool pistol;
    Vector3 moveDirection = Vector3.zero;

    public Vector3 movementSpeed = Vector3.zero;

    Animator animator;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
        controller = transform.GetComponent<CharacterController>();

    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            movementSpeed = moveDirection;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = Mathf.Lerp(speed, runSpeed, Time.deltaTime * 2);
            }
            else
            {
                speed = Mathf.Lerp(speed, walkspeed, Time.deltaTime * 2);
            }
            movementSpeed *= speed;
            animator.SetFloat("MovementX", movementSpeed.x);
            animator.SetFloat("MovementZ", movementSpeed.z);

            if (Input.GetKey(KeyCode.Space))
            {
                animator.SetBool("Jump", true);
            }
            else
            {
                animator.SetBool("Jump", false);
            }
        }



    }
}
