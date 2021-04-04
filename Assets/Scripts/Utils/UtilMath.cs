using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilMath : MonoBehaviour
{
    public static int nfmod(int a,int b)
    {
        return (a%b + b)%b;
    }
}