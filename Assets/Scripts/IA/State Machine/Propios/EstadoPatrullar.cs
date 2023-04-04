using UnityEngine;
using System.Collections;

public class EstadoPatrullar : BaseEstado
{

    public Transform[] WayPoints;

    private ControladorNavMeshAgent controladorNavMesh;
    private ControladorRangoDeVision controladorVision;
    private int siguienteWayPoint;

    protected override void Awake()
    {
        base.Awake();
        controladorNavMesh = GetComponent<ControladorNavMeshAgent>();
        controladorVision = GetComponent<ControladorRangoDeVision>();
    }
	
	// Update is called once per frame
	void Update () {
        // Ve al jugador?
        RaycastHit hit;
        if(controladorVision.PuedeVerAlJugador(out hit))
        {
            controladorNavMesh.perseguirObjectivo = hit.transform;
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoAtacarYPerseguir);
            return;
        }

        if (controladorNavMesh.HemosLlegado())
        {
            siguienteWayPoint = (siguienteWayPoint + 1) % WayPoints.Length;
            ActualizarWayPointDestino();
        }
	}

    void OnEnable()
    {
        ActualizarWayPointDestino();
    }

    void ActualizarWayPointDestino()
    {
        controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent(WayPoints[siguienteWayPoint].position);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && enabled)
        {
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoAlerta);
        }
    }
}
