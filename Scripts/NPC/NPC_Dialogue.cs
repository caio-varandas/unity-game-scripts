using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//responsável por detectar o player e iniciar o diálogo do NPC
public class NPC_Dialogue : MonoBehaviour
{
    public float dialogueRange;//raio de interação com o NPC
    public LayerMask playerLayer;

    public DialogueSettings dialogue; //dados de diálogo associados ao NPC

    bool playerHit;

    private List<string> sentences = new List<string>(); //frases filtradas pelo idioma atual
    private List<string> actorName = new List<string>();
    private List<Sprite> actorSprite = new List<Sprite>();

    void Start()
    {
        GetNPCInfo();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerHit)
        {
            DialogueControl.instance.Speech(sentences.ToArray(), dialogue);
        }
    }

    //carrega as falas do NPC de acordo com o idioma selecionado
    void GetNPCInfo()
    {
        for(int i = 0; i < dialogue.dialogues.Count; i++)
        {
            switch (DialogueControl.instance.language)
            {
                case DialogueControl.idiom.pt:
                    sentences.Add(dialogue.dialogues[i].sentence.portuguese);
                    break;

                case DialogueControl.idiom.eng:
                    sentences.Add(dialogue.dialogues[i].sentence.english);
                    break;

                case DialogueControl.idiom.spa:
                    sentences.Add(dialogue.dialogues[i].sentence.spanish);
                    break;
            }
            actorName.Add(dialogue.dialogues[i].actorName);
            actorSprite.Add(dialogue.dialogues[i].profile);
        }
    }

    void FixedUpdate()
    {
        ShowDialogue();
    }

    //detecta a presença do player dentro do raio de diálogo
    void ShowDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);

        if(hit != null)
        {
            playerHit = true;
        }
        else
        {
            playerHit = false;
        }
    }

    //visualiza o alcance de diálogo do NPC no editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}
