using UnityEngine;
using System.Collections;

public class ControladorNavMeshV2 : MonoBehaviour {

    [HideInInspector]
    public Transform perseguirObjectivo;

    private UnityEngine.AI.NavMeshAgent navMeshAgent;

	void Awake () {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	public void ActualizarPuntoDestinoNavMeshAgent(Vector3 puntoDestino) {
        navMeshAgent.destination = puntoDestino;
        navMeshAgent.Resume();
    }

    public void ActualizarPuntoDestinoNavMeshAgent()
    {
        ActualizarPuntoDestinoNavMeshAgent(perseguirObjectivo.position);
    }

    public void DetenerNavMeshAgent()
    {
        navMeshAgent.Stop();
    }

    public bool HemosLlegado()
    {
        return navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending;
    }
}
