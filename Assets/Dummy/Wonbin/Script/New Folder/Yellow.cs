using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow : Animal
{
    public Sprite babyAnimalSprite;
    public Sprite growUpSprite;

    void Start()
    {
        animalNumber = 2;
        spriteRenderer.sprite = babyAnimalSprite;
    }

    void growup()
    {
        spriteRenderer.sprite = growUpSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
