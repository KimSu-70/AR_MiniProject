using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageManager : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager imageManager;

    [SerializeField] GameObject CatPrefab;

    void Awake()
    {
        imageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        imageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            Instantiate(CatPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
        }

        foreach (var trackedImage in eventArgs.updated)
        {

        }

        foreach (var trackedImage in eventArgs.removed)
        {
            
        }

        // �Ŵ��� ��Ȱ��ȭ
        imageManager.enabled = false;
    }
}
