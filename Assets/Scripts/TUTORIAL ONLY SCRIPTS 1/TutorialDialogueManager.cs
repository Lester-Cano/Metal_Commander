using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDialogueManager : MonoBehaviour
{
    public Queue<string> sentences;
    public TextTutorial dialogue;
    public Text dialogueText;

    public Image imageEnemy;
    public Image imageAlly;
    public Canvas canvasTutorial, canvasUI;

    public GameObject arrow;

    public int counter = 0;

    void Start()
    {
        sentences = new Queue<string>();
        dialogue = GetComponent<TextTutorial>();
        StartTutorial(dialogue.dialogue);
    }


    public void StartTutorial(Dialogue dialogue)
    {
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }


    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        counter++;

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

        if (counter == 2)
        {
            imageEnemy.gameObject.SetActive(false);
            imageAlly.gameObject.SetActive(true);
        }
        

    }

    void EndDialogue()
    {

        arrow.gameObject.SetActive(true);
        canvasTutorial.gameObject.SetActive(false);
        imageAlly.gameObject.SetActive(false);

    }

}
