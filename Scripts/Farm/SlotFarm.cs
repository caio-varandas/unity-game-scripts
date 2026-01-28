using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarm : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;

    [SerializeField] private int digAmount; //tempo para o player cavar para o buraco aparecer
    private int initialDigAmount;

    void Start()
    {
        initialDigAmount = digAmount;
    }

    public void OnHit()
    {
        digAmount--;

        if (digAmount <= initialDigAmount / 2) //cria o buraco
        { 
            spriteRenderer.sprite = hole;
        }

        /*if (digAmount <= 0) //então plantar cenoura
        {
            spriteRenderer.sprite = carrot;
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dig"))
        {
            OnHit();
        }
    }
}
