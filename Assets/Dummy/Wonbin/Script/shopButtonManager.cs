using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopButtonManager: MonoBehaviour
{
    
    float distance = 10;

    public int shopCost;

    //Drag PIDrag;

    private GameObject productImage;
    public GameObject product;
    private GameObject producted;
    public Sprite productSprite;
    private SpriteRenderer spriteRenderer;
    public GameObject AnimalforList;


    private GameObject gameManager;


    public string PSN;//productSourceName

    public GameObject shopButton;
    public GameObject shopCloseButton;
    public GameObject animalShopButton;
    public GameObject farmObShopButton;
    public GameObject text;

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

    void openShop()
    {
        MoneyManager.money -= 1;
        shop.gameObject.SetActive(true);
        tapZone.gameObject.SetActive(false);
        farmObshop.gameObject.SetActive(false);
        animalshop.gameObject.SetActive(true);
        shopButton.gameObject.SetActive(false);
        shopCloseButton.gameObject.SetActive(true);
        animalShopButton.gameObject.SetActive(true);
        farmObShopButton.gameObject.SetActive(true);
    }

    void openAnimalShop()
    {
        if (farmObshop.activeSelf == true)
            cancel();
        shop.gameObject.SetActive(true);
        animalshop.gameObject.SetActive(true);
        farmObshop.gameObject.SetActive(false);
    }

    void openFarmObjectShop()
    {
        shop.gameObject.SetActive(true);
        animalshop.gameObject.SetActive(false);
        farmObshop.gameObject.SetActive(true);
    }

    void closeShop()
    {
        shop.gameObject.SetActive(false);
        shopButton.gameObject.SetActive(true);
        shopCloseButton.gameObject.SetActive(false);
        animalShopButton.gameObject.SetActive(false);
        farmObShopButton.gameObject.SetActive(false);
        //tapZone.gameObject.SetActive(true);
    }


    void buy()
    {
        text.gameObject.SetActive(true);
        productImage = GameObject.Find("arrangeImage");
        spriteRenderer = productImage.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = productSprite;
        //productImage.GetComponent<Image>().sprite = Resources.Load(PSN, typeof(Sprite)) as Sprite;
        shopButtonManager productSBM = productImage.GetComponent<shopButtonManager>();
        productSBM.product = product;
        productSBM.shopCost = shopCost;
        Drag PIDrag = productImage.GetComponent<Drag>();
        PIDrag.PItransformMid();
        OKButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
    }

    void FOTagging()
    {
        productImage = GameObject.Find("arrangeImage");
        productImage.gameObject.tag = "farmObject";
    }

    void cancel()
    {
        text.gameObject.SetActive(false);
        productImage = GameObject.Find("arrangeImage");
        Drag PIDrag = productImage.GetComponent<Drag>();
        PIDrag.PItransformBack();
        OKButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
    }

    void OK()
    {
        text.gameObject.SetActive(false);
        productImage = GameObject.Find("arrangeImage");
        shopButtonManager productSBM = productImage.GetComponent<shopButtonManager>();
        //this.product = productSBM.product;
        producted = Instantiate(productSBM.product, new Vector2(productImage.transform.position.x, productImage.transform.position.y), Quaternion.identity);
        producted.transform.parent = gameManager.transform;
        Drag PIDrag = productImage.GetComponent<Drag>();
        PIDrag.PItransformBack();
        OKButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        productImage.gameObject.tag = "Untagged";
        MoneyManager.money -= productSBM.shopCost;

        //if (animalshop.activeSelf == true)
        //    AnimalManager.AnimalListAdd(producted, AnimalforList, AnimalforList.gameObject.name);

    }





}
