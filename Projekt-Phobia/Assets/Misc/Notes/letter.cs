using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letter : MonoBehaviour
{
    public GameObject letterUI;
    bool toggle;
    public Renderer letterMesh;

    void Start()
    {
        // Automatski pronađi Renderer na ovom GameObject-u ako nije postavljen
        if (letterMesh == null)
        {
            letterMesh = GetComponent<Renderer>();
        }

        // Provjera ako letterMesh nije pronađen
        if (letterMesh == null)
        {
            Debug.LogError("letterMesh is not assigned and no Renderer found on the GameObject.");
        }
    }

    public void openCloseLetter()
    {
        toggle = !toggle;
        if(toggle == false) {
            letterUI.SetActive(false);
            letterMesh.enabled = true;
        }
        if(toggle == true) {
            letterUI.SetActive(true);
            letterMesh.enabled = false;
        }
    }
}
