using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using Screen = UnityEngine.Device.Screen;

public class AdaptSizeCam : MonoBehaviour
{
    Camera m_camera;
    private float c_size_buffer = 7686.36f;

    public float s_screen;

    void Awake()
    {
        s_screen = Screen.height / Screen.width;
        m_camera = Camera.main;
    }

    private void Start()
    {
        s_screen = (float)Screen.height / (float)Screen.width;
        m_camera.orthographicSize = (float)(s_screen * 7.117 / 1.778);
    }

    // Update is called once per frame
    void Update()
    {
        s_screen = (float)Screen.height / (float)Screen.width;
        m_camera.orthographicSize = (float)(s_screen * 7.117 / 1.778);
    }
}
