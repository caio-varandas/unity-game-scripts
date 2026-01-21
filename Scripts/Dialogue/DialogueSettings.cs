using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "New Dialogue/Dialogue")]
public class DialogueSettings : ScriptableObject
{
    [Header("Settings")]
    public GameObject actor; //NPC associado a este diálogo


    [Header("Dialogue")]
    public Sprite speakerSprite;
    public string sentence;

    //lista de falas geradas para o NPC
    public List<Sentences> dialogues = new List<Sentences>();
}

//representa uma fala individual do NPC
[System.Serializable]
public class Sentences
{
    public string actorName;
    public Sprite profile;
    public Languages sentence;
}

//texto da fala em múltiplos idiomas
[System.Serializable]
public class Languages
{
    public string portuguese;
    public string english;
    public string spanish;
}

#if UNITY_EDITOR

//editor customizado para facilitar a criação de diálogos
[CustomEditor(typeof(DialogueSettings))]
public class BuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        DialogueSettings ds = (DialogueSettings)target;
        Languages lg = new Languages();
        lg.portuguese = ds.sentence;
        Sentences s = new Sentences();
        s.profile = ds.speakerSprite;
        s.sentence = lg;

        if(GUILayout.Button("Create Dialogue"))
        {
            if(ds.sentence != "")
            {
                ds.dialogues.Add(s);
                ds.speakerSprite = null;
                ds.sentence = "";
            }
        }

    }
}

#endif
