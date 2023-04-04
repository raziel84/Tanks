using UnityEngine;
using System.Collections;

public class EstadoPatrullaVO : MonoBehaviour {

    //Array que contiene los WP que forman el camino a seguir
    public Transform[] WayPoints;
    //En estado patrulla e color es verde
    public Color ColorEstado = Color.green;

    //Referencias a los scripts
    private MaquinaDeEstadosVO maquinaDeEstados;
    private ControladorNavMeshVO controladorNavMesh;
    private ControladorVisionVO controladorVision;

    //Para guardar la posición del WP actual dentro del array
    private int siguienteWayPoint;

    void Awake()
    {
        //Obtenemos los scripts
        maquinaDeEstados = GetComponent<MaquinaDeEstadosVO>();
        controladorNavMesh = GetComponent<ControladorNavMeshVO>();
        controladorVision = GetComponent<ControladorVisionVO>();
    }
	
	// Update is called once per frame
	void Update () {
        // Ve al jugador?
        RaycastHit hit;
        if(controladorVision.PuedeVerAlJugador(out hit))
        {
            //En la variable hit tendremos la información del jugador y la asignamos en perseguirObjectivo
            controladorNavMesh.perseguirObjectivo = hit.transform;
            //Cambiamos al estado Persecución
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoPersecucion);
            //Retornamos para evitar que se ejecute el resto del código
            return;
        }

        //Comprobamos si llegamos al WP
        if (controladorNavMesh.HemosLlegado())
        {
            //Incrementamos siguienteWayPoint y calculamos el resto con la long del array
            //De esta manera el incremento en siguienteWayPoint nunca pasará la long del array
            siguienteWayPoint = (siguienteWayPoint + 1) % WayPoints.Length;
            //Actualizamos el destino con siguienteWayPoint
            ActualizarWayPointDestino();
        }
	}

    //Este método se llama cada vez que se activa este script
    void OnEnable()
    {
        //Cambiamos el color del indicador
        maquinaDeEstados.MeshRendererIndicador.material.color = ColorEstado;
        //Cada vez que se active este script actualizamos la posición de destino
        ActualizarWayPointDestino();
    }

    //Indicamos cual es el siguiente WP de destino
    void ActualizarWayPointDestino()
    {        
        controladorNavMesh.ActualizarPuntoDestinoNavMeshAgent(WayPoints[siguienteWayPoint].position);
    }

    //Comprobamos si el jugador entro dentro del rango del enemigo
    public void OnTriggerEnter(Collider other)
    {
        //Comprobamos que el objeto que entro tenga el tag Player y además de que este script este activo
        //Sino lo hicieramos por ej si esta persiguiendo y entramos dentro del collider se disparaía este trigger
        if (other.gameObject.CompareTag("Player") && enabled)
        {
            //Activamos el estado de alerta
            maquinaDeEstados.ActivarEstado(maquinaDeEstados.EstadoAlerta);
        }
    }
}
