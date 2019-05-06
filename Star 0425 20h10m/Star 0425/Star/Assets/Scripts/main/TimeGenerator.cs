﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGenerator : MonoBehaviour
{
    private Camera mainCamera;
    public GameObject player, target;
    private PlayerMove playerMove;
    private float timer, setRan, minAngle, maxAngle, rotaTimer, distance;

    public float changeTime;
    public bool cameraMoveNow, zoneHit;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        cameraMoveNow = false;
        mainCamera = Camera.main;
        rotaTimer = 0.0f;
        zoneHit = false;
        playerMove = player.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!zoneHit)
        {
            return;
        }
        if (!cameraMoveNow)
        {
            switch (playerMove.viewSet)
            {
                case PlayerMove.View.back:
                    playerMove.viewSet = PlayerMove.View.side;
                    break;
                case PlayerMove.View.side:
                    playerMove.viewSet = PlayerMove.View.back;
                    break;
                default:
                    break;
            }
            cameraMoveNow = true;
            playerMove.set2DSpeed = player.transform.position.x / 60.0f;
            Time.timeScale = 0f;
        }
        else
        {
            CameraMove();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            zoneHit = true;
        }
    }
    private void CameraMove()
    {
        mainCamera.transform.LookAt(player.transform);
        switch (playerMove.viewSet)
        {
            case PlayerMove.View.back:
                minAngle = -90.0f;
                maxAngle = 0.0f;
                distance = 0;
                break;
            case PlayerMove.View.side:
                minAngle = 0.0f;
                maxAngle = -90.0f;
                distance = 0;
                break;
            default:
                break;
        }

        rotaTimer += 1 / 60.0f;
        float angle = Mathf.LerpAngle(minAngle, maxAngle, rotaTimer);
        target.transform.eulerAngles = new Vector3(0, angle, 0);
        target.transform.position = new Vector3(0, 0, distance * rotaTimer);

        if (rotaTimer >= 1f)
        {
            mainCamera.orthographic = !mainCamera.orthographic;
            rotaTimer = 0;
            cameraMoveNow = false;
            Time.timeScale = 1f;
            zoneHit = false;
        }
    }
}
