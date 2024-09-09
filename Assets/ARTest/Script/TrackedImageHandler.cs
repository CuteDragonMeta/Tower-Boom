using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrackedImageHandler : MonoBehaviour
{
    public GameObject arObjectPrefab;
    private ARTrackedImageManager _trackedImageManager;

    void Awake()
    {
        _trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        _trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        _trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            Instantiate(arObjectPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            // Update AR object position and rotation
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            // Handle removed images
        }
    }
}
