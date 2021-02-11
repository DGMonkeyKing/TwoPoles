using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**Componente que define un objeto magnetico.*/
public abstract class MagneticObject : MonoBehaviour
{
    public enum Polarity { plus, minus };

    [SerializeField]
    protected Polarity polarity;

    protected Vector2 magneticBase;
    [SerializeField]
    protected float force;

    [SerializeField]
    protected LayerMask effectLayers;

    protected void OnDrawGizmos()
    {
        // Draw a semitransparent blue cube at the transforms position
        if(polarity == Polarity.plus) Gizmos.color = new Color(1, 0, 0, 0.2f);
        else if(polarity == Polarity.minus) Gizmos.color = new Color(0, 0, 1, 0.2f);

        magneticBase = GetComponent<Transform>().position;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Check if something enter in the magnetic field
        
    }


///////////////////////////////////////////////////////////////
/////// MAGNETIC CHECK FIELD //////////////////////////////////
///////////////////////////////////////////////////////////////
/*
    private Dictionary<int, float> go2Gravity = new Dictionary<int, float>();
    private void OnTriggerEnter2D(Collider2D other) 
    {
        int mask = 1 << other.gameObject.layer;
        int tmp = effectLayers & mask;
        bool result = tmp != 0;

        if (result)
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb) 
            {
                if(!go2Gravity.ContainsKey(other.GetInstanceID()))
                    go2Gravity.Add(other.GetInstanceID(), rb.gravityScale);
            }
            else Debug.LogError("NO HAY RIGIDBODY2D EN EL OBJETO ATRAIDO.");
        }
    }
*/
    private void OnTriggerStay2D(Collider2D other) 
    {
        int mask = 1 << other.gameObject.layer;
        int tmp = effectLayers & mask;
        bool result = tmp != 0;

        if (result)
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            bool invert = false;
            if(pc)
            {
                Debug.Log("pc:OKEY");
                if(!pc.IsConduting()) return;
                else invert = (pc.Polarity == polarity) ? true : false;  
            } 
            if (rb) ApplyForce(rb, invert);
            else Debug.LogError("NO HAY RIGIDBODY2D EN EL OBJETO ATRAIDO.");
        }
    }
/*
    private void OnTriggerExit2D(Collider2D other) 
    {
        int mask = 1 << other.gameObject.layer;
        int tmp = effectLayers & mask;
        bool result = tmp != 0;

        if (result)
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb) rb.gravityScale = go2Gravity[other.GetInstanceID()];
            else Debug.LogError("NO HAY RIGIDBODY2D EN EL OBJETO ATRAIDO.");
        }
    }
*/
    protected abstract void ApplyForce(Rigidbody2D target, bool invert);
}
