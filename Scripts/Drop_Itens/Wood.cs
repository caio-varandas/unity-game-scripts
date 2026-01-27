using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeMove; //tempo que a madeira se move após spawnar

    private float timeCount; //contador de tempo

    void Update()
    {
        timeCount += Time.deltaTime;

        if(timeCount < timeMove) //move a madeira por um curto período para dar efeito de "drop"
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //detecta quando o jogador encosta na madeira
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerItems>().TotalWood++;
            Destroy(gameObject);
        }
    }
}
