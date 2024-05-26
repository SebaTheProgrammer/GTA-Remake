using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float m_HorizontalInput, m_VerticalInput;
    private float m_CurrentSteerAngle, m_CurrentbreakForce;
    private bool m_IsBreaking;

    [Header("Settings")]
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;

    [Header("Colliders")]
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    [Header("Wheels")]
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    [SerializeField] private float m_TopSpeed;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    public void OutOfCar()
    {
        frontRightWheelCollider.brakeTorque = breakForce;
        frontLeftWheelCollider.brakeTorque = breakForce;
        rearLeftWheelCollider.brakeTorque = breakForce;
        rearRightWheelCollider.brakeTorque = breakForce;
    }

    private void GetInput()
    {
        // Steering Input
        m_HorizontalInput = Input.GetAxis("Horizontal");

        // Acceleration Input
        m_VerticalInput = Input.GetAxis("Vertical");

        // Breaking Input
        m_IsBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = m_VerticalInput * motorForce;
        frontRightWheelCollider.motorTorque = m_VerticalInput * motorForce;
        m_CurrentbreakForce = m_IsBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = m_CurrentbreakForce;
        frontLeftWheelCollider.brakeTorque = m_CurrentbreakForce;
        rearLeftWheelCollider.brakeTorque = m_CurrentbreakForce;
        rearRightWheelCollider.brakeTorque = m_CurrentbreakForce;
    }

    private void HandleSteering()
    {
        m_CurrentSteerAngle = maxSteerAngle * m_HorizontalInput;
        frontLeftWheelCollider.steerAngle = m_CurrentSteerAngle;
        frontRightWheelCollider.steerAngle = m_CurrentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    //testing with rays
    private void TestRay(Transform wheelTransform, Rigidbody carRigidBody)
    {
        ////up and down
        //float suspensionRestDist = 2.0f;
        //float springStrength = 20f;
        //float springDamper = 2;
        //Ray tireRay;

        //RaycastHit hit;

        //bool rayDidHit = false;

        //if (rayDidHit)
        //{
        //    Vector3 springDir = wheelTransform.up;
        //    Vector3 tireWorldVel = carRigidBody.GetPointVelocity(wheelTransform.position);

        //    float offset = suspensionRestDist - tireRay.distance;

        //    float vel = Vector3.Dot(springDir, tireWorldVel);

        //    float force = (offset * springStrength) - (vel * springDamper);

        //    carRigidBody.AddForce(springDir * force, (wheelTransform.GetPositionAndRotation()));

        //}

        ////steering
        //float tireGripFactor = 12;
        //float tireMass = 100;
        //if (rayDidHit)
        //{
        //    Vector3 steeringDir = wheelTransform.right;
        //    Vector3 tireWorldVel = carRigidBody.GetPointVelocity(wheelTransform.position);

        //    float steeringVel = Vector3.Dot(steeringDir, tireWorldVel);

        //    float desiredVelChange = -steeringVel * tireGripFactor;

        //    float desiredAccel = desiredVelChange / Time.fixedDeltaTime;

        //    carRigidBody.AddForce(steeringDir * tireMass * desiredAccel, (wheelTransform.GetPositionAndRotation()));

        //}
        //float accelInput = 1; //graph
        ////acceleration and breaking
        //if (rayDidHit)
        //{
        //    Vector3 accelDir = wheelTransform.forward;

        //    if (accelInput > 0.0f)
        //    {
        //        float carSpeed = Vector3.Dot(carTransform.forward, carRigidBody.velocity);

        //        float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / carTopSpeed);
        //    }

        //}
    }
}
