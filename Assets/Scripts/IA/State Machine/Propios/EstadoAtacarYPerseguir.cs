using UnityEngine;
using System.Collections;

public class EstadoAtacarYPerseguir : BaseEstado
{
    private float maxDistanciaDisparo = 30f;
    private float minDistanciaDisparo = 10f;
    private DisparoSimple disparoSimple;

    private ControladorNavMeshAgent controladorNavMesh;
    private ControladorRangoDeVision controladorVision;
    
    protected override void Awake()
    {
        //La palabra reservada base se utiliza para llamar a métodos de la clase de la cual se hereda
        base.Awake();
        controladorNavMesh = GetComponent<ControladorNavMeshAgent>();
        controladorVision = GetComponent<ControladorRangoDeVision>();
        disparoSimple = GetComponent<DisparoSimple>();
    }

    void Update()
    {
        RaycastHit hit;
        if (!controladorVision.PuedeVerAlJugador(out hit, true))
        {
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoAlerta);
            return;
        }

        controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent();

        if (hit.distance >= minDistanciaDisparo && hit.distance <= maxDistanciaDisparo)
        {
            disparoSimple.shoot = true;

        }
    }
}