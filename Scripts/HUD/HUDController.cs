using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] private Image waterUIBar;
    [SerializeField] private Image woodUIBar;
    [SerializeField] private Image carrotUIBar;
    [SerializeField] private Image fishUIBar;

    [Header("Tools")]
    //[SerializeField] private Image axeUI;
    //[SerializeField] private Image shovelUI;
    //[SerializeField] private Image bucketUI;
    public List<Image> toolsUI = new List<Image>();
    [SerializeField] private Color selectColor;
    [SerializeField] private Color alphaColor;

    private PlayerItems playerItems;
    private Player player;

    private void Awake()
    {
        playerItems = FindAnyObjectByType<PlayerItems>();
        player = playerItems.GetComponent<Player>(); //so para puxar o objeto ja que estão no mesmo objeto
    }


    void Start()
    {
        waterUIBar.fillAmount = 0f;
        woodUIBar.fillAmount = 0f;
        carrotUIBar.fillAmount = 0f;
        fishUIBar.fillAmount = 0f;
    }

    void Update()
    {
        waterUIBar.fillAmount = playerItems.CurrentWater / playerItems.WaterLimit1;
        woodUIBar.fillAmount = playerItems.TotalWood / playerItems.WoodLimit;
        carrotUIBar.fillAmount = playerItems.TotalCarrots / playerItems.CarrotsLimit;
        fishUIBar.fillAmount = playerItems.Fishes / playerItems.FishesLimit;

        for(int i = 0; i < toolsUI.Count; i++)
        {
            toolsUI[i].color = alphaColor;
        }
        toolsUI[player.HandlingObj].color = selectColor;
    }
}
