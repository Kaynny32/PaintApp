using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColor : MonoBehaviour
{
    [SerializeField]
    List<GameObject> brush = new List<GameObject>();
    [SerializeField]
    Color color;
    [SerializeField]
    List<GameObject> _button = new List<GameObject>();

    //string colorStr;

    public void ColorBrush()
    {
       
        for (int index = 0; index < _button.Count; index++)
        {
            _button[index].GetComponent<CanvasGroup>().alpha = 0.5f;
        }

        //gameObject.GetComponent<Image>().color.ToString();
        for (int i =0; i< brush.Count;i++)
        {
            //colorStr = brush[i].GetComponent<P3dPaintDecal>().Color.ToString();
            brush[i].GetComponent<P3dPaintDecal>().Color = color;            
            //Debug.Log(colorStr);
        }
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }
}
