using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSettings : MonoBehaviour
{
    public float radius = 1;
    public float separationWeight = 1;
    public float alignmentWeight = 1;
    public float centerWeight = 1;

    public float boundaryWeight = 1;
    public float targetWeight = 1;

    public float speed = 1;
    public float rotationSpeed = 10;
}
