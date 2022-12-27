using Assets.Scripts.Data.Enums;
using UnityEngine;
using Assets.Scripts.General;
using UnityEngine.UI;
using Assets.Scripts.Controllers;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Camera main_camera;
    public GameObject Crown;


    #region DirectionController Variables
    CharacterController characterController;   
    protected Joystick joystick;
    #endregion
       
    public PlayerStatusEnum playerStatus = PlayerStatusEnum.Available;
   

    private void Awake()
    {

    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        joystick = FindObjectOfType<Joystick>();        
    }

    void Update()
    {
        if (GameManager.pausePlay == PausePlayEnum.Play)
        {         
            if (ScoreManager.Instance.levelStatus == LevelStatusEnum.Ready)
            {
                ReadyContinues_Jobs();               
            }
            else if (ScoreManager.Instance.levelStatus == LevelStatusEnum.Continues)
            {
                ReadyContinues_Jobs();               
            }
            else
            {
                Finish_Jobs();
            }
        }           
        else
        {
            // Pause...
        }
    }

    void SetCameraPosition()
    {
        main_camera.transform.position = new Vector3(transform.position.x, main_camera.transform.position.y, transform.position.z - 15.8f);
    }   
    
    private void ReadyContinues_Jobs()
    {
        SetCameraPosition();

        if (playerStatus == PlayerStatusEnum.Available)
        {
            TouchListener();
        }
        else
        {
            //Unavailable
        }        
    }    

    private void Finish_Jobs()
    {
        //LevelStatusEnum.Finished           
        this.Wait(0.1f, () =>
        {
            //print("ToFinalStage");
            ToFinalPos();
        });
    }

    public void TouchListener()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    break;
                case TouchPhase.Moved:
                    PlayerMove();
                    break;
                case TouchPhase.Ended:
                    break;
                case TouchPhase.Stationary:
                    PlayerMove();                  
                    break;
            }
        }
    }
       
    public void PlayerMove()
    {
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(joystick.Horizontal * Constant.PlayerRunSpeed, rb.velocity.y, joystick.Vertical * Constant.PlayerRunSpeed);
        //rb.rotation = Quaternion.LookRotation(rb.velocity);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rb.velocity), Time.deltaTime * Constant.PlayerRunRotation);
    }
   
    void ToFinalPos()
    {
        transform.position = new Vector3(230.77f, 2.319f, 243.27f);
        transform.rotation = new Quaternion(transform.rotation.x, 172.8f, transform.rotation.z, 0);

        GetComponentInChildren<AvatarController>().OutBasketObject();

    }


    public void SetCrownVisibility(bool visiblityStatu)
    {
        Crown.SetActive(visiblityStatu);
    }







}
