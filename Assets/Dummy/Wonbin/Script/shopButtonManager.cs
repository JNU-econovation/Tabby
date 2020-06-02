using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonManager: MonoBehaviour
{

    public static Animal shopAnimal;
    
    //Drag PIDrag;

    public GameObject farmAnimal;

    public GameObject product;
    private GameObject producted;
    public GameObject productImage;

    private Image image;
    public GameObject AnimalforList;


    private GameObject gameManager;


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
        gameManager = GameObject.Find("GameManager");
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
        product = gameObject.GetComponent<ShopButtonManager>().product;
        print(product);
        ShopButtonManager productImgSBM = productImage.GetComponent<ShopButtonManager>();
        productImgSBM.product = product;
        text.gameObject.SetActive(true);
        productImage = GameObject.Find("arrangeImage");
        image = productImage.GetComponent<Image>();
        shopAnimal = gameObject.GetComponent<Animal>();
        image.sprite = shopAnimal.babyAnimalSprite;
        Animal productAnimal = productImage.GetComponent<Animal>();
        productAnimal.animalNumber = shopAnimal.animalNumber;
        productAnimal.animalCost = shopAnimal.animalCost;
        Drag PIDrag = productImage.GetComponent<Drag>();
        PIDrag.PItransformMid();
        OKButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
        print(shopAnimal);
    }

    public void FOTagging()
    {
        productImage = GameObject.Find("arrangeImage");
        productImage.gameObject.tag = "farmObject";
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

    public void OK()
    {
    
        text.gameObject.SetActive(false);
        productImage = GameObject.Find("arrangeImage");
        ShopButtonManager productSBM = productImage.GetComponent<ShopButtonManager>();
        product = productSBM.product;
        Animal productAnimal = productImage.GetComponent<Animal>();
        
        producted = Instantiate(product, new Vector2(productImage.transform.position.x, productImage.transform.position.y), Quaternion.identity);
        producted.transform.parent = farmAnimal.transform;
        Drag PIDrag = productImage.GetComponent<Drag>();
        PIDrag.PItransformBack();
        OKButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        productImage.gameObject.tag = "Untagged";
        MoneyManager.money -= productAnimal.animalCost;
        print(product + "www");
        print(shopAnimal + "dddd");

        AnimalManager.AddNewAnimal(shopAnimal);
        //if (animalshop.activeSelf == true)
        //    AnimalManager.AnimalListAdd(producted, AnimalforList, AnimalforList.gameObject.name);

    }





}
