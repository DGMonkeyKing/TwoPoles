using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractLevel : MonoBehaviour
{
    [SerializeField]
    private Transform P1StartPoint;
    [SerializeField]
    private Transform P2StartPoint;

    Transform[] allChildren;

    private PlayerController PLAYER1;
    private PlayerController PLAYER2;

    void Awake()
    {
        GameObject[] pc = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < pc.Length; i++)
        {
            if (pc[i].GetComponent<PlayerController>().PlayerNumber == PlayerController.PlayerNum.P1)
            {
                PLAYER1 = pc[i].GetComponent<PlayerController>();
            } 
            else if (pc[i].GetComponent<PlayerController>().PlayerNumber == PlayerController.PlayerNum.P2)
            {
                PLAYER2 = pc[i].GetComponent<PlayerController>();
            } 
        }
        
        allChildren = GetComponentsInChildren<Transform>();
        DesactiveMe();
    }

    void Start()
    {
        
    }

    public void ActiveMe()
    {
        if(PLAYER1 != null) PLAYER1.transform.position = P1StartPoint.position; 
        if(PLAYER2 != null) PLAYER2.transform.position = P2StartPoint.position;


        foreach (Transform child in allChildren)
        {
            child.gameObject.SetActive(true);
        }
    }
    public void DesactiveMe()
    {
        foreach (Transform child in allChildren)
        {
            child.gameObject.SetActive(false);
        }
    }
}
