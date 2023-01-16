using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand : MonoBehaviour
{
    // Start is called before the first frame update
    //animation
    Animator animator;
    SkinnedMeshRenderer mesh;
    private float gripTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float triggerCurrent;
    public float animationSpeed;
    private string animatorGripParam = "Grip";
    private string animatorTriggerParam = "Trigger";
    public string nameOfHoldingObject;
    public bool isWeaponUnlocked = false;
    public bool isWeaponHidden = true;
    //physics movement
    [SerializeField] private ActionBasedController controller;
    [SerializeField] private InputActionReference shootingButton;
    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private Vector3 possitionOffset;
    [SerializeField] private Vector3 rotationOffset;
    [SerializeField] private Transform palm;
    [SerializeField] private float reachDistance = 0.01f, joinDistance = 0.05f;
    [SerializeField] private LayerMask grabbableLayer;
    [SerializeField] private LocomotionSystem locomotionSystem;
    [SerializeField] private GameObject playerRig;
    [SerializeField] private ClimbingInteractible climbingInteractible;
    [SerializeField] private GameObject weapon;
    [SerializeField] InputActionReference controllerVelocity;
    [SerializeField] float minForce;
    [SerializeField] float throwForce = 2;


    private Transform _followTarget;
    private Rigidbody _body;
    private bool _isGrabbing;
    private bool _isGrabbingClimber;
    private GameObject _heldObject;
    private Transform _grabPoint;
    private FixedJoint _joint1;
    private Rigidbody rb;
    private int grabbedObjectOryginalLayer;



    void Start()
    {
        rb = playerRig.GetComponent<Rigidbody>();
        //animation
        animator = GetComponent<Animator>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        //physics movement
        _followTarget = controller.gameObject.transform;
        _body = GetComponent<Rigidbody>();
        _body.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        _body.interpolation = RigidbodyInterpolation.Interpolate;
        _body.mass = 20f;
        _body.maxAngularVelocity = 20f;
        climbingInteractible = playerRig.GetComponent<ClimbingInteractible>();
        //input Setup
        controller.selectAction.action.started += Grab;
        controller.selectAction.action.canceled += Release;
        shootingButton.action.started += EquipWeapon;
        nameOfHoldingObject = string.Empty;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isWeaponHidden = !weapon.activeSelf;
        if (climbingInteractible.isClimbing())
        {
            rb.useGravity = false;
            locomotionSystem.GetComponent<ContinuousMoveProviderBase>().enabled = false;
        }
        AnimateHand();
        if (_isGrabbingClimber)
        {
            Climb();
        }
        else
        {
            PhysicsMove();
        }
    }


    private void Climb()
    {
        var positionWithOffset = _followTarget.TransformPoint(possitionOffset);
        var distance = Vector3.Distance(positionWithOffset, transform.position);
        var a = (positionWithOffset - transform.position).normalized * (0.5f * distance);
        rb.AddForce(-a);
    }

    private void PhysicsMove()
    {

        //position
        var positionWithOffset = _followTarget.TransformPoint(possitionOffset);
        var distance = Vector3.Distance(positionWithOffset, transform.position);
        _body.velocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);
        //rotation
        var rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(_body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        if (Mathf.Abs(axis.magnitude) != Mathf.Infinity)
        {
            if (angle > 180.0f) { angle -= 360.0f; }
            _body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);
        }
    }

    internal void SetGrip(float v)
    {
        gripTarget = v;
    }

    internal void SetTrigger(float v)
    {
        triggerTarget = v;
    }
    void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(animatorGripParam, gripCurrent);
        } if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(animatorTriggerParam, triggerCurrent);
        }
    }
    public void ToggleVisiblity()
    {
        mesh.enabled = !mesh.enabled;
    }
    private void EquipWeapon(InputAction.CallbackContext context)
    {
        if (!isWeaponUnlocked)
            isWeaponUnlocked = FindObjectOfType<Collectibles>().weapon;

        if (isWeaponUnlocked && !_isGrabbing)
        {
            bool isActive = !weapon.activeSelf;
            weapon.SetActive(isActive);
            isWeaponHidden = !isActive;
        }


    }
    private void Grab(InputAction.CallbackContext context)
    {
        if (!isWeaponHidden)
            return;
        if (_isGrabbing || _heldObject) return;
        Collider[] grabbableColliders = Physics.OverlapSphere(palm.position, reachDistance, grabbableLayer);
        if (grabbableColliders.Length < 1) return;
        var objectToGrab = grabbableColliders[0].transform.gameObject;
        var objectBody = objectToGrab.GetComponent<Rigidbody>();
        nameOfHoldingObject = objectToGrab.name;
        if (objectBody != null)
        {
            _heldObject = objectBody.gameObject;
            grabbedObjectOryginalLayer = _heldObject.layer;
            _heldObject.layer = 13;
        }
        else
        {
            objectBody = objectBody.GetComponentInParent<Rigidbody>();
            if (objectBody != null)
            {
                _heldObject = objectBody.gameObject;
            }
            else
            {
                return;
            }
        }
        if (nameOfHoldingObject == "Climber")
        {

            _isGrabbingClimber = true;
            rb.useGravity = false;
            climbingInteractible.setClimbing(gameObject.name);
        }
        if(_heldObject!=null)
        StartCoroutine(GrabObject(grabbableColliders[0], objectBody));
    }

    private IEnumerator GrabObject(Collider collider, Rigidbody targetBody)
    {
        if (_heldObject == null)
            yield break;
        _isGrabbing = true;
        //create grab point
        _grabPoint = new GameObject().transform;
        _grabPoint.position = collider.ClosestPoint(palm.position);
        _grabPoint.parent = _heldObject.transform;

        //move hand to grab point
        _followTarget = _grabPoint;
        //freez hand
        _body.velocity = Vector3.zero;
        _body.angularVelocity = Vector3.zero;
        targetBody.velocity = Vector3.zero;
        targetBody.angularVelocity = Vector3.zero;
        targetBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        targetBody.interpolation = RigidbodyInterpolation.Interpolate;
        //attach joint
        _joint1 = gameObject.AddComponent<FixedJoint>();
        _joint1.connectedBody = targetBody;
        _joint1.breakForce = float.PositiveInfinity;
        _joint1.breakTorque = float.PositiveInfinity;

        _joint1.connectedMassScale = 1;
        _joint1.massScale = 1;
        _joint1.enableCollision = false;
        _joint1.enablePreprocessing = false;

        _followTarget = controller.gameObject.transform;
        _heldObject.layer = grabbedObjectOryginalLayer;
    }
    private void Release(InputAction.CallbackContext context)
    {
        if (_isGrabbingClimber)
        {

            climbingInteractible.stopClimbing(gameObject.name);
            if (!climbingInteractible.isClimbing())
                locomotionSystem.GetComponent<ContinuousMoveProviderBase>().enabled = true;
            rb.useGravity = true;
            _isGrabbingClimber = false;
        }
        if (_joint1 != null)
            Destroy(_joint1);
        if (_grabPoint != null)
            Destroy(_grabPoint.gameObject);
        if (_heldObject != null)
        {
            var targetBody = _heldObject.GetComponent<Rigidbody>();
            targetBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            targetBody.interpolation = RigidbodyInterpolation.None;
            if (_heldObject.tag == "BowlingBall")
                ThrowBowlingBall(_heldObject);
            _heldObject = null;
        }
        _isGrabbing = false;
        if (controller)
            if (controller.gameObject.transform)
                _followTarget = controller.gameObject.transform;
    }
    public void ForceRelease()
    {
        if (_isGrabbingClimber)
        {
            climbingInteractible.stopClimbing(gameObject.name);
            if (!climbingInteractible.isClimbing())
            {
                locomotionSystem.GetComponent<ContinuousMoveProviderBase>().enabled = true;
                rb.useGravity = true;
            }
            _isGrabbingClimber = false;
        }
        if (_joint1 != null)
            Destroy(_joint1);
        if (_grabPoint != null)
            Destroy(_grabPoint.gameObject);
        if (_heldObject != null)
        {
            var targetBody = _heldObject.GetComponent<Rigidbody>();
            targetBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            targetBody.interpolation = RigidbodyInterpolation.None;
            _heldObject = null;
        }
        _isGrabbing = false;
        _isGrabbingClimber = false;
        _followTarget = controller.gameObject.transform;
    }
    private void ThrowBowlingBall(GameObject ball)
    {
        Rigidbody ballRB = ball.GetComponent<Rigidbody>();
        var HandVelocity = controllerVelocity.action.ReadValue<Vector3>();
        if (HandVelocity.sqrMagnitude > minForce * minForce)
        {
            Vector3 worldVelocity = gameObject.transform.TransformDirection(HandVelocity);
            ballRB.AddForce(worldVelocity * throwForce, ForceMode.Acceleration);
        }
        if (ballRB.velocity.sqrMagnitude > 0.01f)
        {
            ballRB.AddForce(ballRB.velocity * throwForce, ForceMode.Acceleration);
        }
    }
}
