using Assets.Scripts.Data.Enums;
using Assets.Scripts.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNPCController : MonoBehaviour
{

    private NavMeshAgent myNavAgent; 
    private float StuckCountDown = 1f;
    private Vector3 TargetPos; 
    NpcStatusEnum npcStatus = NpcStatusEnum.Ready;
    public GameObject Crown;
    public GameObject CashDeskPosObject;

    enum NpcStatusEnum{
        Ready,
        OnMyWay
    }

    void Start()
    {
        myNavAgent = transform.GetComponent<NavMeshAgent>();       
        myNavAgent.speed = transform.GetComponent<PlayerModel>().PlayerSpeed;
        SetMyDestination(RandomNavmeshLocation());
    }

    void Update()
    {
        if (GameManager.pausePlay == PausePlayEnum.Play)
        {
            if (ScoreManager.Instance.levelStatus == LevelStatusEnum.Ready)
            {

            }
            else if (ScoreManager.Instance.levelStatus == LevelStatusEnum.Continues)
            {
                Continues_Jobs();
            }
            else
            {
                Stop_Jobs();
            }
        }
        else
        {
            // Pause...
        }
    }    


    private void Continues_Jobs()
    {
        float dis = Vector3.Distance(transform.position, TargetPos);
        if (dis < 5.3f)
        {
          // transform.LookAt(transform.position, TargetPos);
            npcStatus = NpcStatusEnum.Ready;
            Countdown();
        }
    }

    private void Stop_Jobs()
    {
        myNavAgent.isStopped = true; 
        myNavAgent.velocity = Vector3.zero;
    }



    public Vector3 RandomNavmeshLocation()
    {
        float radius = 40f;
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }    
        return finalPosition;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(Constant.TAG_SHELF) && npcStatus == NpcStatusEnum.Ready)
        {
            if (!other.gameObject.GetComponent<ShelfModel>().IsEmpty)
            {
                SetMyDestination(other.gameObject.transform.position);
            }
        }
    }

    private void SetMyDestination(Vector3 toTarget)
    {
        if (ScoreManager.Instance.levelStatus == LevelStatusEnum.Continues)
        {
            TargetPos = toTarget;
            myNavAgent.SetDestination(toTarget);
            npcStatus = NpcStatusEnum.OnMyWay;
        }       
    }

    private void Countdown()
    {
        if (StuckCountDown >= 0)
        {
            StuckCountDown -= Time.deltaTime;
        }
        else
        {
            SetMyDestination(RandomNavmeshLocation());
            StuckCountDown = 1f;
        }
    }

    public void SetCrownVisibility(bool visiblityStatu)
    {
        Crown.SetActive(visiblityStatu);
    }

       

    public void ToCashDesk()
    {       
        SetMyDestination(GetNearCashDeskPosition());
    }

    public void CashDeskDone()
    {
        npcStatus = NpcStatusEnum.Ready;
        RandomNavmeshLocation();
    }




    public Vector3 GetNearCashDeskPosition()
    {       
        float current;
        float nearest = 99999;
        int nearestIndex = 0;

        for (int i = 0; i < CashDeskPosObject.transform.childCount; i++)
        {
            current = Vector3.Distance(transform.position, CashDeskPosObject.transform.GetChild(i).position);
            if (current < nearest)
            {
                nearest = current;
                nearestIndex = i;
            }  
        }
        return CashDeskPosObject.transform.GetChild(nearestIndex).transform.GetChild(0).position;
    }






}
