using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Food food; // Food ��ũ��Ʈ ����
    public CatController cat; // CatController ��ũ��Ʈ ����

    public void OnFoodButtonClick()
    {
        food.OnButtonClick(cat);
    }
}
