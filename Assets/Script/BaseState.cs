using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : MonoBehaviour
{
    public virtual void Enter() { }      // ���� ���� ��
    public virtual void Update() { }     // ���� ���� ��
    public virtual void Exit() { }       // ���������� ��
}
