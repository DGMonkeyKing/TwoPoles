using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticLinear : MagneticObject
{
    [SerializeField]
    private float magneticWidthScope;
    [SerializeField]
    private float magneticHeightScope;
    
    private BoxCollider2D collider;

    void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Transform T = GetComponent<Transform>();
        Gizmos.DrawCube(new Vector2(magneticBase.x, magneticBase.y - magneticHeightScope/2), 
            new Vector3(magneticWidthScope, magneticHeightScope, 1));
    }
    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        
        collider.offset = new Vector2(magneticBase.x, magneticBase.y - magneticHeightScope/2);
        collider.size = new Vector2(magneticWidthScope, magneticHeightScope);
        collider.isTrigger = true;
    }

    protected override void ApplyForce(Rigidbody2D target, bool invert)
    {
        var direction = new Vector3(0,1,0);
        int i = (invert) ? -1 : 1;
        target.AddForce(direction * (i * force));
    }
}
