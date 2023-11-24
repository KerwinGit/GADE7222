using UnityEngine;
using UnityEngine.InputSystem;

public class Tire : MonoBehaviour
{
    //appologies for spelling tyres as tires and then also sometimes tyres, all the code is probably tires tho

    [Header("Extras")]
    public GameObject car;
    public Rigidbody carRB;
    public Transform tireTrans;
    public Ray tireRay;
    public InputActions inputActions;
    public PlayerInput playerInput;
    public bool isLeftTire = false;
    public LayerMask layerToIgnore;


    public float carMaxVelocity = 30f;
    public float carVelocityPercentage;

    [Header("Suspension")]
    //Suspension
    //force = offset x strength
    public float suspensionStrength = 1f;
    private float suspensionLength = 1f;

    //dampening
    // force = -(velocity x damping)
    public float damper = 1f;

    // force = (offset x strength) - (velocity x damping)


    [Header("Steering + Traction")]
    //acceleration = deltaV/time
    //public float slideCounter;
    public AnimationCurve tireGrip;
    //public float tireGrip = 1f;

    public float tireMass = 1f;
    //using ackerman angles
    private float turnAngleLeft;
    private float turnAngleRight;
    private float tireAngle;
    public float wheelBase;
    public float rearTrack;
    public float turnRad;


    [Header("Acceleration")]
    //public AnimationCurve speedCurve;
    public bool givePower = false;
    public float accel = 200;
    public AnimationCurve a;

    private void Awake()
    {
        tireTrans = this.GetComponent<Transform>();
        playerInput = GetComponent<PlayerInput>();
        inputActions = new InputActions();
        inputActions.VehicleMovement.Enable();


    }
    void Update()
    {
        //handles rotating the tyres
        tireTrans.localRotation = Quaternion.Euler(tireTrans.localRotation.x, tireTrans.localRotation.y + tireAngle, tireTrans.localRotation.z);
        carVelocityPercentage = carRB.velocity.magnitude / carMaxVelocity;
    }

    private void FixedUpdate()
    {
        Suspend();
        Steer();
        Vector2 inputVector = inputActions.VehicleMovement.Forward.ReadValue<Vector2>();
        carRB.AddForceAtPosition(tireTrans.forward * inputVector.y * accel * a.Evaluate(carVelocityPercentage), tireTrans.position);


    }

    public void Suspend()
    {
        tireRay = new Ray(tireTrans.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(tireRay, out hit, 4))
        {
            Vector3 tireVelocity = carRB.GetPointVelocity(tireTrans.position);

            float suspensionOffset = suspensionLength - hit.distance;
            float velocity = Vector3.Dot(tireTrans.up, tireVelocity);

            float force = (suspensionOffset * suspensionStrength) - (velocity * damper);
            carRB.AddForceAtPosition(tireTrans.up * force, tireTrans.position);

        }
    }

    public void Steer()
    {
        tireRay = new Ray(tireTrans.position, -transform.up);
        RaycastHit hit;
        Vector2 inputVector = inputActions.VehicleMovement.Forward.ReadValue<Vector2>();
        if (givePower)
        {
            if (inputVector.x > 0)
            {
                turnAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRad + (rearTrack / 2))) * inputVector.x;
                turnAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRad - (rearTrack / 2))) * inputVector.x;
            }
            else if (inputVector.x < 0)
            {
                turnAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRad - (rearTrack / 2))) * inputVector.x;
                turnAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRad + (rearTrack / 2))) * inputVector.x;
            }
            else
            {
                turnAngleLeft = 0;
                turnAngleRight = 0;
            }

            if (isLeftTire)
            {
                tireAngle = turnAngleLeft;
            }
            else
            {
                tireAngle = turnAngleRight;
            }
        }


        if (Physics.Raycast(tireRay, out hit, 4, ~layerToIgnore))
        {
            Vector3 steerDirection = tireTrans.right;
            Vector3 tireVelocity = carRB.GetPointVelocity(tireTrans.position);

            float velocity = Vector3.Dot(steerDirection, tireVelocity);

            float deltaV = -velocity * tireGrip.Evaluate(carVelocityPercentage); //change in velocity

            float acceleration = deltaV / Time.fixedDeltaTime;
            carRB.AddForceAtPosition(steerDirection * tireMass * acceleration, tireTrans.position);
        }
    }

    public float getVelocityPercentage()
    {
        return carVelocityPercentage;
    }






}
