using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{

    [SerializeField] private float treeHealth;
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject woodPrefab; //prefab da madeira que será dropada
    [SerializeField] private int totalWood; //quantidade total de madeiras que irão cair

    [SerializeField] private ParticleSystem leafs;

    private bool isCut = false;

    public void OnHit()
    {
        treeHealth--;

        anim.SetTrigger("isHit");
        leafs.Play(); //executa a animação da particula

        if(treeHealth <= 0)
        {
            for (int i = 0; i < totalWood; i++)  //instancia as madeiras ao redor da árvore
            {
                Instantiate(woodPrefab, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f), transform.rotation);
            }
            anim.SetTrigger("cut");

            isCut = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //detecta a colisão do machado com a árvore
    {
        if (collision.CompareTag("Axe") && !isCut) //verifica se o objeto que colidiu é o machado e se a árvore ainda não foi cortada
        {
            OnHit();
        }
    }
}
