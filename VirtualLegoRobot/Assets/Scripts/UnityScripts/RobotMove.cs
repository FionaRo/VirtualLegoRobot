using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMove : MonoBehaviour
{

    public float Speed;
    public WheelCollider Right;
    public WheelCollider Left;
    public WheelCollider Right1;
    public WheelCollider Left1;
    public float Steer;

    // Use this for initialization
    void Start()
    {
        Right.motorTorque = Speed;
        Left.motorTorque = Speed;
        Right1.steerAngle = 90;
        Left1.steerAngle = 90;
        //Right.steerAngle = -90;
        Left.steerAngle = 90;
    }

    // Update is called once per frame
    void Update()
    {
        //float v = Input.GetAxis("Vertical") * Speed;
        //Right.motorTorque = v;
        //Left.motorTorque = v;
        //float h = Input.GetAxis("Horizontal") * Steer;
        //Right1.steerAngle = h;
        //Left1.steerAngle = h;
    }
}
