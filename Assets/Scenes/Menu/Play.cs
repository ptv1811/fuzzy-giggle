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
     SceneManager.LoadScene(num);
      }
   public void Git(){
      Application.OpenURL("https://github.com/ptv1811/fuzzy-giggle");
   }
}
