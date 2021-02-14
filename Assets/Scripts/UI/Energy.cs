using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Energy : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer m_Back;
    [SerializeField]
    private SpriteRenderer m_Rays;
    [SerializeField]
    private RectTransform m_Viewport;

    private Animator m_Animator;
    private bool charging = true;

    private float deltaWaste = 0.005f;
    private float deltaCharge = 0.005f;

    [SerializeField]
    private UnityEvent OnNoEnergy;
    [SerializeField]
    private UnityEvent OnFullEnergy;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    
        m_Rays.enabled = false;
    }

    void Update()
    {
        if(charging)
        {
            if(m_Viewport.localPosition.x <= 0)
                //Aumentar Xpos de Viewport
                m_Viewport.localPosition = new Vector2(
                    m_Viewport.localPosition.x+deltaCharge,
                    m_Viewport.localPosition.y
                );
            else OnFullEnergy.Invoke();
        }
        else
        {
            if(m_Viewport.localPosition.x >= -5.2f)
                //Disminuir Xpos de Viewport
                m_Viewport.localPosition = new Vector2(
                    m_Viewport.localPosition.x-deltaWaste,
                    m_Viewport.localPosition.y
                );
            else OnNoEnergy.Invoke();
        }
    }

    public void WasteEnergy()
    {
        charging = false;
        m_Rays.enabled = true;
    }

    public void RestartEnergy()
    {
        charging = true;
        m_Rays.enabled = false;
    }

    public void AutomaticallyCharge()
    {
        StartCoroutine("Recharge");
    }

    IEnumerator Recharge()
    {
        return null;//Aumentar Xpos de Viewport hasta el final
    }
}
