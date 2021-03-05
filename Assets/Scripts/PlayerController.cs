using System.Linq;
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
    public PlayerNum PlayerNumber
    {
        get {return playerNum;}
    }

    [Header("Magnetic Properties")]
    [SerializeField]
    private MagneticField.Polarity polarity;
    public MagneticField.Polarity Polarity
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
    public bool IsGrounded
    {
        get {return isGrounded;}
    }

    [Header("Movement Parameters")]
    [SerializeField]
    public float speed = 5f;
    [SerializeField]
    public float jumpForce = 5f;
    [Range(0, .3f)] [SerializeField] 
    private float movementSmoothing = .05f;  // How much to smooth out the movement    
    [SerializeField]
    private float conductingForce = 30f;
    public float ConductingForce
    {
        get {return conductingForce;}
    }
    [SerializeField]
    private GameObject electricity;
    
    [Header("Animation Variations")]
    [Range(.2f, .5f)] [SerializeField] 
    private float m_StretchVariation = .3f;
    [Range(.2f, .5f)] [SerializeField] 
    private float m_ShortenVariation = .3f;
    [Range(.2f, .5f)] [SerializeField] 
    private float m_ExpandVariation = .3f;
    [Range(.2f, .5f)] [SerializeField] 
    private float m_ShrinkVariation = .3f;

    [Header("Events")]
    [SerializeField]
    public UnityEvent OnWaste;
    [SerializeField]
    public UnityEvent OnCharge;
    [SerializeField]
    public UnityEvent OnChargeAll;

    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;
    private PlayerController m_OtherPlayer;

    private float horizontalInput;
    private float verticalInput;
    private bool jumpInput;
    private bool conducting;
    private bool stillPressing = true;
    private bool noEnergy = false;
    public bool NoEnergy
    {
        get {return noEnergy;} set {noEnergy = value;}
    }

    private float baseGravityScale;

    private Vector3 m_Velocity = Vector3.zero;
    private Vector3 m_Scale = Vector3.one;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        GameObject[] pc = GameObject.FindGameObjectsWithTag("Player");
        m_OtherPlayer = pc.Where(val => val != this.gameObject).ToArray()[0].GetComponent<PlayerController>();

        electricity.SetActive(false);
        baseGravityScale = m_Rigidbody2D.gravityScale;
    }


/// HANDLE WHETHER OR NOT THE PLAYER IS OUT OF SCREEN.
    void OnBecameVisible()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        stillPressing = false;
        if (colliders.Length == 0) {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x,0);
             m_Rigidbody2D.AddForce(new Vector3(0,1,0) * jumpForce*1.3f, ForceMode2D.Impulse);
        }   
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        jumpInput = conducting = noEnergy = false;
        //Charge
        OnCharge.Invoke();
    }
////////////////////////////////////////////////////////

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

        if(isGrounded)
        {
            if(!stillPressing && jumpInput)
            {
                stillPressing = true;
                Stretch();
                isGrounded = false;
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x,0);
                m_Rigidbody2D.AddForce(new Vector3(0,1,0) * jumpForce, ForceMode2D.Impulse);
            } 
        }
        if(stillPressing && !jumpInput) //Soltamos el botón o se acaba el tiempo
        {
            stillPressing = false;
            if(m_Rigidbody2D.velocity.y > 0) m_Rigidbody2D.velocity = new Vector2( m_Rigidbody2D.velocity.x, 0f);
        }

        // Conducting
        conducting = Input.GetButton ("Action - " + playerNum.ToString());
        if(conducting && !noEnergy)
        {
            Expand();
            electricity.SetActive(true);
            stillPressing = false;
            //Waste
            OnWaste.Invoke();
        }
        if(noEnergy || Input.GetButtonUp("Action - " + playerNum.ToString()))
        {
            Shrink();
            electricity.SetActive(false);
            //Charge
            OnCharge.Invoke();
        }

        //Check if conducting
        if(conducting)
        {
            if(m_OtherPlayer.IsConducting() && !isGrounded)
            {
                int inverse = 
                    ((Polarity != MagneticField.Polarity.metal) && 
                    (m_OtherPlayer.Polarity == Polarity)) ? -1 : 1;
                m_Rigidbody2D.AddForce(
                    Vector3.Normalize(
                        (m_OtherPlayer.transform.position - this.transform.position)) 
                        * (inverse * (m_OtherPlayer.ConductingForce*0.1f)), ForceMode2D.Impulse);
            }
        }

        bool wasGrounded = isGrounded;
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject && m_Rigidbody2D.velocity.y <= 0)
            {
                isGrounded = true;
                if(!wasGrounded) {
                    Shorten();
                }
            } 
        }
        if(colliders.Length == 0) isGrounded = false;

        //Update values on Animator
        m_Animator.SetFloat("speed", horizontalInput);
        m_Animator.SetBool("jumping", !isGrounded);
        m_Animator.SetBool("action", IsConducting());
    }

    public bool IsConducting()
    {
        return conducting && !noEnergy;
    }

    // Grow y shrink x
    public void Stretch()
    {
        transform.localScale = m_Scale + (Vector3.up*m_StretchVariation) + (Vector3.left*m_StretchVariation);  

        StartCoroutine("AnimateMovement");
    }
    // Grow x shrink y
    public void Shorten()
    {
        transform.localScale = m_Scale + (Vector3.down*m_ShortenVariation) + (Vector3.right*m_ShortenVariation); 
        StartCoroutine("AnimateMovement");
    }
    // Grow x and y
    public void Expand()
    {
        transform.localScale = m_Scale + (Vector3.up*m_ExpandVariation) + (Vector3.right*m_ExpandVariation); 
        StartCoroutine("AnimateMovement");
    }
    // Shrink x and y
    public void Shrink()
    {
        transform.localScale = m_Scale + (Vector3.down*m_ShrinkVariation) + (Vector3.left*m_ShrinkVariation); 
        StartCoroutine("AnimateMovement");
    }

    private IEnumerator AnimateMovement()
    {
        float speed = 2f;
        float startTime = Time.time;

        Vector3 starPoint = transform.localScale;
        float journeyLength  = Vector3.Distance(starPoint, m_Scale);

        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;
        
        while (fractionOfJourney < 1f)
        {
            // Set our position as a fraction of the distance between the markers.
            transform.localScale = Vector3.Lerp(starPoint, m_Scale, fractionOfJourney);

            yield return null;
            // Distance moved equals elapsed time times speed..
            distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            fractionOfJourney = distCovered / journeyLength;
        }

        transform.localScale = Vector3.one;
    }

    public void Destruction()
    {
        Debug.Log("DEATH");
        this.gameObject.SetActive(false);
    }
}
