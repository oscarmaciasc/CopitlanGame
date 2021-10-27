using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomImage : MonoBehaviour, IScrollHandler
{
    public static ZoomImage instance;
    private Vector3 initialScale;

    [SerializeField] private float zoomSpeed = 0.1f;
    [SerializeField] private float maxZoom = 10f;

    private void Awake()
    {
        instance = this;
        Enable();
    }

    public void Enable() {
        transform.localScale = new Vector3(0.143f, 0.143f, 1f);
        initialScale = transform.localScale;
    }

    public void OnScroll(PointerEventData eventData)
    {
        var delta = Vector3.one * (eventData.scrollDelta.y * zoomSpeed);
        var desiredScale = transform.localScale + delta;

        desiredScale = ClampDesiredScale(desiredScale);

        transform.localScale = desiredScale;
    }

    private Vector3 ClampDesiredScale(Vector3 desiredScale)
    {
        desiredScale = Vector3.Max(initialScale, desiredScale);
        desiredScale = Vector3.Min(initialScale * maxZoom, desiredScale);
        return desiredScale;
    }
}
