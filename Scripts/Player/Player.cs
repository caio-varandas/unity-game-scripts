using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//controla o movimento e ações básicas do player
public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;


    private Rigidbody2D rig;
    private float initialSpeed;
    private bool _isRunning;
    private bool _isRoolling;
    private bool _isCutting;
    private bool _isDigging;
    private bool _isWatering;

    //direção de movimento do player
    private Vector2 _direction;

    private int handlingObj;

    public Vector2 direction
    { 
        get { return _direction; }
        set { _direction = value; }
    }
    public bool isRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }
    public bool isRolling
    {
        get { return _isRoolling; }
        set { _isRoolling = value; }
    }
    public bool isCutting
    {
        get { return _isCutting; }
        set { _isCutting = value; }
    }
    public bool isDigging
    {
        get { return _isDigging; }
        set { _isDigging = value; }
    }
    public bool isWatering
    {
        get { return _isWatering; }
        set { _isWatering = value; }
    }
    public int HandlingObj { get => handlingObj; set => handlingObj = value; }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        playerItems = GetComponent<PlayerItems>();
        initialSpeed = speed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            HandlingObj = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HandlingObj = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            HandlingObj = 2;
        }
        OnInput();
        OnRun();
        OnRolling();
        OnCutting();
        OnDig();
        OnWatering();
    }

    private void FixedUpdate()
    {
        OnMove();
    }



    //organizar um bloco de codigo
    #region Movement

    void OnWatering()
    {
        if (HandlingObj == 2)
        {
            if (Input.GetMouseButtonDown(0) && playerItems.CurrentWater > 0)
            {
                isWatering = true;
                speed = 0f;
            }
            if (Input.GetMouseButtonUp(0) || playerItems.CurrentWater < 0)
            {
                isWatering = false;
                speed = initialSpeed;
            }
            if (isWatering)
            {
                playerItems.CurrentWater -= 0.01f;
            }
        }
    }
    
    void OnDig()
    {
        if(handlingObj == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDigging = true;
                speed = 0f;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isDigging = false;
                speed = initialSpeed;
            }
        }
    }

    void OnCutting()
    {
        if(handlingObj == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isCutting = true;
                speed = 0f;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isCutting = false;
                speed = initialSpeed;
            }
        }
    }

    //captura a direção de movimento do player
    void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    //move o player respeitando a física
    void OnMove()
    {
        rig.MovePosition(rig.position + _direction * speed * Time.fixedDeltaTime);
    }

    //alterna entre caminhada e corrida
    void OnRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runSpeed;
            _isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = initialSpeed;
            _isRunning = false;
        }
    }

    //detecta a ação de rolagem
    void OnRolling()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _isRoolling = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            _isRoolling = false;
        }
    }

    #endregion
}
