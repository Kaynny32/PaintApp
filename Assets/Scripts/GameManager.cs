using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject MainCamera;
    [SerializeField]
    GameObject _paricalBg;

    public void GameStart()
    {
        _paricalBg.SetActive(false);
        MainCamera.SetActive(true);        
    }
}
