using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleSticker : MonoBehaviour
{
    [SerializeField]
    Slider _sizeSlide;

    [SerializeField]
    float _size;

    public void Update()
    {
        _size = _sizeSlide.value;
    }

    public void SetSize(float size)
    {
        _size = size;
        _sizeSlide.value = _size;
    }

    public float GetSize()
    {
        return _size;
    }
}
