using Assets.Scripts.Data.Enums;
using Assets.Scripts.Data.Models;
using UnityEngine;
using UnityEngine.UI;

public class ShelfController : MonoBehaviour
{
    private ShelfModel shelfModel;
    public GameObject EmptyShelf, FullShelf, HopObject;

    private void Awake()
    {
    }

    void Start()
    {
        shelfModel = transform.GetComponent<ShelfModel>();
    }

    void Update()
    {
        if (shelfModel.IsEmpty)
        {
            FullShelf.SetActive(false);
        }
        else
        {
            FullShelf.SetActive(true);
        }
    }          


    private void SetShelfMaterial(Material material)
    {
        switch (shelfModel.ShelfType)
        {
            case ShelfTypeEnum.Shelf:
                for (int i = 1; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<MeshRenderer>().material = material;
                }
                break;
            case ShelfTypeEnum.Grocery:
                transform.GetComponent<MeshRenderer>().material = material;
                break;
            case ShelfTypeEnum.Alcohol:
                transform.GetComponent<MeshRenderer>().material = material;
                break;
            case ShelfTypeEnum.Fridge:
                transform.GetComponent<MeshRenderer>().material = material;
                break;
            case ShelfTypeEnum.Freeze:
                transform.GetComponent<MeshRenderer>().material = material;

                break;
            default:
                break;
        }
    }

  
    public void PlayHopAnimation(Transform targetTransform)
    {
        HopObject.SetActive(true);       

        for (int i = 0; i < HopObject.transform.childCount; i++)
        {
            HopObject.transform.GetChild(i).transform.GetComponent<HopItemController>().FollowBasket(targetTransform.gameObject);
        }
    }






}




