using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRotate : MonoBehaviour
{
    [SerializeField]
    GameObject _goRotate;
    float y;
    float x;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _goRotate.transform.Rotate(0, 0 - 10, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _goRotate.transform.Rotate(0, 0 + 10, 0);
        }
    }
    public void RotateLeft()
    {
        y -= 10;
       // x -= 10;
        _goRotate.transform.DORotate(new Vector3(-90f, y, 0f), 3f);
        //_goRotate.transform.DOLocalRotate(new Vector3(x, 90f, 90f), 3f);
        Debug.Log(y);
    }

    public void RotateRight()
    {
        y += 10;
       // x += 10;
        _goRotate.transform.DORotate(new Vector3(-90f, y, 0f), 3f);
       // _goRotate.transform.DOLocalRotate(new Vector3(x, 90f, 90f), 3f);
        Debug.Log(y);
    }
}
