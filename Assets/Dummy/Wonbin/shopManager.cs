using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopManager: MonoBehaviour
{
    
    float distance = 10;
    public GameObject productImage;
    public GameObject product;

    public GameObject shopButton;
    public GameObject shopCloseButton;
    public GameObject animalShopButton;
    public GameObject farmObShopButton;

    public GameObject OKButton;
    public GameObject cancelButton;

    public GameObject shop;
    public GameObject animalshop;
    public GameObject farmObshop;
    //public GameObject productAnimal;
    void Start()
    {
        
    }

    public void openshop()
    {
        shop.gameObject.SetActive(true);
        farmObshop.gameObject.SetActive(false);
        animalshop.gameObject.SetActive(true);
        shopButton.gameObject.SetActive(false);
        shopCloseButton.gameObject.SetActive(true);
        animalShopButton.gameObject.SetActive(true);
        farmObShopButton.gameObject.SetActive(true);
    }

    public void openanimalshop()
    {
        shop.gameObject.SetActive(true);
        animalshop.gameObject.SetActive(true);
        farmObshop.gameObject.SetActive(false);
    }

    public void openfarmObshop()
    {
        shop.gameObject.SetActive(true);
        animalshop.gameObject.SetActive(false);
        farmObshop.gameObject.SetActive(true);
    }

    public void closeshop()
    {
        shop.gameObject.SetActive(false);
        shopButton.gameObject.SetActive(true);
        shopCloseButton.gameObject.SetActive(false);
        animalShopButton.gameObject.SetActive(false);
        farmObShopButton.gameObject.SetActive(false);
    }


    public void buy()
    {
        Destroy(productImage);
        Instantiate(productImage, new Vector2(0, -5.5f), Quaternion.identity);
        OKButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
    }

    public void cancel()
    {
        Destroy(productImage);
        OKButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
    }

    public void OK()
    {

        Instantiate(product, new Vector2(product.transform.position.x, product.transform.position.y), Quaternion.identity);
        Destroy(productImage);
        OKButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
    }


}
