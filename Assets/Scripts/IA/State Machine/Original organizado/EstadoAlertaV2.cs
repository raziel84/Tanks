using UnityEngine;
using System.Collections;

public class EstadoAlertaV2 : Estado {

    public float velocidadGiroBusqueda = 120f;
    public float duracionBusqueda = 4f;

    private ControladorNavMeshV2 controladorNavMesh;
    private ControladorVisionV2 controladorVision;
    private float tiempoBuscando;

    //Se debe sobreescribir este método para poder llamar este método de la clase desde que se hereda
	protected override void Awake () {
        //Llamamos al método Awake de la clase Estado
        base.Awake();
        controladorNavMesh = GetComponent<ControladorNavMeshV2>();
        controladorVision = GetComponent<ControladorVisionV2>();
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
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoPersecucion);
            return;
        }

        transform.Rotate(0f, velocidadGiroBusqueda * Time.deltaTime, 0f);
        tiempoBuscando += Time.deltaTime;
        if(tiempoBuscando >= duracionBusqueda)
        {
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoPatrulla);
            return;
        }
	}
}
