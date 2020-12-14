using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WheelJoint2D))]
public class LogBehaviour : MonoBehaviour
{
    [Header("Rotation options")]
    [SerializeField] private RotationElement[] rotationPattern;

    private WheelJoint2D wheelJoint;
    private JointMotor2D motor;

    private void Awake()
    {
        wheelJoint = GetComponent<WheelJoint2D>();
        motor = new JointMotor2D();
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        var rotationIndex = 0;

        while(true)
        {
            yield return new WaitForFixedUpdate();

            motor.motorSpeed = rotationPattern[rotationIndex].Speed;
            motor.maxMotorTorque = 10000;
            wheelJoint.motor = motor;

            yield return new WaitForSecondsRealtime(rotationPattern[rotationIndex].Duration);
            rotationIndex++;
            
            if (rotationIndex >= rotationPattern.Length)
                rotationIndex = 0;
        }
    }
}

[System.Serializable]
public class RotationElement
{
    public float Speed;
    public float Duration;
}