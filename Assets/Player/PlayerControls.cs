using Runic;
using Runic.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    //Components & Systems
    Animator characterAnimator;
    WeaponObject characterWeapon;
    Rigidbody characterRigid;
    CharacterController characterController;
    //Control Bits
    public Transform playerRoot;
    public bool isSprinting = false;
    Vector2 look;
    float rotationPower = 0.5f;
    float moveSpeed = 6f;
    Vector3 lastPosition;
    float speed;
    Vector2 move;
    bool isGrounded = true;
    //States
    public bool isInteracting = false;
    public bool isWalking = false;
    public bool canAct = true;
    public bool canCamControl = true;
    //Objects
    public GameObject followTarget;
    public GameObject equippedWeapon;


    void Awake()
    {
        characterAnimator = GetComponent<Animator>();
        characterRigid = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();

        if (GetComponentInChildren<WeaponObject>()) //This needs to be replaced
        {
            equippedWeapon = GetComponentInChildren<WeaponObject>().gameObject;
            characterWeapon = equippedWeapon.GetComponent<WeaponObject>();
        }
    }


    void Update()
    {
        characterAnimator.SetFloat("Speed_f", GetSpeed());
        Look(look);
        Move(move);
        Interact();
    }

    private void Interact()
    {
        if (GetComponent<PlayerInput>().actions.FindAction("Interact").triggered)
        {
            isInteracting = true;
        }
        else
        {
            isInteracting = false;
        }
    }

    private void Move(Vector2 direction)
    {
        if (isWalking)
        {
            direction /= 2;
        }
        if (direction.sqrMagnitude < 0.01)
        {
            return;
        }

        Vector3 MoveDirection = (new Vector3(direction.x, 0, direction.y));
        float scaledMoveSpeed = moveSpeed * Time.deltaTime;
        Vector3 heading = MoveDirection * scaledMoveSpeed;
        float angle = Mathf.Atan2(heading.x, heading.z) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

        #region Character Controller Based
        if (isSprinting)
        {
            characterController.Move(heading);

        }
        else
        {
            characterController.Move(heading);
        }
        #endregion
        #region Physics Based
        //if (isSprinting)
        //{
        //    characterRigid.velocity = new Vector3(MoveDirection.x,
        //        characterRigid.velocity.y, MoveDirection.z) * 4;
        //}
        //else
        //{
        //    characterRigid.velocity = new Vector3(MoveDirection.x,
        //        characterRigid.velocity.y, MoveDirection.z) * 2;
        //}
        #endregion


    }

    private void Look(Vector2 direction)
    {
        if (!canCamControl)
        {
            return;
        }
        if (direction == Vector2.zero)
        {
            return;
        }
        followTarget.transform.rotation *= Quaternion.AngleAxis(direction.x * rotationPower, Vector3.up);
        followTarget.transform.rotation *= Quaternion.AngleAxis(direction.y * rotationPower, Vector3.right);

        var angles = followTarget.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTarget.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }
        followTarget.transform.localEulerAngles = angles;
    }

    public void Interact(InteractionTypes interaction) 
    {
        switch (interaction)
        {
            case InteractionTypes.Chest:
                StartCoroutine(ChestInteractAnimation());
                break;
            case InteractionTypes.Person:
                StartCoroutine(PlayerInteractAnimation());
                break;
            default:
                break;
        }
    }

    #region Interactions
    IEnumerator ChestInteractAnimation() 
    {
        characterAnimator.SetInteger("Interaction", 1);
        characterAnimator.SetBool("Interacting", true);
        yield return new WaitForSeconds(2.7f);
        characterAnimator.SetInteger("Interaction", 0);
        characterAnimator.SetBool("Interacting", false);
    }

    IEnumerator PlayerInteractAnimation()
    {
        characterAnimator.SetInteger("Interaction", 2);
        characterAnimator.SetBool("Interacting", true);
        yield return new WaitForSeconds(1.75f);
        characterAnimator.SetInteger("Interaction", 0);
        characterAnimator.SetBool("Interacting", false);
    }
    #endregion

    #region InputEvents
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!isGrounded)
        {
            return;
        }
        StartCoroutine(jump());
    }
    IEnumerator jump()
    {
        characterAnimator.SetBool("Jump_b", true);
        yield return new WaitForSeconds(0.2f);
        characterAnimator.SetBool("Jump_b", false);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (canAct && equippedWeapon != null)
        {
            characterWeapon.Attack();
        }
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isInteracting = true;
        }
        else
        {
            isInteracting = false;
        }
    }

    public void OnSprintToggle(InputAction.CallbackContext context) 
    {
        if (!context.performed)
        {
            return;
        }
        isSprinting = !isSprinting;
    }

    public void OnWalkToggle(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        isWalking = !isWalking;
    }
    #endregion

    #region Misc
    public float GetSpeed()
    {
        speed = Mathf.Lerp(speed, (transform.position - lastPosition).magnitude / Time.deltaTime, 0.75f);
        lastPosition = transform.position;
        return speed;
    }

    #endregion

    #region Cross-System
        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.GetComponent<MeshRenderer>().enabled)
            {
                collision.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }

    #endregion


}
