using UnityEngine;
using UnityEngine.InputSystem;

public class Suspension : MonoBehaviour
{
    public GameObject playerCar;
    public Rigidbody car;
    public Transform frontRightTire, frontLeftTire, backRightTire, backLeftTire;
    public Ray rayFRT, rayFLT, rayBRT, rayBLT;
    public float suspensionForce = 1f;
    public float suspensionLength = 1f;
    public float acceleration = 10f;
    public float turnSpeed = 2f;


    public PlayerInput playerInput;
    public InputActions inputActions;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        inputActions = new InputActions();
        inputActions.VehicleMovement.Enable();
        inputActions.VehicleMovement.Forward.performed += Accelerate_Performed;
    }

    private void Accelerate_Performed(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        //Vector2 inputVector = context.ReadValue<Vector2>();
        //car.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * acceleration, ForceMode.Force);
    }

    private void Update()
    {
        suspend();
        Vector2 inputVector = inputActions.VehicleMovement.Forward.ReadValue<Vector2>();
        car.AddForce(transform.forward * inputVector.y * acceleration, ForceMode.Force);
        //car.AddForce(new Vector3(inputVector.x * 0, 0, inputVector.y) * acceleration, ForceMode.Acceleration);
        playerCar.transform.Rotate(new Vector3(0, inputVector.x*turnSpeed, 0));


    }

    public void suspend()
    {
        rayFRT = new Ray(frontRightTire.transform.position, -transform.up);
        rayFLT = new Ray(frontLeftTire.transform.position, -transform.up);
        rayBRT = new Ray(backRightTire.transform.position, -transform.up);
        rayBLT = new Ray(backLeftTire.transform.position, -transform.up);
        if (Physics.Raycast(rayFRT, out RaycastHit hitFR, suspensionLength))
        {
            car.AddForce(frontRightTire.transform.up * suspensionForce);
        }
        if (Physics.Raycast(rayFLT, out RaycastHit hitFL, suspensionLength))
        {
            car.AddForce(frontLeftTire.transform.up * suspensionForce);
        }
        if (Physics.Raycast(rayBRT, out RaycastHit hitBR, suspensionLength))
        {
            car.AddForce(backRightTire.transform.up * suspensionForce);
        }
        if (Physics.Raycast(rayBLT, out RaycastHit hitBL, suspensionLength))
        {
            car.AddForce(backLeftTire.transform.up * suspensionForce);
        }

        //Legacy bad versioon booooooo!!
        //if (car.transform.position.y <= suspensionLength)
        //{
        //    car.AddForce(frontRightTire.transform.up * suspensionForce);
        //    car.AddForce(frontLeftTire.transform.up * suspensionForce);
        //    car.AddForce(backRightTire.transform.up * suspensionForce);
        //    car.AddForce(backLeftTire.transform.up * suspensionForce);
        //}
    }

    public void drive(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();


        car.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * acceleration, ForceMode.Force);

        //Debug.Log("forward!!" + context.phase);

    }
}
