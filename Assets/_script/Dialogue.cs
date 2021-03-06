﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//! dialog teks
[RequireComponent(typeof(Text))]
public class Dialogue : MonoBehaviour
{
    private Text _textComponent;
    public Text _textNama;/*!<teks nama NPC*/

    public string[] DialogueLines;/*!<teks dialog yang diucapkan NPC*/
    public string nama;/*!<nama NPC dalam string*/

    public float SecondsBetweenCharacters = 0.15f; /*!<jeda waktu dalam memunculkan karakter huruf*/
    public float CharacterRateMultiplier = 0.5f; /*!<jeda waktu dipercepat jika menekan enter*/

    public KeyCode DialogueInput = KeyCode.Return;/*!<dialog muncul ketika menekan enter*/

    private bool _isStringBeingRevealed = false;
    private bool _isDialoguePlaying = false;
    private bool _isEndOfDialogue = false;

    //public GameObject ContinueIcon;
    //public GameObject StopIcon;

	// Use this for initialization
	void Start ()
	{
	    _textComponent = GetComponent<Text>();
	    _textComponent.text = "";
        _textNama.text = nama;

       // HideIcons();
	}
	
	// Update is called once per frame
	void Update () 
	{
	    if (Input.GetKeyDown(KeyCode.Return))
	    {
	        if (!_isDialoguePlaying)
	        {
                _isDialoguePlaying = true;
                StartCoroutine(StartDialogue());
            }
	        
	    }
	}

    private IEnumerator StartDialogue()
    {
        int dialogueLength = DialogueLines.Length;
        int currentDialogueIndex = 0;

        while (currentDialogueIndex < dialogueLength || !_isStringBeingRevealed)
        {
            if (!_isStringBeingRevealed)
            {
                _isStringBeingRevealed = true;
                StartCoroutine(DisplayString(DialogueLines[currentDialogueIndex++]));

                if (currentDialogueIndex >= dialogueLength)
                {
                    _isEndOfDialogue = true;
                }
            }

            yield return 0;
        }

        while (true)
        {
            if (Input.GetKeyDown(DialogueInput))
            {
                break;
            }

            yield return 0;
        }

       // HideIcons();
        _isEndOfDialogue = false;
        _isDialoguePlaying = false;
    }

    private IEnumerator DisplayString(string stringToDisplay)
    {
        int stringLength = stringToDisplay.Length;
        int currentCharacterIndex = 0;

        //HideIcons();

        _textComponent.text = "";

        while (currentCharacterIndex < stringLength)
        {
            _textComponent.text += stringToDisplay[currentCharacterIndex];
            currentCharacterIndex++;

            if (currentCharacterIndex < stringLength)
            {
                if (Input.GetKey(DialogueInput))
                {
                    yield return new WaitForSeconds(SecondsBetweenCharacters*CharacterRateMultiplier);
                }
                else
                {
                    yield return new WaitForSeconds(SecondsBetweenCharacters);
                }
            }
            else
            {
                break;
            }
        }

       // ShowIcon();

        while (true)
        {
            if (Input.GetKeyDown(DialogueInput))
            {
                break;
            }

            yield return 0;
        }

       // HideIcons();

        _isStringBeingRevealed = false;
        _textComponent.text = "";
    }

    /*private void HideIcons()
    {
        ContinueIcon.SetActive(false);
        StopIcon.SetActive(false);
    }

    private void ShowIcon()
    {
        if (_isEndOfDialogue)
        {
            StopIcon.SetActive(true);
            return;
        }

        ContinueIcon.SetActive(true);
    }*/
}
