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
        // 터치 입력을 확인
        if (Input.touchCount > 0)
        {
            HandleTouch(Input.GetTouch(0));
        }
    }

    private void HandleTouch(Touch touch)
    {
        // 터치가 시작된 경우
        if (touch.phase == TouchPhase.Began)
        {
            ProcessTouch(touch.position);
        }
        // 터치가 이동 중인 경우
        else if (touch.phase == TouchPhase.Moved && Touched)
        {
            MoveSelectedObject(touch.position);
        }
        // 터치가 끝난 경우
        else if (touch.phase == TouchPhase.Ended)
        {
            Touched = false; // 드래그 종료
            SelectedObj = null; // 선택된 오브젝트 해제
        }
    }

    private void ProcessTouch(Vector2 touchPosition)
    {
        // 터치된 화면 좌표를 스크린 포인트로 변환하여 레이 생성
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);

        // 레이를 사용하여 충돌체 감지
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            HandleHit(hit);
        }
    }

    private void HandleHit(RaycastHit hit)
    {
        // 태그를 사용하여 고양이 확인
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
            // 선택된 오브젝트의 위치를 터치 위치로 업데이트
            // 카메라와의 거리 유지
            Vector3 newPosition = hit.point;
            newPosition.y = SelectedObj.transform.position.y; // Y 위치 유지

            // 부드럽게 이동하기 위해 Lerp 사용
            SelectedObj.transform.position = Vector3.Lerp(SelectedObj.transform.position, newPosition, Time.deltaTime * 10);
        }
    }
}