using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour 
{
	#region Enums
	
	
    #endregion
	
    #region Atributos y Propiedades
	
	
    #endregion
	
    #region Eventos    
	
	
    #endregion
	
    #region Mensajes Unity
	
	void Start () {
		
	}

    Quaternion rotacionObjetivo;
	// Update is called once per frame
	void Update () 
    {
        transform.Translate( Time.deltaTime * transform.forward);

        float angle = Random.Range(-10, 10);
        transform.Rotate(Vector3.up, angle, Space.World);
        
		
	}
	#endregion
	
    #region Métodos
	
	
    #endregion
    #region CoRutinas
	
	
	#endregion
}
