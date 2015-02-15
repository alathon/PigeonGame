using UnityEngine;
using System.Collections;

public class CameraOrthoSize : MonoBehaviour {

	// Use this for initialization
    void Awake()
    {
        const float UnitsPerPixel = 1f/100f;
        Camera.main.orthographicSize = Screen.height / 2f * UnitsPerPixel;
    }
}
