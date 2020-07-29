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

    public static float shopTime;

    public GameObject mapButton;

    public GameObject tapZone;

    public GameObject OKButton;
    public GameObject cancelButton;

    public GameObject shop;
    public GameObject farmObshop;


    void Start()
    {
    }

    private void Update()
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
        Debug.Log("꺼짐");
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
        FarmObject productFarmObject = product.GetComponent<FarmObject>();
        FarmObject shopFarmObject;
        shopFarmObject = gameObject.GetComponent<FarmObject>();
        Debug.Log(shopFarmObject.shopCost);
        Debug.Log(DataManager._instance.playerData.money);
        if (shopFarmObject.shopCost < DataManager._instance.playerData.money)
        {
            Debug.Log("실행됨");
            productImage = GameObject.Find("arrangeImage");
            product = gameObject.GetComponent<ShopButtonManager>().product;
            ShopButtonManager productImgSBM = productImage.GetComponent<ShopButtonManager>();
            productImgSBM.product = product;
            image = productImage.GetComponent<Image>();
            image.sprite = shopFarmObject.farmObjectSprite;
            FarmObject productImageFarmObject = productImage.GetComponent<FarmObject>();
            productImageFarmObject.farmObjectNumber = shopFarmObject.farmObjectNumber;
            productImageFarmObject.shopCost = shopFarmObject.shopCost;
            Drag PIDrag = productImage.GetComponent<Drag>();
            PIDrag.PItransformMid();
            OKButton.gameObject.SetActive(true);
            cancelButton.gameObject.SetActive(true);
            shopList.SetActive(false);
            
        }

    }




    public void cancel()
    {
            text.gameObject.SetActive(false);
            productImage = GameObject.Find("arrangeImage");
            Drag PIDrag = productImage.GetComponent<Drag>();
            PIDrag.PItransformBack();
            OKButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);
            Debug.Log(gameObject.name);
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
            //Debug.Log("동물" + i + "번 : " + farmAnimal.transform.GetChild(i).transform.position.x);
            //Debug.Log("productImage : " + productImage.transform.position.x);
            //Debug.Log(i+"번 차이 : "+Mathf.Abs(farmAnimal.transform.GetChild(i).transform.position.x - productImage.transform.position.x));
            //Debug.Log(i+"번 차이 : "+ Mathf.Abs(farmAnimal.transform.GetChild(i).transform.position.y - productImage.transform.position.y));
            if (Mathf.Abs(farmAnimal.transform.GetChild(i).transform.position.x - productImage.transform.position.x) > 3 || Mathf.Abs(farmAnimal.transform.GetChild(i).transform.position.y - productImage.transform.position.y) > 3) {
                
                putable++; }
        }
        for (int i = 0; i < farmFarmObject.transform.childCount; i++)
        {
            //Debug.Log("설치물" + i + "번 : " + farmFarmObject.transform.GetChild(i).transform.position.x);
            //Debug.Log("productImage : " + productImage.transform.position.x);
            //Debug.Log(i + "번 차이 : " + Mathf.Abs(farmFarmObject.transform.GetChild(i).transform.position.x - productImage.transform.position.x));
            //Debug.Log(i + "번 차이 : " + Mathf.Abs(farmFarmObject.transform.GetChild(i).transform.position.y - productImage.transform.position.y));
            if (Mathf.Abs(farmFarmObject.transform.GetChild(i).transform.position.x - productImage.transform.position.x) > 3 || Mathf.Abs(farmFarmObject.transform.GetChild(i).transform.position.y - productImage.transform.position.y) > 3)
            {

                putable++;
            }
        }
        //Debug.Log("농장동물"+farmAnimal.transform.childCount+"마리");
        //Debug.Log("농장설치물"+farmFarmObject.transform.childCount+"개");
        //Debug.Log(putable);
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
            MoneyManager.money -= productedObject.shopCost;
            DataManager._instance.SaveMoney(MoneyManager.money, MoneyManager.heart);

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
