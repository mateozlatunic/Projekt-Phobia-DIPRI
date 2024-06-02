using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoorScript; // Dodaj ovaj redak

namespace KeySystem
{
    public class KeyDoorController : MonoBehaviour
    {
        private Door doorScript; // Referenca na skriptu Door
        private bool doorOpen = false;

        [SerializeField] private int timeToShowUI = 1;
        [SerializeField] private GameObject showDoorLockerUI = null;

        [SerializeField] private KeyInventory _keyInventory = null;

        [SerializeField] private int waitTimer = 1;
        [SerializeField] private bool pauseInteraction = false;

        private void Awake()
        {
            doorScript = gameObject.GetComponent<Door>();
        }

        private IEnumerator PauseDoorInteraction()
        {
            pauseInteraction = true;
            yield return new WaitForSeconds(waitTimer);
            pauseInteraction = false;
        }

        public void PlayAnimation()
        {
            if (_keyInventory.hasBasementKey)
            {
                if (!pauseInteraction)
                {
                    doorScript.OpenDoor();
                    doorOpen = !doorOpen;
                    StartCoroutine(PauseDoorInteraction());
                }
            }
            else
            {
                StartCoroutine(ShowDoorLocked());
            }
        }

        IEnumerator ShowDoorLocked()
        {
            showDoorLockerUI.SetActive(true);
            yield return new WaitForSeconds(timeToShowUI);
            showDoorLockerUI.SetActive(false);
        }
    }
}
