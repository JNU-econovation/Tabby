using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;

public class ShopButtonManager : MonoBehaviour
{
    public GameObject farmAnimal;
    public GameObject farmFarmObject;

    public GameObject product;
    private GameObject producted;
    public GameObject productImage;

    public GameObject shopList;
    public GameObject arrangeButtons;

    private Image image;
    
    public GameObject[] shopFarmObjectProducts;

    public string PSN;//productSourceName

    public GameObject shopButton;
    public GameObject shopCloseButton;
    public GameObject farmObShopButton;

    public GameObject inventoryButton;
    public GameObject putInvenButton;
    public GameObject text;


    public GameObject mapButton;

    public GameObject tapZone;

    public GameObject OKButton;
    public GameObject cancelButton;

    public GameObject shop;
    public GameObject farmObshop;


    void Start()
    {

    }

    public void openShop()
    {
        
        shop.gameObject.SetActive(true);
        mapButton.gameObject.SetActive(false);
        tapZone.gameObject.SetActive(false);
        shopButton.gameObject.SetActive(false);
        shopCloseButton.gameObject.SetActive(true);
        inventoryButton.SetActive(false);
        putInvenButton.SetActive(false);
    }

    public void closeShop()
    {
        shop.gameObject.SetActive(false);
        shopButton.gameObject.SetActive(true);
        shopCloseButton.gameObject.SetActive(false);
        mapButton.gameObject.SetActive(true);
        inventoryButton.SetActive(true);
        tapZone.gameObject.SetActive(true);
    }


    public void buy()
    {
        FarmObjectBuy();
    }

    void FarmObjectBuy()

    {
        productImage = GameObject.Find("arrangeImage");
        product = gameObject.GetComponent<ShopButtonManager>().product;
        ShopButtonManager productImgSBM = productImage.GetComponent<ShopButtonManager>();
        productImgSBM.product = product;
        image = productImage.GetComponent<Image>();
        FarmObject shopFarmObject;
        shopFarmObject = gameObject.GetComponent<FarmObject>();
        image.sprite = shopFarmObject.farmObjectSprite;
        FarmObject productFarmObject = productImage.GetComponent<FarmObject>();
        productFarmObject.farmObjectNumber = shopFarmObject.farmObjectNumber;
        productFarmObject.shopCost = shopFarmObject.shopCost;
        Drag PIDrag = productImage.GetComponent<Drag>();
        PIDrag.PItransformMid();
        OKButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
        shopList.SetActive(false);
    }



    public void cancel()
    {
        text.gameObject.SetActive(false);
        productImage = GameObject.Find("arrangeImage");
        Drag PIDrag = productImage.GetComponent<Drag>();
        PIDrag.PItransformBack();
        OKButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        shopList.SetActive(true);
    }

    public void PressOK()
    { 
        FarmObjectOK();
        shopList.SetActive(true);
    }

    void FarmObjectOK()
    {
        shopList.SetActive(true);
        productImage = GameObject.Find("arrangeImage");
        int putable = 0;
        for (int i = 0; i < farmAnimal.transform.childCount; i++)
        {
            if (Mathf.Abs(farmAnimal.transform.GetChild(i).transform.position.x - productImage.transform.position.x) > 0.5 && Mathf.Abs(farmAnimal.transform.GetChild(i).transform.position.y - productImage.transform.position.y) > 0.5)
                putable++;
        }
        for (int i = 0; i < farmFarmObject.transform.childCount; i++)
        {
            if (Mathf.Abs(farmFarmObject.transform.GetChild(i).transform.position.x - productImage.transform.position.x) > 0.5 && Mathf.Abs(farmFarmObject.transform.GetChild(i).transform.position.y - productImage.transform.position.y) > 0.5)
                putable++;
        }
        Debug.Log(putable);
        if (putable == farmAnimal.transform.childCount + farmFarmObject.transform.childCount)
        {
            text.gameObject.SetActive(false);
            ShopButtonManager productSBM = productImage.GetComponent<ShopButtonManager>();
            product = productSBM.product;
            FarmObject productFarmObject = product.GetComponent<FarmObject>();

            producted = Instantiate(productSBM.product, new Vector2(productImage.transform.position.x, productImage.transform.position.y), Quaternion.identity);
            producted.transform.parent = farmFarmObject.transform;
            Drag PIDrag = productImage.GetComponent<Drag>();
            PIDrag.PItransformBack();
            FarmObject productedObject = producted.GetComponent<FarmObject>();
            productedObject.farmObjectIndex = Spawner._instance.farmObjects.Count;
            productedObject.isField = true;


            OKButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);
            MoneyManager.money -= productFarmObject.shopCost;

            Spawner._instance.BuyNewFarmObject(producted);
            DataManager._instance.ParseFarmObjectData(Spawner._instance.farmObjects);

            for (int i = 0; i < farmAnimal.transform.childCount; i++)
            {
                AnimalController animalController = farmAnimal.transform.GetChild(i).GetComponent<AnimalController>();
                animalController.pathStart();
            }
        }
        
    }






}
