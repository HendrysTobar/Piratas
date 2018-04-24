using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {
    
    #region Atributos
    /// <summary>
    ///  Prefab del sprite de explosión
    /// </summary>
    public GameObject explosionPrefab;/// 
    #endregion

    #region Funciones de evento
    // La bala se autodestruye tras 3 segundos sin colisionar con nada
    private void Start()
    {
        // Invoca el método cuyo nombre es pasado como parámetro, dentro de 3 segundos
        Invoke("Destruir", 3f);
    }

    /// <summary>
    ///  Se llama esta función de evento cuando un colisionador hace contacto con el colisionador de este objeto de juego
    /// </summary>
    /// <param name="collision">Colisionador</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se compara si el objeto que hace contacto tiene asignado el TAG: Bala
        if (collision.collider.CompareTag("Bala"))
        {
            // si es así se destruye dicho objeto
            Destroy(collision.gameObject);
            // y se instancia una copia del objeto explosión
            Instantiate(explosionPrefab, transform.position, Quaternion.identity); ;
        }
    }
    #endregion

    #region Métodos
    /// <summary>
    /// Destruye este mismo objeto de juego
    /// </summary>
    public void Destruir()
    {
        Destroy(this.gameObject);
    }
    #endregion
}
