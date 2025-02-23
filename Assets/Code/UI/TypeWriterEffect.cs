using UnityEngine;
using System.Collections;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    public float delay = 0.1f;
    private string fullText;
    private string currentText = "";
    private TMP_Text tmpText;
    private Coroutine typingCoroutine;

    void Start()
    {
        tmpText = GetComponent<TMP_Text>(); // Get TMP component
        fullText = tmpText.text; // Store the initial text
        tmpText.text = ""; // Clear text before typing effect
    }

    private void StartTyping()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine); // Stop any ongoing effect
        typingCoroutine = StartCoroutine(ShowText());
    }

    public void ResetAndStart(string newText)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // Stop the currently running coroutine
            typingCoroutine = null; // Reset reference
        }

        tmpText.text = ""; // Clear text before typing effect
        fullText = newText; // Set new text
        StartTyping(); // Start effect
    }

    public void StopTyping() // Renamed to avoid Unity confusion
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            tmpText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        typingCoroutine = null; // Reset reference when finished
    }
}