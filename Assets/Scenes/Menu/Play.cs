﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public GameObject obj;
 public void PlayGame(){
    PassMat.passmat(obj.GetComponent<Renderer>().material);
     SceneManager.LoadScene(1);
      }
   public void Git(){
      Application.OpenURL("https://github.com/ptv1811/fuzzy-giggle");
   }
}