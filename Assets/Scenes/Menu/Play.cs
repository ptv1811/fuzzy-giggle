using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public GameObject obj;
 public void PlayGame(){
   System.Random rnd = new System.Random();
int num  = rnd.Next(1, 100);
    PassMat.passmat(obj.GetComponent<Renderer>().material);
    if(num % 2 ==0){
       num = 2;
    }
    else num =1;
    FindObjectOfType<AudioScript>().PlaySound("Cancel");
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
   public void QuitGame(){
      Application.Quit();
   }
}
