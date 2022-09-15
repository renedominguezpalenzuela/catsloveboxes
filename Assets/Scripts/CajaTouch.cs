using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaTouch : MonoBehaviour
{
    public GameObject caja;
    float touchDuration;
    Touch touch;
    

    // Start is called before the first frame update
    void Start()
    {
        Vector3 posicionCaja = new Vector3(0, 15, 0);
        caja.transform.position += posicionCaja;
    }

    // Update is called once per frame
    void Update()
    {
        
         {
             if (Input.touchCount > 0)
             {
                 touchDuration += Time.deltaTime;
                 touch = Input.GetTouch(0);

                 if (touch.phase == TouchPhase.Ended && touchDuration < 0.2f)
                 {
                     StartCoroutine("simpleOSencillo");
                 }
             }
             else
             {
                 touchDuration = 0.0f;
             }

             if (Input.touchCount > 0)
             {
                 if (Input.GetTouch(0).tapCount > 1)
                 {
                     //toque doble o mas
                 }
                 else
                 {
                     //toque simple
                 }
             }
         }

         IEnumerator simpleOSencillo()
         {
             yield return new WaitForSeconds(0.2f);
             if (touch.tapCount == 1)
             {
                 Debug.Log("Simple");

                Vector3 rotacionCaja = new Vector3(0, 10, 0);
                transform.eulerAngles = rotacionCaja;
            }
             else if (touch.tapCount == 2)
             {
                 StopCoroutine("simpleOSencillo");
                 Debug.Log("Doble");

             }
         }


     
    }
}

