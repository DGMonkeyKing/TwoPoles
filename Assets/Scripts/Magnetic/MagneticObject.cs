using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MagneticObject : MonoBehaviour
{
    [SerializeField]
    protected MagneticField.Polarity polarity;
    public MagneticField.Polarity Polarity
    {
        get {return polarity;}
    }
    
    protected Rigidbody2D m_Rigidbody2D;
    protected bool conducting = true;
    private GameObject[] pc;

    protected void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < pc.Length; i++)
        {
            pc[i].GetComponent<PlayerController>().OnWaste.AddListener(StartMagnetism);
            pc[i].GetComponent<PlayerController>().OnCharge.AddListener(StopMagnetism);
        }
    }

    private void StartMagnetism()
    {
        conducting = true;
        for(int i = 0; i < pc.Length; i++)
        {
            PlayerController p = pc[i].GetComponent<PlayerController>();
            if(p.IsConducting())
            {
                int inverse = 
                    ((Polarity != MagneticField.Polarity.metal) && 
                    (p.Polarity == Polarity)) ? -1 : 1;
                m_Rigidbody2D.AddForce((p.transform.position - this.transform.position) * (inverse * p.ConductingForce));
            }
        }
    }

    private void StopMagnetism()
    {
    }

    // Desde aqui nos  aseguramos que es el player
    void FixedUpdate() 
    {
        //if(conducting && pc.IsConducting())
        //{

        //}
    }

    public bool IsConducting()
    {
        return conducting;
    }
}
