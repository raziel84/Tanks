using UnityEngine;
using System.Collections;

public class MaquinaDeEstadosV2 : MonoBehaviour {

    public Estado EstadoPatrulla;
    public Estado EstadoAlerta;
    public Estado EstadoPersecucion;
    public Estado EstadoInicial;

    public MeshRenderer MeshRendererIndicador;

    private Estado estadoActual;

    void Start () {
        ActivarEstado(EstadoInicial);
	}

    public void ActivarEstado(Estado nuevoEstado)
    {
        if(estadoActual!=null) estadoActual.enabled = false;
        estadoActual = nuevoEstado;
        estadoActual.enabled = true;

        MeshRendererIndicador.material.color = estadoActual.ColorEstado;
    }

}
