using UnityEngine;
using System.Collections;

public class ControladorVisionVO : MonoBehaviour {

    //Variables a utilizar para disparar en el raycast
    //Posicion de origen
    public Transform Ojos;
    //Distancia maxima del rayo
    public float rangoVision = 20f;

    //Variable auxiliar utilizada en el cálculo del vector dirección
    //Sumamos un incremento en y para que el vector dirección no vaya desde los ojos del enemigo
    //hasta los pies del jugador (la posición y del jugador es cero)
    //PRECAUCIÓN: si el rayo golpea entre el borde de un elemento del escenario y del jugador
    //puede ingresar en un bucle infinito entre estado alerta y persecución
    public Vector3 offset = new Vector3(0f, 0.75f, 0f);

    //Referencias al script controlador navmesh
    private ControladorNavMeshVO controladorNavMesh;

    void Awake()
    {
        controladorNavMesh = GetComponent<ControladorNavMeshVO>();
    }

    //Método que indica si vemos al jugador
    //Al definir el valor de una variable dentro de la definición de parametros
    //al llamar el método podemos ignorar el segundo parametro porque ya tiene un valor definido
    public bool PuedeVerAlJugador(out RaycastHit hit, bool mirarHaciaElJugador = false)
    {
        Vector3 vectorDireccion;

        //Será verdadero cuando estemos persiguiendo al enemigo
        if (mirarHaciaElJugador)
        {
            //El vector dirección se calcula restando a la posición del jugador la posición de los ojos del enemigo
            vectorDireccion = (controladorNavMesh.perseguirObjectivo.position + offset) - Ojos.position;
        }else
        {
            vectorDireccion = Ojos.forward;
        }

        //Devolverá true si el collider con el que impacto tiene el tag Player
        return Physics.Raycast(Ojos.position, vectorDireccion, out hit, rangoVision) && hit.collider.CompareTag("Player");
    }
}
