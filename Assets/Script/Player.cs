using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [System.Serializable]
    public enum PlayerState
    {
        Grounded, Ragdoll, Falling, Jumping, Diving, DiveToGround
    }

    public PlayerState mState = PlayerState.Grounded;
    public Transform transformCam;

    [Header("Movement Speed")]
    public float Speed = 5f;
    public float diveSpeed = 3f;
    public float jumpSpeed = 5f;
    public float gravity = 15;
    public float airControl = 0.4f;
    public float turnSpeed = 1500f;

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode diveKey = KeyCode.C;
    public LayerMask mask;


    #region Private Variables
    private Rigidbody playerRigid;
    float inputH, inputV;
    float jumpCounter;
    CapsuleCollider capsuleCollider;
    private bool isGrounded;
    private Animator anim;
    float currentYposition;
    Vector3 recentMoveVelocity;
    bool jumpInput, diveInput, inDive, inJump;
    float diveCounter;
    RaycastHit hit;
    float groundDistance;
    Vector3 momentum;
    RagdollSystem Ragdoll;
    Vector3 moveDirection;
    #endregion

    private void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        //anim = GetComponent<Animator>();
        Ragdoll = GetComponent<RagdollSystem>();

    }

    private void Update()
    {
        InputHandle();
        CheckGround();
        //AnimatorController();
        //inputH = Input.GetAxisRaw("Horizontal");
        //inputV = Input.GetAxisRaw("Vertical");
    }

    private void AnimatorController()
    {
        Vector3 _velocity = moveDirection;
        _velocity = Vector3.Lerp(recentMoveVelocity, _velocity, 0.8f);
        recentMoveVelocity = _velocity;
        anim.SetFloat("HorizontalSpeed", _velocity.magnitude);
        anim.SetBool("inDive", inDive);
        anim.SetBool("isGrounded", mState == PlayerState.Grounded);
    }
    float radius = 0.3f;


    private void CheckGround()
    {
        groundDistance = mState == PlayerState.Grounded ? 1.4f : 0.95f;
        isGrounded = (Physics.SphereCast(transform.position + transform.up,
            radius, -transform.up, out hit, groundDistance, mask, QueryTriggerInteraction.Ignore));
    }

    private void FixedUpdate()
    {
        if (mState == PlayerState.Ragdoll)
        {
            currentYposition = transform.eulerAngles.y;
            momentum = Vector3.zero;
            return;
        }
        HandleState();

        //Vector3 velocity = Time.deltaTime * Speed * InputDirection();
        //playerRigid.MovePosition(transform.position + velocity);
    }

    void HandleAction()
    {
        if (mState == PlayerState.Grounded)
        {
            if (jumpInput && !inJump)
            {
                mState = PlayerState.Jumping;
                jumpCounter = 0.2f;
                inJump = true;
                //anim.CrossFadeInFixedTime("Jump", 0.01f);

            }
        }
        if (mState != PlayerState.Diving)
        {
            if (diveInput && !inDive)
            {
                diveCounter = 0.2f;
                inDive = true;
                //anim.CrossFadeInFixedTime("Dive", 0.01f);
                mState = PlayerState.Diving;
            }
        }
    }

    private void HandleState()
    {
        HandleAction();
        switch (mState)
        {
            case PlayerState.Grounded:
                Vector3 velocity = Time.deltaTime * Speed * InputDirection();
                playerRigid.MovePosition(transform.position + velocity);
                momentum = velocity;
                if (!isGrounded)
                {
                    mState = PlayerState.Falling;
                }
                if (inDive)
                {
                    //play sound later
                }
                inJump = false;
                inDive = false;
                break;
            case PlayerState.Falling:
                momentum = Vector3.down * gravity;
                playerRigid.AddForce(momentum);
                if (isGrounded)
                    mState = PlayerState.Grounded;
                break;
            case PlayerState.Jumping:
                jumpCounter -= Time.deltaTime;
                if (jumpCounter <= 0)
                {
                    jumpCounter = 0;
                    mState = PlayerState.Falling;
                }
                Vector3 vel = momentum;
                vel.y = jumpSpeed;
                playerRigid.velocity = vel + moveDirection * Speed * airControl;
                break;
            case PlayerState.Diving:
                diveCounter -= Time.deltaTime;
                if (diveCounter <= 0)
                {
                    diveCounter = 0;
                    mState = PlayerState.Falling;
                }
                Vector3 _vel;
                if (inJump)
                    _vel = (transform.forward) * diveSpeed;
                else
                    _vel = (transform.up + transform.forward) * diveSpeed;
                playerRigid.velocity += _vel * Time.deltaTime * 10f;
                break;
        }
    }

    private void InputHandle()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");
        jumpInput = Input.GetKey(jumpKey);
        diveInput = Input.GetKeyDown(diveKey);
        moveDirection = InputDirection();
    }

    private void LateUpdate()
    {
        if (!inDive && mState != PlayerState.Ragdoll)
            Rotation();
    }

    Vector3 InputDirection()
    {
        Vector3 _direction = Vector3.zero;
        _direction += transformCam.forward * inputV;
        _direction += transformCam.right * inputH;
        if (_direction.magnitude > 1f)
            _direction.Normalize();
        _direction.y = 0f;

        return _direction;
    }

    private void Rotation()
    {
        Vector3 moveDirection = InputDirection();
        if (moveDirection.magnitude >= 0.001f)
        {
            Vector3 _currentFoward = transform.forward;
            float _angle = Vector3.Angle(_currentFoward, moveDirection);
            float _sign = Mathf.Sign(Vector3.Dot(transform.up, Vector3.Cross(_currentFoward, moveDirection)));
            float _difference = _angle * _sign;
            float _factor = Mathf.InverseLerp(0f, 90f, Mathf.Abs(_difference));
            float _step = Mathf.Sign(_difference) * _factor * Time.deltaTime * turnSpeed;
            if (_difference < 0f && _step < _difference)
                _step = _difference;
            else if (_difference > 0f && _step > _difference)
                _step = _difference;
            currentYposition += _step;
            if (currentYposition > 360f)
                currentYposition -= 360f;
            if (currentYposition < -360f)
                currentYposition += 360f;
        }
        transform.rotation = Quaternion.Euler(0f, currentYposition, 0f);
    }
}
