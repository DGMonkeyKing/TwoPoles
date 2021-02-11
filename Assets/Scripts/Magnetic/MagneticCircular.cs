using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticCircular : MagneticObject
{
    //Si la fuerza es negativa, gira en sentido antihorario.

    [SerializeField]
    private float magneticRadiusScope;

    void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Transform T = GetComponent<Transform>();
        float theta = 0, x = magneticRadiusScope*Mathf.Cos(theta), y = magneticRadiusScope*Mathf.Sin(theta);
        Vector3 pos = new Vector3(x + magneticBase.x, y + magneticBase.y), newPos = pos, lastPos = pos;
        
        for(theta = 0.1f;theta<Mathf.PI*2;theta+=0.1f)
        {
            x = magneticRadiusScope*Mathf.Cos(theta);
            y = magneticRadiusScope*Mathf.Sin(theta);
            newPos = new Vector3(x + magneticBase.x, y + magneticBase.y);
            Gizmos.DrawLine(pos,newPos);
            pos = newPos;
        }
        Gizmos.DrawLine(pos,lastPos);
        Gizmos.DrawSphere(magneticBase, magneticRadiusScope);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void ApplyForce(Rigidbody2D target, bool invert)
    {
    }
}
