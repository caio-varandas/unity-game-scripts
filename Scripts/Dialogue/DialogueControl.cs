using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt,
        eng,
        spa
    }

    public idiom language;

    [Header("Components")]
    public GameObject dialogueObj; 
    public Image profileSprite;
    public TMP_Text speechText;
    public TMP_Text actorNameText;

    [Header("Settings")]
    public float typingSpeed;

    private bool isShowing; //se a janela esta visível
    private int index; //index das falas
    private string[] sentences;
    private string[] currentActorName;
    private Sprite[] actorSprite;

    private Player player;

    //Singleton: garante acesso global ao DialogueControl
    public static DialogueControl instance; //fazer um Singleton e atraves dele eu posso acessar qualquer variavel ou metodo publico atraves do instance

    public bool IsShowing { get => isShowing; set => isShowing = value; }

    public void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    //IEnumerator no diálogo para conseguir mostrar o texto aos poucos, com tempo entre as letras, sem travar o jogo
    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if(speechText.text == sentences[index])
        {

            if(index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                profileSprite.sprite = actorSprite[index];
                actorNameText.text = currentActorName[index];
                StartCoroutine(TypeSentence());
            }
            else
            {
                speechText.text = "";
                actorNameText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                sentences = null;
                IsShowing = false;
                player.isPaused = false;
            }
        }
    }

    public void Speech(string[] txt, string[] actorName, Sprite[] actorProfile)
    {
        if (!IsShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            currentActorName = actorName;
            actorSprite = actorProfile;
            profileSprite.sprite = actorSprite[index];
            actorNameText.text = currentActorName[index];
            index = 0;
            StartCoroutine(TypeSentence());
            IsShowing = true;
            player.isPaused = true;
        }
    }
}
