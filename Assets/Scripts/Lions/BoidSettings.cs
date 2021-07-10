using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSettings : MonoBehaviour
{
    public float radius = 1;

    public float separationWeight = 1;
    public float alignmentWeight = 1;
    public float structureWeight = 1; // center does not work
    public float boundaryWeight = 1;
    public float avoidTargetWeight = 1;

    public float speed = 1;
    public float rotationSpeed = 10;


    public float distUntilAttack = 4;
    public float acceleration = 1;
}
