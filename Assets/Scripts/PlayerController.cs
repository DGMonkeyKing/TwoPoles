using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public enum PlayerNum
    {
        P1,
        P2
    } 

    [Header("Player")]   
    [SerializeField]
    private PlayerNum playerNum;

    [Header("Magnetic Properties")]
    [SerializeField]
    private MagneticObject.Polarity polarity;
    public MagneticObject.Polarity Polarity
    {
        get {return polarity;}
    }

    [Header("Checkers")]
    [SerializeField]
    public GameObject topeSuelo;
    [SerializeField] 
    private Transform m_GroundCheck; // A position marking where to check if the player is grounded.
    [Range(0, .4f)][SerializeField] 
    private float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded    [Header("Movement Parameters")]
    [SerializeField] 
    private LayerMask m_WhatIsGround;  
    private bool isGrounded; 

    [Header("Movement Parameters")]
    [SerializeField]
    public float speed = 5f;
    [SerializeField]
    public float jumpForce = 5f;
    [Range(0, .3f)] [SerializeField] 
    private float movementSmoothing = .05f;  // How much to smooth out the movement

    private Animator m_Animator;
    private Rigidbody2D m_Rigidbody2D;
    private SpriteRenderer m_SpriteRenderer;

    private float horizontalInput;
    private float verticalInput;
    private bool jumpInput;
    private bool actionInput;
    private bool stillPressing = true;

    private float baseGravityScale;

    private Vector3 m_Velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        baseGravityScale = m_Rigidbody2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        horizontalInput = Input.GetAxisRaw ("Horizontal - " + playerNum.ToString());
        verticalInput = Input.GetAxisRaw ("Vertical - " + playerNum.ToString()); //Useless for now

        //If horizontalInput is negative, dale la vuelta.
        if (horizontalInput > 0) 
        {
            horizontalInput = 1;
            m_SpriteRenderer.flipX = true;
        }
        else if (horizontalInput < 0)
        {
            horizontalInput = -1;
            m_SpriteRenderer.flipX = false;
        }

        //Movement
        Vector3 targetVelocity = new Vector2(horizontalInput * speed, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, movementSmoothing);

        //Jumping
        jumpInput = Input.GetButton ("Jump - " + playerNum.ToString());

        if(!stillPressing && isGrounded && jumpInput)
        {
            stillPressing = true;
            isGrounded = false;
            m_Rigidbody2D.AddForce(new Vector3(0,1,0) * jumpForce, ForceMode2D.Impulse);
        } else if(stillPressing && !jumpInput) //Soltamos el botón o se acaba el tiempo
        {
            stillPressing = false;
            m_Rigidbody2D.gravityScale = baseGravityScale;
            if(m_Rigidbody2D.velocity.y > 0) m_Rigidbody2D.velocity = new Vector2( m_Rigidbody2D.velocity.x, 0f);
        }

        // Conducting
        actionInput = Input.GetButton ("Action - " + playerNum.ToString());
        Debug.Log("actionInput: " + actionInput);

        //Update values on Animator
        m_Animator.SetFloat("speed", horizontalInput);
        m_Animator.SetBool("jumping", !isGrounded);
    }


    void FixedUpdate()
    {
        bool wasGrounded = isGrounded;
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject && m_Rigidbody2D.velocity.y <= 0)
            {
                isGrounded = true;
            } 
            else
            {
                isGrounded = false;
            }
        }
    }

    public bool IsConduting()
    {
        return actionInput;
    }
}
