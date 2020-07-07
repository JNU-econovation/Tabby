using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;

public class ShopButtonManager : MonoBehaviour
{

    public Animal shopAnimal;
    public FarmObject shopFarmObject;

    //Drag PIDrag;

    public GameObject farmAnimal;
    public GameObject farmFarmObject;

    public GameObject product;
    private GameObject producted;
    public GameObject productImage;

    private Image image;
    public GameObject AnimalforList;

    public GameObject Animals;
    
    public GameObject[] shopAnimalProducts;
    public GameObject[] shopFarmObjectProducts;


    public string PSN;//productSourceName

    public GameObject shopButton;
    public GameObject shopCloseButton;
    public GameObject animalShopButton;
    public GameObject farmObShopButton;
    public GameObject text;


    public GameObject mapButton;

    public GameObject tapZone;

    public GameObject OKButton;
    public GameObject cancelButton;

    public GameObject shop;
    public GameObject animalshop;
    public GameObject farmObshop;


    void Start()
    {

    }

    public void openShop()
    {
        MoneyManager.money -= 1;
        shop.gameObject.SetActive(true);
        mapButton.gameObject.SetActive(false);
        tapZone.gameObject.SetActive(false);
        farmObshop.gameObject.SetActive(false);
        animalshop.gameObject.SetActive(true);
        shopButton.gameObject.SetActive(false);
        shopCloseButton.gameObject.SetActive(true);
        animalShopButton.gameObject.SetActive(true);
        farmObShopButton.gameObject.SetActive(true);
    }

    public void openAnimalShop()
    {
        if (farmObshop.activeSelf == true)
            cancel();
        shop.gameObject.SetActive(true);
        animalshop.gameObject.SetActive(true);
        farmObshop.gameObject.SetActive(false);
    }

    public void openFarmObjectShop()
    {
        shop.gameObject.SetActive(true);
        animalshop.gameObject.SetActive(false);
        farmObshop.gameObject.SetActive(true);
    }

    public void closeShop()
    {
        shop.gameObject.SetActive(false);
        shopButton.gameObject.SetActive(true);
        shopCloseButton.gameObject.SetActive(false);
        animalShopButton.gameObject.SetActive(false);
        farmObShopButton.gameObject.SetActive(false);
        mapButton.gameObject.SetActive(true);
        //tapZone.gameObject.SetActive(true);
    }


    public void buy()
    {
        if (animalshop.activeSelf == true)
            AnimalBuy();
        else
            FarmObjectBuy();
    }

    void AnimalBuy()
    {
        productImage = GameObject.Find("arrangeImage");
        product = gameObject.GetComponent<ShopButtonManager>().product;
        ShopButtonManager productImgSBM = productImage.GetComponent<ShopButtonManager>();
        productImgSBM.product = product;
        text.gameObject.SetActive(true);
        image = productImage.GetComponent<Image>();
        shopAnimal = gameObject.GetComponent<Animal>();
        image.sprite = shopAnimal.animalSprite;
        Animal productAnimal = productImage.GetComponent<Animal>();
        productAnimal.animalNumber = shopAnimal.animalNumber;
        productAnimal.animalCost = shopAnimal.animalCost;
        Drag PIDrag = productImage.GetComponent<Drag>();
        PIDrag.PItransformMid();
        OKButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);

    }

    void FarmObjectBuy()

    {
        productImage = GameObject.Find("arrangeImage");
        product = gameObject.GetComponent<ShopButtonManager>().product;
        ShopButtonManager productImgSBM = productImage.GetComponent<ShopButtonManager>();
        productImgSBM.product = product;
        image = productImage.GetComponent<Image>();
        shopFarmObject = gameObject.GetComponent<FarmObject>();
        image.sprite = shopFarmObject.farmObjectSprite;
        FarmObject productFarmObject = productImage.GetComponent<FarmObject>();
        productFarmObject.farmObjectNumber = shopFarmObject.farmObjectNumber;
        productFarmObject.shopCost = shopFarmObject.shopCost;
        Drag PIDrag = productImage.GetComponent<Drag>();
        PIDrag.PItransformMid();
        OKButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
    }



    public void cancel()
    {
        text.gameObject.SetActive(false);
        productImage = GameObject.Find("arrangeImage");
        Drag PIDrag = productImage.GetComponent<Drag>();
        PIDrag.PItransformBack();
        OKButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
    }

    public void PressOK()
    {

        if (animalshop.activeSelf == true)
            AnimalOK();
        else
            FarmObjectOK();

    }

    void AnimalOK()
    {
        text.gameObject.SetActive(false);
        productImage = GameObject.Find("arrangeImage");
        ShopButtonManager productSBM = productImage.GetComponent<ShopButtonManager>();
        product = productSBM.product;
        Animal productAnimal = product.GetComponent<Animal>();

        producted = Instantiate(shopAnimalProducts[productAnimal.animalNumber], new Vector2(productImage.transform.position.x, productImage.transform.position.y), Quaternion.identity);
        producted.transform.parent = farmAnimal.transform;
        Drag PIDrag = productImage.GetComponent<Drag>();
        PIDrag.PItransformBack();

        OKButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        MoneyManager.money -= productAnimal.animalCost;

        Spawner.AddNewAnimal(producted);

        DataManager._instance.ParseAnimalDate(Spawner.animals);

    }

    void FarmObjectOK()
    {
        text.gameObject.SetActive(false);
        productImage = GameObject.Find("arrangeImage");
        ShopButtonManager productSBM = productImage.GetComponent<ShopButtonManager>();
        product = productSBM.product;
        FarmObject productFarmObject = product.GetComponent<FarmObject>();

        producted = Instantiate(productSBM.product, new Vector2(productImage.transform.position.x, productImage.transform.position.y), Quaternion.identity);
        producted.transform.parent = farmFarmObject.transform;
        Drag PIDrag = productImage.GetComponent<Drag>();
        PIDrag.PItransformBack();

        
        OKButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        MoneyManager.money -= productFarmObject.shopCost;

        Spawner.BuyNewFarmObject(producted);
        DataManager._instance.ParseFarmObjectData(Spawner.farmObjects);

        for (int i = 0; i < farmAnimal.transform.childCount; i++)
        {
            AnimalController animalController = farmAnimal.transform.GetChild(i).GetComponent<AnimalController>();
            animalController.pathStart();
        }
    }






}
