using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwimmingScript : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float swimForce = 2f;
    [SerializeField] float dragForce = 1f;
    [SerializeField] float minForce;
    [SerializeField] float swimCooldown;

    [SerializeField] InputActionReference leftControllerSwimRefernece;
    [SerializeField] InputActionReference leftControllerVelocity;
    [SerializeField] InputActionReference rightControllerSwimRefernece;
    [SerializeField] InputActionReference rightControllerVelocity;
    [SerializeField] Transform forwardReference;
    [SerializeField] TextMeshProUGUI left;
    [SerializeField] TextMeshProUGUI left1;
    [SerializeField] TextMeshProUGUI right;
    [SerializeField] TextMeshProUGUI right1;

    [SerializeField] Rigidbody _rigidbody;
    private bool isSwiming = false;
    float _cooldownTimer;

    private void Start()
    {
    }
    private void FixedUpdate()
    {
        
        if (isSwiming)
        {
            if (leftControllerSwimRefernece.action.IsPressed()
                && rightControllerSwimRefernece.action.IsPressed())
            {
                //to sa dane z lewego kontrollera o warto?ci 0-1 zaleznie od predkosci ruchu
                var leftHandVelocity = leftControllerVelocity.action.ReadValue<Vector3>();
                //to sa dane z prawego kontrollera o warto?ci 0-1 zaleznie od predkosci ruchu
                var rightHandVelocity = rightControllerVelocity.action.ReadValue<Vector3>();
                Vector3 velocityToAdd = leftHandVelocity + rightHandVelocity;
                velocityToAdd *= -1;
                if (velocityToAdd.sqrMagnitude > minForce * minForce)
                {
                    Vector3 worldVelocity = forwardReference.TransformDirection(velocityToAdd);
                    _rigidbody.AddForce(worldVelocity * swimForce, ForceMode.Acceleration);

                }
                if (_rigidbody.velocity.sqrMagnitude > 0.01f)
                {
                    _rigidbody.AddForce(-_rigidbody.velocity * dragForce, ForceMode.Acceleration);
                }
            }
        }
    }
    public void StartSwimming()
    {
        isSwiming=true;
        _rigidbody.useGravity = false;
    }
    public void StopSwimming()
    {
        isSwiming = false;
        _rigidbody.useGravity = true;
    }
}
