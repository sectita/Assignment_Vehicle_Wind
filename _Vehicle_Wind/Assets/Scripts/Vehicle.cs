using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vehicle : MonoBehaviour
{
    [SerializeField] private WheelCollider[] wheelCollider = new WheelCollider[4];
    [SerializeField] private Transform[] wheelTransform = new Transform[4];

    [SerializeField] private float maxTorque = 50f;
    [SerializeField] private float steerAngle = 40f;
    [SerializeField] private float breakTorque = 70f;
    [SerializeField] private float turnTorque;
    [SerializeField] private float turnTorqueMultiplier = 0.5f;



    private const string VerticalMove = "Vertical";
    private const string HorizontalMove = "Horizontal";

    float VerticalInput;
    float HorizontalInput;
    private float lRpm;
    private float rRpm;

    Vector3 flipDrag;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = flipDrag.normalized;
    }

    private void Update()
    {
        UpdateWheelPosition();
        lRpm = turnTorque;
        rRpm = turnTorque;

    }

    private void FixedUpdate()
    {
        InputsVehicle();
    }
    void InputsVehicle()
    {
        VerticalInput = Input.GetAxis(VerticalMove);
        HorizontalInput = Input.GetAxis(HorizontalMove);

        float floatAngle = HorizontalInput * steerAngle;
        wheelCollider[0].steerAngle = floatAngle;
        wheelCollider[1].steerAngle = floatAngle;

        if (HorizontalInput < 0 && wheelCollider[0].rpm < 0)
        {
            rRpm *= turnTorqueMultiplier;
            wheelCollider[1].motorTorque = rRpm;
        }
        else if (HorizontalInput > 0 && wheelCollider[0].rpm > 0)
        {
            lRpm *= turnTorqueMultiplier;
            wheelCollider[0].motorTorque = rRpm;
        }


        for (int i = 0; i < 4; i++)
        {
            wheelCollider[i].motorTorque = VerticalInput * maxTorque;
            if (VerticalInput == 0)
            {
                wheelCollider[i].brakeTorque = breakTorque;
            }
            else
            {
                wheelCollider[i].brakeTorque = 0;
            }
        }
    }

    void UpdateWheelPosition()
    {
        for (int i = 0; i < 4; i++)
        {
            Quaternion q;
            Vector3 r;
            wheelCollider[i].GetWorldPose(out r, out q);
            wheelTransform[i].position = r;
            wheelTransform[i].rotation = q;

        }
    }
}
