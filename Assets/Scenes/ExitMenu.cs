﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class ExitMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj;
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if(Input.GetKeyDown(KeyCode.Escape))  
     {
            obj.SetActive(true);
     } 
    }
    public void BacktoMenu(){
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }
}