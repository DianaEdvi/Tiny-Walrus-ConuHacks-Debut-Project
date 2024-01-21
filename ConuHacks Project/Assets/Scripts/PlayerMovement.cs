using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;
    public float chargeSpeed = 5f;
    public float maxChargeTime = 3f;
    public float shootForce = 10f;
    private float currentChargeTime = 0f;
    private Rigidbody rb;
    private bool isRightClicking = false;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;

    [SerializeField] AudioSource shootSound;
    [SerializeField] AudioSource chargeSound;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 lookDirection = playerCamera.transform.forward;
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);


        // controls the shooting forward 
        if (!isRightClicking && currentChargeTime > 0 ){
            Vector3 appliedForce = lookDirection * shootForce;
            if(characterController.enabled){
            characterController.Move(appliedForce * Time.deltaTime);
            currentChargeTime -= 0.01f;
            }
            
        }

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftControl) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;

        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 6f;
            runSpeed = 12f;
        }

        if(characterController.enabled){
        characterController.Move(moveDirection * Time.deltaTime);
        }

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }    

        // Check if the right mouse button is being held down
        if (Input.GetMouseButton(1))
        {
            // Right-click is held down
            OnRightClickHold();
        }

        // Check if the right mouse button is released
        if (Input.GetMouseButtonUp(1))
        {
            // Right-click is released
            OnRightClickRelease();
        }
    }
    //end of update function

    void OnRightClickHold()
    {
        // Your code while right-click is held down
        Debug.Log("Right Click Held");
        isRightClicking = true;
        Charge();
    }

    void OnRightClickRelease()
    {
        // Your code when right-click is released
        Debug.Log("Right Click Released");
        isRightClicking = false;
        Shoot();
    }

// ChatGPT used to generate the charge time part of the code 
    void Charge()
    {
        if (currentChargeTime < maxChargeTime)
    {
        float previousChargeTime = currentChargeTime;

        currentChargeTime += Time.deltaTime;

        // Check if the charge time increased for the first time
        if (previousChargeTime == 0)
        {
            // Play Charge Sound only once at the beginning of charging
            chargeSound.Play();
        }

        // Check if the charge sound is still playing from the previous charge
        if (!chargeSound.isPlaying)
        {
            // If not playing, start playing the charge sound
            chargeSound.Play();
        }

        Debug.Log("Charging: " + currentChargeTime);
    }
    }

    void Shoot()
    {
    // Shoot Sound plays
    shootSound.Play();
    // Calculate the shooting direction based on the player's forward direction
    //Vector3 shootDirection = transform.forward;

    Vector3 forward = transform.TransformDirection(Vector3.forward);

    // Apply force to shoot the player in the direction they are looking
    float forceMagnitude = Mathf.Lerp(0f, shootForce, currentChargeTime / maxChargeTime);
    Debug.Log(forceMagnitude);

    Vector3 appliedForce = forward * shootForce;

    // Apply the force to the character controller
    
    if(characterController.enabled){
    characterController.Move(appliedForce * Time.deltaTime);
    }

    // Reset charge time for the next shot

    Debug.Log("Shoot!");
    }
}