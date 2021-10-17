using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINonRotate : MonoBehaviour
{
    private Quaternion rotation;
 
    void Awake()
    {
        rotation = transform.rotation;
    }
     
    void LateUpdate () 
    {
        transform.rotation = rotation;    
    }
}
