using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;
using System;

public class AnimationCode : MonoBehaviour
{
    // Start is called before the first frame update
    public UDPReceive udpReceive;
    public GameObject[] Body;
    public GameObject Anchor;
    public GameObject[] HandsModels;
    public GameObject LH;
    public GameObject RH;
    public int grabState = 0;
    Vector3 LhGrabPos;
    Vector3 RhGrabPos;
    int LhCollision;
    int RhCollision;
    float LhDelta_y;
    float RhDelta_y;
    Vector3 tempPos;

    // Update is called once per frame
    void Update()
    {
        string data = udpReceive.data;
        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        string[] points = data.Split(',');
        string grab = data.Remove(0, data.Length - 2);

        if (grab == "b'")
        {
            // lh
            HandsModels[0].SetActive(false);
            HandsModels[1].SetActive(true);
            HandsModels[2].SetActive(false);
            HandsModels[3].SetActive(true);
            grabState = 3;
        }
        else if (grab == "l'")
        {
            HandsModels[0].SetActive(false);
            HandsModels[1].SetActive(true);
            HandsModels[2].SetActive(true);
            HandsModels[3].SetActive(false);
            grabState = 1;
        }
        else if (grab == "r'")
        {
            HandsModels[0].SetActive(true);
            HandsModels[1].SetActive(false);
            HandsModels[2].SetActive(false);
            HandsModels[3].SetActive(true);
            grabState= 2;
        }
        else
        {
            HandsModels[0].SetActive(true);
            HandsModels[1].SetActive(false);
            HandsModels[2].SetActive(true);
            HandsModels[3].SetActive(false);
            grabState = 0;
        }

        for (int i = 0; i <= 32; i++)
        {
            float x = float.Parse(points[0 + (i * 3)]);
            float y = float.Parse(points[1 + (i * 3)]);
            float z = float.Parse(points[2 + (i * 3)]) / 3;
            LhCollision = LH.GetComponent<GrabCollision>().handCollision;
            RhCollision = RH.GetComponent<GrabCollision>().handCollision;
            if (i == 15 && (grabState == 1 || grabState == 3) && LhCollision == 1)
            {
                LhGrabPos = LH.GetComponent<GrabCollision>().grabPosition;
                LH.transform.position = LhGrabPos;
                Debug.Log(LhGrabPos);
                continue;
            }
            else if (i == 15)
            {
                LH.transform.localPosition = new Vector3 (x,y,z);
                LhGrabPos = new Vector3 (x,y,z);
            } 

            if (i == 16 && (grabState == 2 || grabState ==3) && RhCollision == 1)
            {
                RhGrabPos = RH.GetComponent<GrabCollision>().grabPosition;
                RH.transform.position = RhGrabPos;
                Debug.Log(RhGrabPos);
                continue;
            }
            else if (i == 16)
            {
                RH.transform.localPosition = new Vector3(x,y,z);
                RhGrabPos = new Vector3(x, y, z);
            }

            Body[i].transform.localPosition = new Vector3(x, y, z);
            Body[i].transform.localPosition += Anchor.transform.position;
            continue;
        }

        LhDelta_y = LH.GetComponent<GrabCollision>().delta_y;
        LhDelta_y = Math.Min(LhDelta_y, 0);
        RhDelta_y = RH.GetComponent<GrabCollision>().delta_y;
        RhDelta_y = Math.Min(RhDelta_y, 0);
        tempPos = Anchor.transform.position;

        if (grabState == 3)
        {
            tempPos.y += (-(LhDelta_y + RhDelta_y) / 2) * 0.05f;
            Anchor.transform.position = tempPos;
        }
        else
        {
            if (grabState == 1)
            {
                tempPos.y += -LhDelta_y * 0.05f;
                Anchor.transform.position = tempPos;
            }
            if (grabState == 2)
            {
                tempPos.y += -RhDelta_y * 0.05f;
                Anchor.transform.position = tempPos;
            }
        }
    }
}
