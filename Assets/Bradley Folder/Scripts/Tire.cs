using UnityEngine;
using UnityEngine.InputSystem;

public class Tire : MonoBehaviour
{
    [Header("Extras")]
    public GameObject car;
    public Rigidbody carRB;
    public Transform tireTrans;
    public Ray tireRay;
    public InputActions inputActions;
    public PlayerInput playerInput;
    public bool isLeftTire = false;



    [Header("Suspension")]
    //Suspension
    //force = offset x strength
    public float suspensionStrength = 1f;
    private float suspensionLength = 1f;
    //dampening
    // force = -(velocity x damping)
    private float suspensionVelocity;
    public float damper = 1f;
    //full spring calc
    // force = (offset x strength) - (velocity x damping)
    // Start is called before the first frame update

    [Header("Steering")]
    //acceleration = deltaV/time
    //public float slideCounter;
    //public AnimationCurve tireGrip;
    public float tireGrip = 1f;
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
    public float a;

    private void Awake()
    {
        tireTrans = this.GetComponent<Transform>();
        playerInput = GetComponent<PlayerInput>();
        inputActions = new InputActions();
        inputActions.VehicleMovement.Enable();

    }
    void Update()
    {
        tireTrans.localRotation = Quaternion.Euler(tireTrans.localRotation.x, tireTrans.localRotation.y + tireAngle, tireTrans.localRotation.z);
    }

    private void FixedUpdate()
    {
        Suspend();
        Steer();
        Vector2 inputVector = inputActions.VehicleMovement.Forward.ReadValue<Vector2>();
        carRB.AddForceAtPosition(tireTrans.forward * inputVector.y * a, tireTrans.position);


    }

    public void Suspend()
    {
        tireRay = new Ray(tireTrans.position, -transform.up);




        if (Physics.Raycast(tireRay, out RaycastHit hitTire))
        {
            Vector3 springDirection = tireTrans.up;
            Vector3 tireVelocity = carRB.GetPointVelocity(tireTrans.position);
            float suspensionOffset = suspensionLength - hitTire.distance;

            float velocity = Vector3.Dot(springDirection, tireVelocity);

            float force = (suspensionOffset * suspensionStrength) - (velocity * damper);
            carRB.AddForceAtPosition(springDirection * force, tireTrans.position);
        }
    }

    public void Steer()
    {
        tireRay = new Ray(tireTrans.position, -transform.up);
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


        if (Physics.Raycast(tireRay, out RaycastHit hitTire))
        {
            Vector3 steerDirection = tireTrans.right;
            Vector3 tireVelocity = carRB.GetPointVelocity(tireTrans.position);

            float velocity = Vector3.Dot(steerDirection, tireVelocity);

            float deltaV = -velocity * tireGrip;

            float acceleration = deltaV / Time.fixedDeltaTime;
            carRB.AddForceAtPosition(steerDirection * tireMass * acceleration, tireTrans.position);
        }
    }

    //utter nonsense
    //public void Motor()
    //{
    //    if (givePower)
    //    {
    //        Vector3 accelerationDirection = tireTrans.forward;
    //        Vector2 inputVector = inputActions.VehicleMovement.Forward.ReadValue<Vector2>();

    //        //if (inputVector.y > 0)
    //        //{
    //        //    float vehicleSpeed = Vector3.Dot(car.transform.forward, carRB.velocity);

    //        //    float normSpeed = Mathf.Clamp01(Mathf.Abs(vehicleSpeed) * inputVector.y)*100;

    //        //    float torque = speedCurve.Evaluate(normSpeed) * inputVector.y;

    //        //    carRB.AddForceAtPosition(accelerationDirection * torque, tireTrans.position);
    //        //}

    //        carRB.AddForceAtPosition(accelerationDirection * , tireTrans.position);
    //    }

    //}
}
