using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public GameObject obj;
 public void PlayGame(){
    FindObjectOfType<AudioScript>().PlaySound("Cancel");
    PassMat.passmat(obj.GetComponent<Renderer>().material);
     SceneManager.LoadScene(1);
     
      }
   public void Git(){
      FindObjectOfType<AudioScript>().PlaySound("Accept");
      Application.OpenURL("https://github.com/ptv1811/fuzzy-giggle");
      
   }
   public void PlayCancel(){
    
       FindObjectOfType<AudioScript>().PlaySound("Cancel");
   }
   public void PlayAccept(){
  FindObjectOfType<AudioScript>().PlaySound("Accept");
   }
   public void PlayPopUp(){
      FindObjectOfType<AudioScript>().PlaySound("PopUp");
   }
}
