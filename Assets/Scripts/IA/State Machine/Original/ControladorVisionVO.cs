using UnityEngine;
using System.Collections;

public class ControladorVisionVO : MonoBehaviour {

    //Variables a utilizar para disparar en el raycast
    //Posicion de origen
    public Transform Ojos;
    //Distancia maxima del rayo
    public float rangoVision = 20f;

    //Variable auxiliar utilizada en el c�lculo del vector direcci�n
    //Sumamos un incremento en y para que el vector direcci�n no vaya desde los ojos del enemigo
    //hasta los pies del jugador (la posici�n y del jugador es cero)
    //PRECAUCI�N: si el rayo golpea entre el borde de un elemento del escenario y del jugador
    //puede ingresar en un bucle infinito entre estado alerta y persecuci�n
    public Vector3 offset = new Vector3(0f, 0.75f, 0f);

    //Referencias al script controlador navmesh
    private ControladorNavMeshVO controladorNavMesh;

    void Awake()
    {
        controladorNavMesh = GetComponent<ControladorNavMeshVO>();
    }

    //M�todo que indica si vemos al jugador
    //Al definir el valor de una variable dentro de la definici�n de parametros
    //al llamar el m�todo podemos ignorar el segundo parametro porque ya tiene un valor definido
    public bool PuedeVerAlJugador(out RaycastHit hit, bool mirarHaciaElJugador = false)
    {
        Vector3 vectorDireccion;

        //Ser� verdadero cuando estemos persiguiendo al enemigo
        if (mirarHaciaElJugador)
        {
            //El vector direcci�n se calcula restando a la posici�n del jugador la posici�n de los ojos del enemigo
            vectorDireccion = (controladorNavMesh.perseguirObjectivo.position + offset) - Ojos.position;
        }else
        {
            vectorDireccion = Ojos.forward;
        }

        //Devolver� true si el collider con el que impacto tiene el tag Player
        return Physics.Raycast(Ojos.position, vectorDireccion, out hit, rangoVision) && hit.collider.CompareTag("Player");
    }
}
