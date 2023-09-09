using UnityEngine;
using UnityEngine.InputSystem;

public class Tire : MonoBehaviour
{
    //appologies for spelling tyres as tires and then also sometimes tyres, all the code is probably tires tho

    [Header("Extras")]
    public GameObject car; //car game object
    public Rigidbody carRB; //car rigidbody
    public Transform tireTrans; //tire transform
    public Ray tireRay; //raycast projected from tire location
    public InputActions inputActions;
    public PlayerInput playerInput;
    public bool isLeftTire = false;

    public float carMaxVelocity = 30f; //the car's max velocity
    public float carVelocityPercentage; //percentage of the max velocity the car is currently moving    

    [Header("Suspension")]
    //Suspension
    //force = offset x strength
    public float suspensionStrength = 1f;   //strength of the spring
    private float suspensionLength = 1f;    //length of the suspension
    //dampening
    // force = -(velocity x damping)
    private float suspensionVelocity;
    public float damper = 1f;   //force that acts against spring/suspension force to slow down movement and make smoother
    //full spring calc
    // force = (offset x strength) - (velocity x damping)
    // Start is called before the first frame update

    [Header("Steering + Traction")]
    //acceleration = deltaV/time
    //public float slideCounter;
    public AnimationCurve tireGrip;
    //public float tireGrip = 1f;

    public float tireMass = 1f;
    //using ackerman angles
    private float turnAngleLeft;    //angle of the left tyre
    private float turnAngleRight;   //angle of the right tyre
    private float tireAngle;        //max angle the tyre will turn
    public float wheelBase; //width of tire
    public float rearTrack; //distance from middle of vehicle to the tyre
    public float turnRad;   //turn radius


    [Header("Acceleration")]
    //public AnimationCurve speedCurve;
    public bool givePower = false;  //if the tire turns and moves car forward
    public float accel = 200;       //Acceleration
    public AnimationCurve a;        //the graph that determines how much power acceleration power is given to the tires based on vehicle speed

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
        carVelocityPercentage = carRB.velocity.magnitude/carMaxVelocity;
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
        
        if (Physics.Raycast(tireRay, out RaycastHit hitTire, 4))
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


        if (Physics.Raycast(tireRay, out RaycastHit hitTire, 4))
        {
            Vector3 steerDirection = tireTrans.right;
            Vector3 tireVelocity = carRB.GetPointVelocity(tireTrans.position);

            float velocity = Vector3.Dot(steerDirection, tireVelocity);

            float deltaV = -velocity * tireGrip.Evaluate(carVelocityPercentage);

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
