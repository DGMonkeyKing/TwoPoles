using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**Componente que define un objeto magnetico.*/
public class MagneticObject : MonoBehaviour
{
    public enum ForceType { circle, linear };
    public enum Polarity { plus, minus };

    [SerializeField]
    private ForceType forceType;
    [SerializeField]
    private Polarity polarity;

    //Utilizar estos parametros para definir 
    // 1. El movimiento del sistema de particulas
    // 2. La fuerza de atracción/repulsion
    [SerializeField]
    private Vector2 magneticBase;
    [SerializeField]
    private float rangeX;
    [SerializeField]
    private float rangeY;
    [SerializeField]
    private float force;

    [SerializeField]
    private LayerMask effectLayers;

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        if(polarity == Polarity.plus) Gizmos.color = new Color(1, 0, 0, 0.2f);
        else if(polarity == Polarity.minus) Gizmos.color = new Color(0, 0, 1, 0.2f);
        Gizmos.DrawCube(magneticBase, new Vector3(rangeX, rangeY, 1));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
