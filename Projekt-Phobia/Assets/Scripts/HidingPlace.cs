using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlace : MonoBehaviour
{
    public GameObject hideText, stopHideText;
    public GameObject realPlayer, hidingPlayer;
    public EnemyAI enemyAI;
    public Transform enemyTransform;
    bool inReach, hiding;
    public float loseDistance;

    void Start()
    {
        inReach = false;
        hiding = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            hideText.SetActive(true);
            inReach = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            hideText.SetActive(false);
            inReach = false;
        }
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            HidePlayer();
        }

        if (hiding && Input.GetKeyDown(KeyCode.Q))
        {
            StopHiding();
        }
    }

    void HidePlayer()
    {
        if (hiding == true)
        {
            hideText.SetActive(false);
        }
        hidingPlayer.SetActive(true);
        hideText.SetActive(false);
        stopHideText.SetActive(true);
        hiding = true;
        realPlayer.SetActive(false);
        inReach = false;
        enemyAI.currentState = EnemyAI.EnemyState.Idle;
    }

    void StopHiding()
    {
        stopHideText.SetActive(false);
        realPlayer.SetActive(true);
        hidingPlayer.SetActive(false);
        hiding = false;
    }
}