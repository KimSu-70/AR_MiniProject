using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sing : MonoBehaviour
{
    [SerializeField] CatController cat;

    public void OnButton()
    {
        cat.Sing();
    }
}
