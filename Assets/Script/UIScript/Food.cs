using UnityEngine;

public class Food : MonoBehaviour
{
    public GameObject foodPrefab; // ������ ���� ������
    public Transform spawnPoint; // ������ ������ ��ġ

    private GameObject currentFood; // ���� ������ ���� ������Ʈ

    public void OnButtonClick(CatController cat)
    {
        // ���� ����
        CreateFood(cat);
    }

    private void CreateFood(CatController cat)
    {
        // ���� ������Ʈ ����, spawnPoint ��ġ�� ���
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