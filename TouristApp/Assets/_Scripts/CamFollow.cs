using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject Player;
    public float speed;
    void FixedUpdate()
    {
        Vector3 newPos = Vector3.MoveTowards(Camera.main.transform.position, Player.transform.position, speed * Time.deltaTime);
        Camera.main.transform.position = new Vector3(newPos.x, Camera.main.transform.position.y, newPos.z);
        Camera.main.transform.LookAt(Player.transform);
    }
}
