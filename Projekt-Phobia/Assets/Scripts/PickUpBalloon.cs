using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpBalloon : MonoBehaviour
{
    public GameObject BalloonPickUp;
    public GameObject PickUpText;
    private playerMovement playerMovementScript;

    void Start()
    {
        BalloonPickUp.SetActive(false);
        PickUpText.SetActive(false);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovementScript = player.GetComponent<playerMovement>();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PickUpText.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                this.gameObject.SetActive(false);
                BalloonPickUp.SetActive(true);
                PickUpText.SetActive(false);

                if (playerMovementScript != null)
                {
                    playerMovementScript.jumpHeight += 6f;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PickUpText.SetActive(false);
        }
    }
}