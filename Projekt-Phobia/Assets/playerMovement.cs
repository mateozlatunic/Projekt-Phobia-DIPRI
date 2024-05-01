using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -21f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;
    public float sprintModifier;
    public Camera normalCam;
    public Transform flashlightParent;
    public AudioSource WalkSound;

    private bool isMoving = false;
    private float stepInterval; // Interval between steps in seconds
    private float stepTimer;

    Vector3 velocity;
    bool isGrounded; 
    private float baseFOV;
    private float sprintFOVModifier = 1.25f;
    private Vector3 flashlightParentOrigin;
    private float movementCounter;
    private float idleCounter;
    private Vector3 targetFlashlightBobPosition;
    // Start is called before the first frame update

    private void Start()
    {
        baseFOV = normalCam.fieldOfView;
        flashlightParentOrigin = flashlightParent.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;


        controller.Move(move * speed * Time.deltaTime);


        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool isSprinting = sprint && z > 0;

        float t_adjustedSpeed = speed;
        if (isSprinting)
        {
            t_adjustedSpeed *= sprintModifier;
            controller.Move(move * t_adjustedSpeed * Time.deltaTime);
        }

        if(isSprinting)
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 8f);
            stepInterval = 0.4f;
        }
        else
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.deltaTime * 8f);
            stepInterval = 0.7f;
        }

        if(move.x == 0 && move.y == 0)
        {
            isMoving = false;
            HeadBob(idleCounter, 0.0002f, 0.0002f);
            idleCounter += Time.deltaTime;
            flashlightParent.localPosition = Vector3.Lerp(flashlightParent.localPosition, targetFlashlightBobPosition, Time.deltaTime * 2f);

        }
        else if(!isSprinting)
        {
            isMoving = true;
            HeadBob(movementCounter, 0.00035f, 0.00035f);
            movementCounter += Time.deltaTime * 4.5f;
            flashlightParent.localPosition = Vector3.Lerp(flashlightParent.localPosition, targetFlashlightBobPosition, Time.deltaTime * 6f);

        }
        else
        {
            HeadBob(movementCounter, 0.0006f, 0.0006f);
            movementCounter += Time.deltaTime * 7f;
            flashlightParent.localPosition = Vector3.Lerp(flashlightParent.localPosition, targetFlashlightBobPosition, Time.deltaTime * 10f);

        }
        HandleFootsteps();

    }
    void HeadBob(float p_z, float p_x_intensity, float p_y_intensity)
    {
        targetFlashlightBobPosition = flashlightParentOrigin += new Vector3(Mathf.Cos(p_z) * p_x_intensity, Mathf.Sin(p_z * 2) * p_y_intensity, 0);
    }

    void HandleFootsteps()
    {
        if (isMoving && !WalkSound.isPlaying && stepTimer <= 0f)
        {
            WalkSound.Play();
            stepTimer = stepInterval;
        }
        else if (!isMoving)
        {
            WalkSound.Stop();
            stepTimer = 0f;
        }

        if (stepTimer > 0)
        {
            stepTimer -= Time.deltaTime;
        }
    }

}
