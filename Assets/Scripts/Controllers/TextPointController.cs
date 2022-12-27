using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPointController : MonoBehaviour
{
    public GameObject PlayerObject;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(PlayerObject.transform.position.x, transform.position.y,  PlayerObject.transform.position.z);
    }



}
