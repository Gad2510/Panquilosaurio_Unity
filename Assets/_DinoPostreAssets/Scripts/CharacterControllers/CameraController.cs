using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform trns_pl;
    private void Awake()
    {
        trns_pl = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(trns_pl.position.x, trns_pl.position.y + 2f, trns_pl.position.z - 2f);
        transform.LookAt(trns_pl);
    }
}
