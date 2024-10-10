using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Food food; // Food 스크립트 참조
    public CatController cat; // CatController 스크립트 참조

    public void OnFoodButtonClick()
    {
        food.OnButtonClick(cat);
    }
}
