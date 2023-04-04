using UnityEngine;
using System.Collections;

public class ControladorNavMeshVO : MonoBehaviour {

    [HideInInspector]
    //Guarda la posici�n del jugador
    public Transform perseguirObjectivo;

    //Referencia al navmeshagent
    private UnityEngine.AI.NavMeshAgent navMeshAgent;

	void Awake () {
    //Asiganamos el NavMeshAgent
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
    //Actualiza el punto de destino del enemigo
	public void ActualizarPuntoDestinoNavMeshAgent(Vector3 puntoDestino) {
        //Asignamos el nuevo punto
        navMeshAgent.destination = puntoDestino;
        //Cada vez que se cambia el destino debemos indicarle al navmesh que retome su camino
        navMeshAgent.Resume();
    }

    //Metodo de sobrecarga para actualizar la posici�n de destino con la del jugador
    //Se utiliza en la persecuci�n
    public void ActualizarPuntoDestinoNavMeshAgent()
    {
        ActualizarPuntoDestinoNavMeshAgent(perseguirObjectivo.position);
    }

    //Metodo que detendra al enemigo. Utilizado cuando el jugador entra en el rango de visi�n
    public void DetenerNavMeshAgent()
    {
        navMeshAgent.Stop();
    }

    //Metodo para saber si el enemigo alcanzo la posici�n de destino
    //Utilizado por ej cuando alcanza el primer waypoint y debemos indicarle el siguiente waypoint
    public bool HemosLlegado()
    {
        //remainingDistance: indica la distancia que falta para llegar al punto de destino
        //Devuelve true si la distancia que falta es menor a la distancia de detenido
        //y no queda camino pendiente por recorrer
        return navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending;
    }
}
