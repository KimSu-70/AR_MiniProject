using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TouchDetection : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hit = new List<ARRaycastHit>();

    private bool Touched = false;
    private GameObject SelectedObj;

    private void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        // ��ġ �Է��� Ȯ��
        if (Input.touchCount > 0)
        {
            HandleTouch(Input.GetTouch(0));
        }
    }

    private void HandleTouch(Touch touch)
    {
        // ��ġ�� ���۵� ���
        if (touch.phase == TouchPhase.Began)
        {
            ProcessTouch(touch.position);
        }
        // ��ġ�� �̵� ���� ���
        else if (touch.phase == TouchPhase.Moved && Touched)
        {
            MoveSelectedObject(touch.position);
        }
        // ��ġ�� ���� ���
        else if (touch.phase == TouchPhase.Ended)
        {
            Touched = false; // �巡�� ����
            SelectedObj = null; // ���õ� ������Ʈ ����
        }
    }

    private void ProcessTouch(Vector2 touchPosition)
    {
        // ��ġ�� ȭ�� ��ǥ�� ��ũ�� ����Ʈ�� ��ȯ�Ͽ� ���� ����
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);

        // ���̸� ����Ͽ� �浹ü ����
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            HandleHit(hit);
        }
    }

    private void HandleHit(RaycastHit hit)
    {
        // �±׸� ����Ͽ� ����� Ȯ��
        if (hit.collider.CompareTag("Cat"))
        {
            CatController cat = hit.collider.GetComponent<CatController>();
            if (cat != null)
            {
                cat.Clicks();
            }
        }
        else if (hit.collider.CompareTag("Ball"))
        {
            SelectedObj = hit.collider.gameObject;
            Touched = true;
        }
    }

    private void MoveSelectedObject(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // ���õ� ������Ʈ�� ��ġ�� ��ġ ��ġ�� ������Ʈ
            // ī�޶���� �Ÿ� ����
            Vector3 newPosition = hit.point;
            newPosition.y = SelectedObj.transform.position.y; // Y ��ġ ����

            // �ε巴�� �̵��ϱ� ���� Lerp ���
            SelectedObj.transform.position = Vector3.Lerp(SelectedObj.transform.position, newPosition, Time.deltaTime * 10);
        }
    }
}