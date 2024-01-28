using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteHallway : MonoBehaviour
{
    private bool toActivateUp = true;
    private bool toActivateDown = true;

    private void Update()
    {
        if (transform.position.z <= -5 && toActivateUp)
        {
            Debug.Log("hola;");
            Vector3 pos = transform.position;
            pos.y += 5;
            transform.position = pos;
            toActivateUp = false;
        }
    }
}