using UnityEngine;

public class Food : MonoBehaviour
{
    public GameObject foodPrefab; // 생성할 음식 프리팹
    public Transform spawnPoint; // 음식이 생성될 위치

    private GameObject currentFood; // 현재 생성된 음식 오브젝트

    public void OnButtonClick(CatController cat)
    {
        // 음식 생성
        CreateFood(cat);
    }

    private void CreateFood(CatController cat)
    {
        // 음식 오브젝트 생성, spawnPoint 위치를 사용
        if (spawnPoint != null)
        {
            currentFood = Instantiate(foodPrefab, spawnPoint.position, spawnPoint.rotation);

            if(cat != null)
            {
                cat.SetTargetFood(currentFood.transform);
            }
        }
    }
}