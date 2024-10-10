using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAegyo : MonoBehaviour
{
    [SerializeField] CatController cat;

    public void OnButton()
    {
        cat.UseAegyo();
    }
}
