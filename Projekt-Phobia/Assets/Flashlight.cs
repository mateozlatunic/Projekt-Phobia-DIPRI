using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject flashlight;

    public AudioSource turnOn;
    public AudioSource turnOff;

    public bool on;
    public bool off;

    private void Start()
    {
        off = true;
        flashlight.SetActive(false);
    }

    public void Update()
    {
        if(off && Input.GetButtonDown("F"))
        {
            flashlight.SetActive(true);
            turnOn.Play();
            off = false;
            on = true;
        }
        else if (on && Input.GetButtonDown("F"))
        {
            flashlight.SetActive(false);
            turnOff.Play();
            off = true;
            on = false;
        }
    }
}
