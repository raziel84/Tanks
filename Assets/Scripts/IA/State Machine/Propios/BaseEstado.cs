using UnityEngine;
using System.Collections;

public class BaseEstado : MonoBehaviour {

    public Color ColorEstado;

    //Las variables y m�todos protected pueden ser utilizadas por clases que herendan de esta    
    protected MaquinaDeEstados maquinaDeEstados;

    //Los m�todos virtual puede ser llamados desde cualquier clase que herede de est�a utilizando la palabra base
    //Adem�s en la clase que hereda el metodo deber� sobreescribirse
    protected virtual void Awake()
    {
        maquinaDeEstados = GetComponent<MaquinaDeEstados>();
    }

}
