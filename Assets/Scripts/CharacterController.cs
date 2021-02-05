using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    public GameObject topeTecho;
    [SerializeField]
    public GameObject topeSuelo;

    [SerializeField]
    public float speed = 5f;
    [Range(0, .3f)] [SerializeField] 
    private float movementSmoothing = .05f;  // How much to smooth out the movement


    private Animator m_Animator;
    private Rigidbody2D m_Rigidbody2D;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 m_Velocity = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis ("Horizontal");
        verticalInput = Input.GetAxis ("Vertical"); //Useless for now

        Vector3 targetVelocity = new Vector2(horizontalInput * speed, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, movementSmoothing);
        
        //Update values on Animator
    }
}
