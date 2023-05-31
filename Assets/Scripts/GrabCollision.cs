using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GrabCollision : MonoBehaviour
{
    public GameObject grabStateObj;
    public Vector3 grabPosition;
    int grabState;
    public int handCollision;
    public int grabHand = 0;
    Vector3 intPos;
    Vector3 endPos;
    public float delta_y;

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        grabState = grabStateObj.GetComponent<AnimationCode>().grabState;
        intPos = gameObject.transform.position;
        if (collision.gameObject.tag == "Interactive" && grabState == 3)
        {
            
            grabPosition = collision.gameObject.transform.position;
            Debug.Log("Collide Grab Both");
            handCollision = 1;
        }

        else if (collision.gameObject.tag == "Interactive" && grabState == 1)
        {
            grabPosition = collision.gameObject.transform.position;
            Debug.Log("Collide Grab Left");
            handCollision = 1;
        }

        else if (collision.gameObject.tag == "Interactive" && grabState == 2)
        {
            grabPosition = collision.gameObject.transform.position;
            Debug.Log("Collide Grab Right");
            handCollision = 1;
        }

        else if (collision.gameObject.tag == "Interactive" && grabState == 0)
        {
            //Debug.Log("Collide");
            handCollision = 1;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        endPos = gameObject.transform.position;
        handCollision = 0;
    }
    private void Update()
    {
        delta_y = endPos.y - intPos.y;
        Debug.Log(delta_y);
    }
}