using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeySystem
{ 
    public class KeyItemController : MonoBehaviour
    {
        [SerializeField] private bool basementDoor = false;
        [SerializeField] private bool upperfloarDoor = false;
        [SerializeField] private bool basementKey = false;
        [SerializeField] private bool upperfloarKey = false;
        [SerializeField] private KeyInventory _keyInventory = null;

        private KeyDoorController doorObject;

        private void Start()
        {
            if (basementDoor)
            {
                doorObject = GetComponent<KeyDoorController>();
            }

            if (upperfloarDoor)
            {
                doorObject = GetComponent<KeyDoorController>();
            }
        }

        public void ObjectInteraction()
        {
            if (basementDoor)
            {
                doorObject.PlayAnimation();
            }

            else if (basementKey)
            {
                _keyInventory.hasBasementKey = true;
                gameObject.SetActive(false);
            }


            if (upperfloarDoor)
            {
                doorObject.PlayAnimation();
            }

            else if (upperfloarKey)
            {
                _keyInventory.hasUpperfloarKey = true;
                gameObject.SetActive(false);
            }
        }
    }
}
