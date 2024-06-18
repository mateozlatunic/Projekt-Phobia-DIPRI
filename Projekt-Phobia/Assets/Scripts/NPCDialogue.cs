using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCDialogue : MonoBehaviour
{
    // UI element za prikaz dijaloga
    public Text dialogueText;

    void Start()
    {
        // Provjeri je li dialogueText postavljen
        if (dialogueText == null)
        {
            Debug.LogError("DialogueText nije postavljen u inspektoru!");
            return;
        }

        // Inicijalno sakrij dijalog
        dialogueText.text = "";
        dialogueText.enabled = false;
    }

    // Metoda koja pokreće dijalog
    public void ShowDialogue()
    {
        string dialogueLine = "Dobrodošli u naš svijet!"; // Testni tekst
        Debug.Log("Prikazivanje dijaloga: " + dialogueLine);
        dialogueText.text = dialogueLine;
        dialogueText.enabled = true;

        Debug.Log("dialogueText.text: " + dialogueText.text); // Dodano za provjeru
        Debug.Log("dialogueText.enabled: " + dialogueText.enabled); // Dodano za provjeru

        // Pokreni Coroutine za skrivanje dijaloga nakon 5 sekundi
        StartCoroutine(HideDialogueAfterTime(5f));
    }

    private IEnumerator HideDialogueAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        dialogueText.enabled = false;
    }

    void Update()
    {
        // Provjeri je li pritisnuta tipka za interakciju (npr. tipka T) i pokaži dijalog
        if (Input.GetKeyDown(KeyCode.T))
        {
            ShowDialogue();
        }
    }
}
