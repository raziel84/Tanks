using UnityEngine;
using System.Collections;

public class MaquinaDeEstados : MonoBehaviour {

    public BaseEstado EstadoPatrullar;
    public BaseEstado EstadoAlerta;
    public BaseEstado EstadoPerseguir;
    public BaseEstado EstadoInicial;
    public BaseEstado EstadoAtacarYPerseguir;

    public MeshRenderer MeshRendererIndicador;

    private BaseEstado estadoActual;

    void Start () {
        ActivarEstado(EstadoInicial);
	}

    public void ActivarEstado(BaseEstado nuevoEstado)
    {
        if(estadoActual!=null) estadoActual.enabled = false;
        estadoActual = nuevoEstado;
        estadoActual.enabled = true;

        MeshRendererIndicador.material.color = estadoActual.ColorEstado;
    }

}
