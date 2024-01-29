using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteHallway : MonoBehaviour
{
    private bool toActivateUp = true;
    private bool toActivateDown = false;

    public Collider tp1;

    public GameObject tpTargetUp;

    private PlayerMovement playerMovement;

    private void Update()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Teleport")
        {
            Debug.Log("hola");

            if (toActivateUp)
            {
                StartCoroutine("Teleport");
            }
        }
    }

    private IEnumerator Teleport()
    {
        playerMovement.enabled = false;
        yield return new WaitForSeconds(0.001f);
        transform.position = new Vector3(transform.position.x, transform.position.y + 4.78f, transform.position.z);
        yield return new WaitForSeconds(0.001f);
        playerMovement.enabled = true;
    }
}