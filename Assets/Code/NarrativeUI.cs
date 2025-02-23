using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class NarrativeUI : MonoBehaviour
{

    [SerializeField] GameObject narrativeGameObject;

    [SerializeField] TextMeshProUGUI nameText;

    [SerializeField] TextMeshProUGUI dialogueText;

    [SerializeField] Image dialogueBox;

    [SerializeField] Image nameBox;

    bool coroutineactive = false;

    [SerializeField] int delay;
    [SerializeField] int fadeInDuration;

    private Color originalNameTextColor;
    private Color originalDialogueTextColor;
    private Color originalDialogueBoxColor;
    private Color originalNameBoxColor;

    private Coroutine narrativeCoroutine;
    [SerializeField] private TypeWriterEffect typeWritterEffect;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeTextDialogueBox("Ya corres bue mano");
            ActivateText();
        }
    }

    private void OnEnable()
    {
        interactableO2Supply.OxygenChangedNarrative += ChangeTextDialogueBox;
        FireEvent.FireEventChangedNarrative += ChangeTextDialogueBox;
        FuelThrustersEvent.FuelChangedNarrative += ChangeTextDialogueBox;
        AirLeakEvent.airLeakEventChangedNarrative += ChangeTextDialogueBox;
        PipeBreakEvent.pipeBreakEventChangedNarrative += ChangeTextDialogueBox;
        FuelEventLever.FuelLeverChangedNarrative += ChangeTextDialogueBox;
        FireExtinguisher.fireExtinguisherCallChangedNarrative += ChangeTextDialogueBox;
    }

    private void OnDisable()
    {
        interactableO2Supply.OxygenChangedNarrative -= ChangeTextDialogueBox;
        FireEvent.FireEventChangedNarrative -= ChangeTextDialogueBox;
        FuelThrustersEvent.FuelChangedNarrative -= ChangeTextDialogueBox;
        AirLeakEvent.airLeakEventChangedNarrative -= ChangeTextDialogueBox;
        PipeBreakEvent.pipeBreakEventChangedNarrative -= ChangeTextDialogueBox;
        FuelEventLever.FuelLeverChangedNarrative -= ChangeTextDialogueBox;
        FireExtinguisher.fireExtinguisherCallChangedNarrative -= ChangeTextDialogueBox;
    }

    private void Start()
    {
        originalNameTextColor = nameText.color;
        originalDialogueTextColor = dialogueText.color;
        originalDialogueBoxColor = dialogueBox.color;
        originalNameBoxColor = nameBox.color;
    }
    public void ActivateText()
    {
        narrativeGameObject.SetActive(true);
        nameText.color = new Color(originalNameTextColor.r, originalNameTextColor.g, originalNameTextColor.b, 1f);
        dialogueText.color = new Color(originalDialogueTextColor.r, originalDialogueTextColor.g, originalDialogueTextColor.b, 1f);
        dialogueBox.color = new Color(originalDialogueBoxColor.r, originalDialogueBoxColor.g, originalDialogueBoxColor.b, 1f);
        nameBox.color = new Color(originalNameBoxColor.r, originalNameBoxColor.g, originalNameBoxColor.b, 1f);

        if (narrativeCoroutine != null)
        {
            StopCoroutine(narrativeCoroutine);
            typeWritterEffect.StopCoroutine();
        }

        narrativeCoroutine = StartCoroutine(NarrativeCoruotine(delay,fadeInDuration));
        typeWritterEffect.ResetAndStart(dialogueText.text);
    }

    IEnumerator NarrativeCoruotine(float waitTime, float duration)
    {

        yield return new WaitForSeconds(waitTime);

        float elapsedTime = 0f;
        Color startColorNameText = nameText.color;
        Color startColorDialogueText = dialogueText.color;
        Color startColorDialogueBox = dialogueBox.color;
        Color startColorNameBox = nameBox.color;


        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            nameText.color = new Color(startColorNameText.r, startColorNameText.g, startColorNameText.b, alpha);
            dialogueText.color = new Color(startColorDialogueText.r, startColorDialogueText.g, startColorDialogueText.b, alpha);
            dialogueBox.color = new Color(startColorDialogueBox.r, startColorDialogueBox.g, startColorDialogueBox.b, alpha);
            nameBox.color = new Color(startColorNameBox.r, startColorNameBox.g, startColorNameBox.b, alpha);
            yield return null;
        }

        nameText.color = new Color(startColorNameText.r, startColorNameText.g, startColorNameText.b, 0f);
        dialogueText.color = new Color(startColorDialogueText.r, startColorDialogueText.g, startColorDialogueText.b, 0f);
        dialogueBox.color = new Color(startColorDialogueBox.r, startColorDialogueBox.g, startColorDialogueBox.b, 0f);
        nameBox.color = new Color(startColorDialogueBox.r, startColorDialogueBox.g, startColorDialogueBox.b, 0f);

        coroutineactive = false;
        narrativeGameObject.SetActive(false);

    }

    public void ChangeTextDialogueBox(string text)
    {
        ActivateText();
        dialogueText.text = text;
    }


}
