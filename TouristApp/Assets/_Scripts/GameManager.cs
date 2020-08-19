using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using TMPro;
using Mapbox.Unity.MeshGeneration.Factories;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Player, Target;
    public AbstractMap Map;
    public float latTarget, latPlayer, lonTarget, lonPlayer;

    public GameObject MapView, POIView;
    public Button Switch;
    public Text SwitchText;
    public DirectionsFactory DirectionsFactory;

    public List<Vector3> ListWaypoints;
    public float MoveSpeed = 30;
    Coroutine MoveIE;

    private void Awake()
    {
        Instance = this;
    }
    public void SwitchView()
    {
        if (!MapView.activeSelf)
        {
            MapView.SetActive(true);
            POIView.SetActive(false);
            SwitchText.text = "POI";
        }
        else
        {
            MapView.SetActive(false);
            POIView.SetActive(true);
            SwitchText.text = "Map";
        }
    }
    public void SetWorldPositionBaseOnRealPosition(float lat, float lon)
    {
        //var worldPosition = Conversions.GeoToWorldPosition(lat, lon, new Vector2d(49.73404, 4.167768), (float)0.000001f);
        //var worldPosition = Conversions.GeoToWorldGlobePosition(lat, lon, 500);
        var worldPosition = Map.GeoToWorldPositionXZ(new Vector2d(lat, lon));
        Debug.Log(worldPosition);
        Target.transform.position = worldPosition;
        DirectionsFactory.Query();
    }

    public void GetListWayPoint(List<Vector3> listWaypoints)
    {
        ListWaypoints = listWaypoints;
        for (int i = 0; i < ListWaypoints.Count; i++)
        {
            ListWaypoints[i] = new Vector3(ListWaypoints[i].x, Player.transform.position.y, ListWaypoints[i].z);
        }
    }
    public void Move() 
    {
        SwitchText.text = "POI";
        Debug.Log(ListWaypoints.Count);
        StartCoroutine(moveObject());
    }
    IEnumerator moveObject()
    {
        for (int i = 0; i < ListWaypoints.Count; i++)
        {
            //Player.transform.LookAt(Target.transform);
            MoveIE = StartCoroutine(Moving(i));
            yield return MoveIE;
        }
    }

    IEnumerator Moving(int currentPosition)
    {
        while (Player.transform.position != ListWaypoints[currentPosition])
        {
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, ListWaypoints[currentPosition], MoveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
