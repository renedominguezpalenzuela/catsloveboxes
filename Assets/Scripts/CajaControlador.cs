using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CajaControlador : MonoBehaviour {

    Quaternion angulo_OESTE= Quaternion.Euler (0, 180, 0);
    Quaternion angulo_SUR = Quaternion.Euler (0, 90, 0);
    public DireccionObjetos direccion_puerta_caja = DireccionObjetos.Sur;

    Quaternion angulo_deseado;

    float velocidad_rotacion = 5f;

    Touch toque;

    void Start () {

        angulo_deseado = angulo_SUR;
        direccion_puerta_caja = DireccionObjetos.Sur;

    }

    // Update is called once per frame
    void Update () {

        if (Input.touchCount > 0) {
         
            toque = Input.GetTouch (0);
            if (toque.phase == TouchPhase.Began) {

                Ray ray = Camera.main.ScreenPointToRay (Input.touches[0].position);
                //Debug.DrawRay (ray.origin, ray.direction * 1000, Color.green, 5, false);

                RaycastHit hit;

                if (Physics.Raycast (ray, out hit)) {
                    if (hit.collider != null && hit.collider.gameObject.tag == "caja") {
                        CambiarAngulo ();
                        //Color nuevoColor = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), 1.0f);
                        //hit.collider.GetComponent<MeshRenderer> ().material.color = nuevoColor;
                    }
                }

                
            }
        }

        transform.rotation = Quaternion.Lerp (transform.rotation, angulo_deseado, Time.deltaTime * velocidad_rotacion);
    }

    void CambiarAngulo () {
        //if (angulo_deseado.eulerAngles.y == angulo_0.eulerAngles.y) {

        if (direccion_puerta_caja == DireccionObjetos.Sur){
            direccion_puerta_caja = DireccionObjetos.Oeste;    
            angulo_deseado = angulo_OESTE;
        } else {
            
            direccion_puerta_caja = DireccionObjetos.Sur;    
            angulo_deseado = angulo_SUR;
        }
    }

}