using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NPC com IA básica de patrulha por pontos (waypoints)
//o personagem se move automaticamente entre posições pré-definidas
//e pausa o movimento quando um diálogo está ativo.
public class NPC : MonoBehaviour
{
    public float speed;
    public float initialSpeed;

    private int index;
    private Animator anim;

    public List<Transform> paths = new List<Transform>(); //lista de pontos de patrulha

    void Start()
    {
        initialSpeed = speed;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //caso um diálogo esteja ativo, o NPC para de se mover
        //para evitar conflitos visuais durante a interação
        if (DialogueControl.instance.IsShowing)
        {
            speed = 0f;
            anim.SetBool("isWalking", false);
        }
        else
        {
            speed = initialSpeed;
            anim.SetBool("isWalking", true);
        }

        //Rigidbody não é utilizado pois não há interação física com o player
        transform.position = Vector2.MoveTowards(transform.position, paths[index].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, paths[index].position) < 0.1f)
        {
            if(index < paths.Count - 1)
            {
                index++; //ele vai para o proximo
                //index = Random.Range(0, paths.Count - 1); ele vai aleatorio
            }
            else
            {
                index = 0;
            }
        }

        Vector2 direction = paths[index].position - transform.position;

        if (direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }
}
