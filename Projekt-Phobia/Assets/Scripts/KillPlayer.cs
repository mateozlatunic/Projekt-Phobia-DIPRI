using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    public string nextSceneName;
    public float delay = 0.5f; 
    public GameObject fadeout;

    bool playerInsideTrigger = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = true;
            fadeout.SetActive(true);
            Invoke("LoadNextScene", delay);
        }
    }

    void LoadNextScene()
    {
        if (playerInsideTrigger)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
