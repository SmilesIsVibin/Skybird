using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceWindowSize : MonoBehaviour
{
    void Start()
    {
        Screen.SetResolution(540, 960, FullScreenMode.Windowed);
    }
}
