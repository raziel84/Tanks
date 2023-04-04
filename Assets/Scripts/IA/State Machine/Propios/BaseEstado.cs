using UnityEngine;
using System.Collections;

public class BaseEstado : MonoBehaviour {

    public Color ColorEstado;

    //Las variables y métodos protected pueden ser utilizadas por clases que herendan de esta    
    protected MaquinaDeEstados maquinaDeEstados;

    //Los métodos virtual puede ser llamados desde cualquier clase que herede de estña utilizando la palabra base
    //Además en la clase que hereda el metodo deberá sobreescribirse
    protected virtual void Awake()
    {
        maquinaDeEstados = GetComponent<MaquinaDeEstados>();
    }

}
