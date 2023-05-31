using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRotation : MonoBehaviour
{
    public Transform origin;
    public Transform destination;
    Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        direction = destination.position - origin.position;
        gameObject.transform.forward = -direction;
    }
}
