using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    // GAmeObject de la logica del nivel al que voy
    [SerializeField]
    private int currentLevel;
/*    [SerializeField]
    private float width;
    [SerializeField]
    
    private float height;
    private BoxCollider2D collider;

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.2f);
        Transform T = GetComponent<Transform>();
        Gizmos.DrawCube(new Vector2(magneticBase.x, magneticBase.y - magneticHeightScope/2), 
            new Vector3(magneticWidthScope, magneticHeightScope, 1));
    }
    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        
        collider.offset = new Vector2(magneticBase.x, magneticBase.y - magneticHeightScope/2);
        collider.size = new Vector2( Mathf.Abs(magneticWidthScope), Mathf.Abs(magneticHeightScope));
        collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */

    private GameObject[] levels;

    private GameObject PLAYER1;
    private GameObject PLAYER2;

    void Awake()
    {
#if !DEVELOPMENT_BUILD
        currentLevel = 0;
#endif

        levels = GameObject.FindGameObjectsWithTag("Level");
        levels = levels.OrderBy(obj => obj.name, new AlphanumComparatorFast()).ToArray();

        Debug.Log("levels: " + levels.Length);

        GameObject[] pc = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < pc.Length; i++)
        {
            if (pc[i].GetComponent<PlayerController>().PlayerNumber == PlayerController.PlayerNum.P1)
            {
                PLAYER1 = pc[i];
            } 
            else if (pc[i].GetComponent<PlayerController>().PlayerNumber == PlayerController.PlayerNum.P2)
            {
                PLAYER2 = pc[i];
            } 
        }
    }

    void Start()
    {
        levels[currentLevel].GetComponent<AbstractLevel>().ActiveMe();
        SetCameraToLevel();
    }

    void LateUpdate()
    {
        if(!PLAYER1.activeInHierarchy && !PLAYER2.activeInHierarchy)
        {
            Debug.Log("currentLevel: " + currentLevel);
            levels[currentLevel].GetComponent<AbstractLevel>().DesactiveMe();
            currentLevel = currentLevel+1;
            levels[currentLevel].GetComponent<AbstractLevel>().ActiveMe();

            PLAYER1.SetActive(true);
            PLAYER2.SetActive(true);

            SetCameraToLevel();
        }
    }

    void SetCameraToLevel()
    {
        transform.position = new Vector3(
            levels[currentLevel].transform.position.x,
            levels[currentLevel].transform.position.y,
            transform.position.z);
    }
}
