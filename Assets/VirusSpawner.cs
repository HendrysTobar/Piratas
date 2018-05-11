using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSpawner : MonoBehaviour 
{
    #region Enums


    #endregion

    #region Atributos y Propiedades
    public GameObject virusPrefab;
	
    #endregion
	
    #region Eventos    
	
	
    #endregion
	
    #region Mensajes Unity
	
	void Start ()
    {
        StartCoroutine(CorutinaEngendrarViruses());		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    #endregion

    #region Métodos
    public void SpawnVirus()
    {
        Instantiate(virusPrefab, this.transform.position,virusPrefab.transform.rotation);
    }

    public IEnumerator CorutinaEngendrarViruses()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            SpawnVirus();
        }
    }
	
    #endregion
    #region CoRutinas
	
	
	#endregion
}
