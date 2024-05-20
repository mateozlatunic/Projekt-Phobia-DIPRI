using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLights : MonoBehaviour
{
    public Light lightObject;
    public AudioSource lightSound;

    public float minTime;
    public float maxTime;
    public float timer;
    void Start()
    {
        timer = Random.Range(minTime, maxTime);

    }
    void Update()
    {
        LightsFlickering();
    }
    void LightsFlickering()
    {
        if (timer > 0)
            timer -= Time.deltaTime;

        if (timer <= 0)
        {
            lightObject.enabled = !lightObject.enabled;
            timer = Random.Range(minTime, maxTime);
            lightSound.Play();
        }
    }
}
