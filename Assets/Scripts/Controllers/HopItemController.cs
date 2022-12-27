using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopItemController : MonoBehaviour
{
    private bool IsFollow = false;
    private GameObject FollowObject;

    void Start()
    {
        
    }

    void Update()
    {
        if (IsFollow)
        {
            Following();
        }        
    }


    
    private void Following()
    {
        float step = Constant.HopItemSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, FollowObject.transform.position, step);
    }

    public void FollowBasket(GameObject targetObject)
    {
        FollowObject = targetObject;
        IsFollow = true;
    }

    public void UnFollowBasket()
    {
        IsFollow = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constant.TAG_FOLLOW_OBJECT))
        {
            Destroy(transform.gameObject);
        }
    }


  


}
