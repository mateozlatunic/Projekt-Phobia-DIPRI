using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Dodajemo namespace za UI
using UnityEngine.SceneManagement; // Dodajemo namespace za Scene Management
using TMPro; // Dodajemo namespace za TextMeshPro

public class interaction : MonoBehaviour
{
    // The distance from which the player can interact with an object
    public float interactionDistance;

    // Text or crosshair that shows up to let the player know they can interact with an object they're looking at
    public GameObject interactionText;
    private Text interactionTextComponent; // Tekst komponenta za promjenu teksta
    private TextMeshProUGUI interactionTMPComponent; // TMP komponenta za promjenu teksta

    // Layers the raycast can hit/interact with. Any layers unchecked will be ignored by the raycast.
    public LayerMask interactionLayers;

    void Start()
    {
        if (interactionText != null)
        {
            // Pokušaj pronaći Text komponentu unutar interactionText objekta
            interactionTextComponent = interactionText.GetComponentInChildren<Text>();

            // Pokušaj pronaći TextMeshProUGUI komponentu unutar interactionText objekta
            interactionTMPComponent = interactionText.GetComponentInChildren<TextMeshProUGUI>();

            // Ako nema pronađene ni Text ni TextMeshProUGUI komponente, prijavi grešku
            if (interactionTextComponent == null && interactionTMPComponent == null)
            {
                Debug.LogError("No Text or TextMeshProUGUI component found within interactionText.");
            }
        }
        else
        {
            Debug.LogError("interactionText is not assigned.");
        }
    }

    // The Update() void is used to make stuff happen every frame
    void Update()
    {
        if (interactionTextComponent == null && interactionTMPComponent == null)
        {
            return; // Ako nema ni interactionTextComponent ni interactionTMPComponent, izađi iz metode
        }

        // RaycastHit variable which will collect information from objects the raycast hits
        RaycastHit hit;

        // If the raycast hits something,
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, interactionLayers))
        {
            // If the object it hit contains the letter script,
            if (hit.collider.gameObject.GetComponent<letter>())
            {
                // The interaction text will enable
                interactionText.SetActive(true);

                letter hitLetter = hit.collider.gameObject.GetComponent<letter>();

                // Promjena teksta ovisno o tome je li pismo otvoreno
                if (hitLetter.isLetterOpen())
                {
                    SetInteractionText("[ESC] to close");
                }
                else
                {
                    SetInteractionText("[E] to open");
                }

                // If the E key is pressed,
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // The letter component is accessed and the letter will open or close
                    hitLetter.openCloseLetter();
                }
            }
            // If the object it hit contains the newScene script,
            else if (hit.collider.gameObject.GetComponent<newScene>())
            {
                // The interaction text will enable
                interactionText.SetActive(true);
                SetInteractionText("[E] to enter safe room");

                // If the E key is pressed,
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Učitaj novu scenu
                    newScene sceneSwitcher = hit.collider.gameObject.GetComponent<newScene>();
                    if (sceneSwitcher != null)
                    {
                        sceneSwitcher.LoadScene();
                    }
                }
            }
            // else, the interaction text is set false.
            else
            {
                interactionText.SetActive(false);
            }
        }
        // else, the interaction text is set false.
        else
        {
            interactionText.SetActive(false);
        }

        // If the ESC key is pressed,
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Find all letters and close them
            foreach (letter letterScript in FindObjectsOfType<letter>())
            {
                letterScript.closeLetter();
            }
        }
    }

    private void SetInteractionText(string text)
    {
        if (interactionTextComponent != null)
        {
            interactionTextComponent.text = text;
        }
        else if (interactionTMPComponent != null)
        {
            interactionTMPComponent.text = text;
        }
    }
}
