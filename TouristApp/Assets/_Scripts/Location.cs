using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Location : MonoBehaviour
{
    public string Name;
    public float Lat, Lon;
    public Sprite Avatar;

    public Text LocationName;
    public Image LocationAvatar;

    private void Awake()
    {
        LocationAvatar.sprite = Avatar;
    }
    public void OnChosen()
    {
        Debug.Log("Name: " + Name + ", Lat: " + Lat + ", Long: " + Lon);
        GameManager.Instance.SetWorldPositionBaseOnRealPosition(Lat, Lon);
    }
}
