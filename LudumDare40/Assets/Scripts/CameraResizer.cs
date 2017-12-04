using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class CameraResizer : MonoBehaviour
{

    private new Camera camera;
    
    public float width, height;

    private float camWidth, camHeight;
    
    public float margin = 1.5f;

    public float viewportMarginTop = 0f;
    public float viewportMarginBottom = 0f;
    public float viewportMarginLeft = 0f;
    public float viewportMarginRight = 0f;

    private bool forceResize = false;

    private void Awake()
    {
    }

    void Start()
    {

        camera = GetComponent<Camera>();

        Resize();
    }

    void Resize()
    {
        float actualHeight = height * (1 + viewportMarginTop + viewportMarginBottom);
        float actualWidth = width * (1 + viewportMarginLeft + viewportMarginRight);

        float ratioCam = camera.aspect;
        float ratioBoard = (actualWidth + 2 * margin) / (actualHeight + 2 * margin);


        camera.orthographicSize = ratioBoard > ratioCam ? (actualWidth + 2 * margin) / 2 / ratioCam : (actualHeight + 2 * margin) / 2;


        camWidth = camera.pixelWidth;
        camHeight = camera.pixelHeight;


    }

    private void Update()
    {
        if (forceResize || camera.pixelHeight != camHeight || camera.pixelWidth != camWidth)
        {
            Resize();
        }
    }

    private void OnValidate()
    {
        forceResize = true;
    }
}
