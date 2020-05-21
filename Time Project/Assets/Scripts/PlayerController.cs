using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 3f;
    [SerializeField]
    private Vector3 velocity;
    private float gravityForce = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    public float jumpHeight = 3f;
    public Animator animator;
    public bool isSlowed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        animator.SetBool("InAir", !isGrounded);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Remove y fields from look vectors to prevent vertical movement.
        Transform cameraTransform = Camera.main.transform;
        Vector3 forward = cameraTransform.forward;
        forward.y = 0;

        Vector3 right = cameraTransform.right;
        right.y = 0;
        
        Vector3 moveVector = (right * x) + (forward * z);
        moveVector.Normalize();

        controller.Move(moveVector * speed * Time.deltaTime * (isSlowed ? SlowdownStatics.PLAYER_SLOWDOWN : 1f));
        transform.LookAt(transform.position + moveVector);
        animator.SetFloat("speed", moveVector.magnitude * speed * Time.deltaTime);
        animator.SetFloat("animPlaybackSpeed", isSlowed ? SlowdownStatics.PLAYER_SLOWDOWN : 1f);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityForce);
        }

        velocity.y += gravityForce * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    void OnTriggerEnter(Collider otherCollider)
    {
        Debug.Log("Entered Trigger: " + otherCollider.gameObject.tag);
        if (otherCollider.tag == "Slowdown")
        {
            Debug.Log("2");
            isSlowed = true;
        }
    }

    void OnTriggerExit(Collider otherCollider)
    {
        Debug.Log("Exited Trigger: " + otherCollider.name);
        if (otherCollider.tag == "Slowdown")
        {
            Debug.Log("4");
            isSlowed = false;
        }
    }
}
