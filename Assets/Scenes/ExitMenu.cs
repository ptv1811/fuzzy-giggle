using System.Collections;
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
         FindObjectOfType<AudioScript>().PlaySound("PopUp");
            obj.SetActive(true);
     } 
    }
    public void BacktoMenu(){
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
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
