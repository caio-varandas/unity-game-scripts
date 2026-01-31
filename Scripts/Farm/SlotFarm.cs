using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarm : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;

    [Header("Settings")]
    [SerializeField] private int digAmount; //tempo para o player cavar para o buraco aparecer
    [SerializeField] private float waterAmount; //quantidade de aágua para nascer uma cenoura

    [SerializeField] private bool detecting;

    private int initialDigAmount;
    private float currentWater;
    private bool dugHole;

    PlayerItems playerItems;

    void Start()
    {
        playerItems = FindObjectOfType<PlayerItems>();
        initialDigAmount = digAmount;
    }
    void Update()
    {
        if (dugHole)
        {
            if (detecting)
            {
                currentWater += 0.01f;
            }

            if(currentWater >= waterAmount) //encheu total de água necessaria
            {
                spriteRenderer.sprite = carrot;

                if(Input.GetKeyDown(KeyCode.E))
                {
                    spriteRenderer.sprite = hole;
                    playerItems.TotalCarrots++;
                    currentWater = 0f;
                }
            }
        }
        
    }
    public void OnHit()
    {
        digAmount--;

        if (digAmount <= initialDigAmount / 2) //cria o buraco
        { 
            spriteRenderer.sprite = hole;
            dugHole = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dig"))
        {
            OnHit();
        }

        if (collision.CompareTag("Water"))
        {
            detecting = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            detecting = false;
        }
    }
}
