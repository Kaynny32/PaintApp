using DG.Tweening;
using UnityEngine;

public class ButtonRotate : MonoBehaviour
{
    [SerializeField]
    GameObject _goRotate;
    [SerializeField]
    Ease ease;

    float y;
    float x;


    public void RotateItem(bool isRotate = false)
    {
        if (!isRotate)
        {
            _goRotate.transform.DOKill();
            y -= 10;
            _goRotate.transform.DORotate(new Vector3(-90, y, 0), 0.15f).SetEase(ease);
        }
        else
        {
            _goRotate.transform.DOKill();
            y += 10;
            _goRotate.transform.DORotate(new Vector3(-90, y, 0), 0.15f).SetEase(ease);
        }
    }
}
