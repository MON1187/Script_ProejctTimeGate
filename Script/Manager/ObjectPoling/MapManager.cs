using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��� ����.
public enum PlayerPositionCheack
{
    shelter = 0,
    cave,
    leftMap,
    rightMap,
    mainHoll
}

public class MapManager : MonoBehaviour
{
    public GameObject shelter;  //����
    public GameObject cave;     //����

    public GameObject leftMap;
    public GameObject rightMap;
    public GameObject mainHoll;

    PlayerPositionCheack playerPositionCheack;

    //private void Start()
    //{
    //    shelter.SetActive(true);
    //    cave.SetActive(true);

    //    leftMap.SetActive(false);
    //    rightMap.SetActive(false);
    //    mainHoll.SetActive(false);
    //}
}
