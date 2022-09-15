using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorGatos : MonoBehaviour {

     [SerializeField] Transform[] listaPuntoNacimiento; //posicion y rotacion inicial
     [SerializeField] Transform[] listaPuntoFinal; //untos finales

     [SerializeField] //asignar en el inspector el prefab
     private GameObject gato;

     [SerializeField]
     private float gatoRespawnTime = 3.5f;

     [SerializeField]
     private int totalGatosVivosPermitidos = 1;
     int totalGatosVivos = 0;



     void Start () {

          if (listaPuntoFinal.Length == 0) {
               Debug.Log ("ERROR: Debe asignar al menos un objeto como punto final (y al menos uno como inicial)");
               return;
          }

          if (listaPuntoNacimiento.Length == 0) {
               Debug.Log ("ERROR: Debe asignar al menos un objeto como punto inicial (y al menos uno como final)");
               return;
          }

          if (gato != null) {

               StartCoroutine (spawnNPC (1, gato, false)); //un solo gato al cabo de 1 segundo

               StartCoroutine (spawnNPC (gatoRespawnTime, gato, true));
          } else {
               Debug.Log ("ERROR: Debe asignar un prefab de gato al Generador de Gatos");
          }

     }

     private IEnumerator spawnNPC (float intervalo, GameObject npc, bool repetir) {
          yield return new WaitForSeconds (intervalo); //espera x Segundos

          if (totalGatosVivos < totalGatosVivosPermitidos) {
               totalGatosVivos++;

               int indice = Random.Range (0, listaPuntoNacimiento.Length - 1);

               if (listaPuntoNacimiento[indice].position == null || listaPuntoFinal[indice].position == null) {
                    Debug.Log ("Error puntos de nacimiento o final no validos");
               } else {
                    Transform puntoNacimiento = listaPuntoNacimiento[indice];
                    Transform puntoFinal = listaPuntoFinal[indice];
                    GameObject nuevoNPC = Instantiate (npc, puntoNacimiento.position, puntoNacimiento.rotation);
                    nuevoNPC.GetComponent<GatoControlador> ().setPuntos (puntoNacimiento, puntoFinal);
                    nuevoNPC.GetComponent<GatoControlador> ().iniciar = true;
               }

          }

          if (repetir) StartCoroutine (spawnNPC (intervalo, npc, true));

     }
}