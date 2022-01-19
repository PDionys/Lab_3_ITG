using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitConfig : MonoBehaviour
{
    public float maxFOV = 180;
    public float maxAcceleration;
    public float maxVelocity;

    //Wander Variables
    public float wanderJitter;
    public float wanderRadius;
    public float wanderDistance;
    public float wanderPriority;

    //Avoidance Variable
    public float avoidanceRadius;
    public float avoidancePriority;
}
