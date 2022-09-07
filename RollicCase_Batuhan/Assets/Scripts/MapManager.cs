using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject[] maps;

    [SerializeField] private GameObject mapsParent;

    [SerializeField] private GameObject finishPart;

    [SerializeField] private Vector3 finishPosition;

    public Vector3 mapPosition;   

    private void Start()
    {
        MapsCreator();
    }
    private void MapsCreator()
    {
        if (GameManager.instance.mapsLevel < maps.Length)
        {
            GameManager.instance.mapOnPlay = Instantiate(maps[GameManager.instance.mapsLevel],mapsParent.transform);
            GameManager.instance.mapOnPlay.transform.localPosition = mapPosition;
            MapPartCountCalculator(GameManager.instance.mapOnPlay);
        }
        else
        {
            GameManager.instance.mapsLevel = 0;
            MapsCreator();
        }
        DeleteOldMaps();
    }
    public void NextLevelLoad()
    {
        GetFinisLine();
        NextLevelUpdate();
    }
    public void TryAgain()
    {
        Destroy(GameManager.instance.mapOnPlay);
        MapsCreator();
    }
    private void GetFinisLine()
    {
        Vector3 endLevelPosition = mapPosition;
        finishPart.SetActive(true);
        finishPart.transform.localPosition = new Vector3(finishPosition.x, finishPosition.y, endLevelPosition.z + 133f);
    }
    private void NextLevelUpdate()
    {
        mapPosition.z += 213;
        GameManager.instance.mapsLevel++;
        MapsCreator();
    }
    private void MapPartCountCalculator(GameObject _onPlayMap)
    {
        GameManager.instance.mapPartCount = 0;
        GameManager.instance.mapPartIndex = 0;
        foreach (Transform child in _onPlayMap.transform)
        {
            if (child.gameObject.CompareTag("Challenge"))
            {
                GameManager.instance.mapPartCount++;
            }
        }
    }
    private void DeleteOldMaps()
    {
        if (mapsParent.transform.childCount>3)
        {
            Destroy(mapsParent.transform.GetChild(1).gameObject);
        }
    }
}
