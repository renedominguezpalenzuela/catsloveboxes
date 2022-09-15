using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToquesV4 : MonoBehaviour
{


    public GameObject g1, g2;
    private float lastTap; //time of the last click
    private float tapThreshold = 0.25f; //time threshold in which all taps should happen
    private int tapCount = 0; //tap count in current detection attempt

    Touch toque;    
    
    void Update()     {
        if (Input.touchCount > 0)  {
           toque = Input.GetTouch(0);
        
           if ( toque.phase==TouchPhase.Began){

            if (tapCount == 0   ) {
                lastTap = Time.time;
                tapCount=1;                             
            } else if (tapCount == 1 ) {
                 lastTap = Time.time;
                 tapCount=2;                            
             }   
           }
        }


       if (tapCount!=0 && Time.time - lastTap > tapThreshold) {                         
                lastTap = 0;
                switch (tapCount)   {
                    case 1: {
                        Debug.Log("Single tap");
                        tapCount = 0;
                          g1.SetActive(!g1.activeSelf);
                        break;
                    }

                    case 2: {
                        Debug.Log("Double tap");  
                        tapCount = 0;    
                          g2.SetActive(!g2.activeSelf);                                   
                        break;
                    }
                    
                    default: {
                        //Debug.Log("No tap");
                        break;
                    }


                }

               
                
             
        } //if (Time.time
        

    } //Update
}
