using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//AnimalAnimator는 PathFinder 스크립트에 붙어있음
public class Animator : MonoBehaviour
{
    public void ChangeSprite(SpriteRenderer spriteRenderer, Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
