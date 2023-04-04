using UnityEngine;
using System.Collections;

public class EstadoAlerta : BaseEstado {

    public float velocidadGiroBusqueda = 120f;
    public float duracionBusqueda = 4f;

    private ControladorNavMeshAgent controladorNavMesh;
    private ControladorRangoDeVision controladorVision;
    private float tiempoBuscando;

    //Se debe sobreescribir este método para poder llamar este método de la clase desde que se hereda
	protected override void Awake () {
        //Llamamos al método Awake de la clase Estado
        base.Awake();
        controladorNavMesh = GetComponent<ControladorNavMeshAgent>();
        controladorVision = GetComponent<ControladorRangoDeVision>();
	}

    void OnEnable()
    {
        controladorNavMesh.DetenerNavMeshAgent();
        tiempoBuscando = 0f;
    }
	
	void Update () {
        RaycastHit hit;
        if (controladorVision.PuedeVerAlJugador(out hit))
        {
            controladorNavMesh.perseguirObjectivo = hit.transform;
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoAtacarYPerseguir);
            return;
        }

        transform.Rotate(0f, velocidadGiroBusqueda * Time.deltaTime, 0f);
        tiempoBuscando += Time.deltaTime;
        if(tiempoBuscando >= duracionBusqueda)
        {
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoPatrullar);
            return;
        }
	}
}
