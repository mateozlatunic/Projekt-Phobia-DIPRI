using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpBalloon : MonoBehaviour
{
    public GameObject BalloonPickUp;
    public GameObject PickUpText;
    void Start()
    {
        BalloonPickUp.SetActive(false);
        PickUpText.SetActive(false);
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PickUpText.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                this.gameObject.SetActive(false);

                BalloonPickUp.gameObject.SetActive(true);

                PickUpText.SetActive(false);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        PickUpText.SetActive(false);
    }
}
