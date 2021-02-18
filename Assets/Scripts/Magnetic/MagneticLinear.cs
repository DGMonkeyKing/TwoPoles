using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticLinear : MagneticField
{
    [SerializeField]
    private float magneticWidthScope;
    [SerializeField]
    private float magneticHeightScope;

    [SerializeField]
    private bool vertical = true;
    
    private BoxCollider2D collider;
    float virtualBaseX, virtualBaseY;

    void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        float virtualBaseX, virtualBaseY;
        virtualBaseX = magneticBase.x;
        virtualBaseY = magneticBase.y;
        if(vertical) virtualBaseY -= magneticHeightScope/2;
        else virtualBaseX -= magneticWidthScope/2;

        Gizmos.DrawCube(new Vector2(virtualBaseX, virtualBaseY), 
            new Vector3(magneticWidthScope, magneticHeightScope, 1));
    }

    void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;

        if(vertical) magneticBase.y -= magneticHeightScope/2;
        else magneticBase.x -= magneticWidthScope/2;

        collider.offset = new Vector2(magneticBase.x, magneticBase.y);
        collider.size = new Vector2( Mathf.Abs(magneticWidthScope), Mathf.Abs(magneticHeightScope));
        collider.isTrigger = true;
    }

    protected override void ApplyForce(Rigidbody2D target, bool invert)
    {
        Debug.Log("ACCION");
        var direction = (vertical) ? new Vector3(0,1,0) : new Vector3(1,0,0);
        int i = (invert) ? -1 : 1;
        target.AddForce(direction * (i * force));
    }
}
