using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManejadorJuego : Singleton<ManejadorJuego> {

    #region Atributos
    /// <summary>
    /// Referencia al bacro jugador
    /// </summary>
    public ControladorBarcoJugador barcoJugador;
    /// <summary>
    /// Enum que representa los estados de pantalla del juego
    /// </summary>
    public enum EstadoJuego { INICIAR_JUEGO, JUGANDO, JUEGO_TERMINADO };
    // estado actual del juego
    public EstadoJuego estadoActualJuego = EstadoJuego.INICIAR_JUEGO;

    // Arreglo de Objetos de juego que representan cada pantalla del juego
    public GameObject[] uiEstadosDeJuego;
    // Arreglo de objetos de juego vacios usados para desde ahí, crear los barcos enemigos
    public Transform[] spawnPoints;
    // Arreglos de Prefabs de barcos enemigos 
    public GameObject[] barcosEnemigos;
    public List<GameObject> barcosEnemigosInstanciados = new List<GameObject>();
    public int numeroMaximoEnemigos = 5;

    public int enemigosDestruidos;
    #endregion

	#region Funciones de evento
    // Use this for initialization
    new void Start()
    {
        // Se suscribe un metodo al Delegado (Action) aMuerte, para cuado se invoque, también lo hagan los métodos suscritos
        barcoJugador.aMuerte += FinJuego;
        // Se cambia el estado de juego inicial a: INICIAR_JUEGO
        CambiarEstadoJuego(EstadoJuego.INICIAR_JUEGO);
    }
    #endregion

    #region Métodos
    /// <summary>
    /// Sirve para configurar el estado de juego al perder
    /// </summary>
    void FinJuego(bool murioJugador)
    {
        // si el jugador murió
        if (murioJugador)
        {
            // Se cambia el estado actual de jeugo a: JUEGO_TERMINADO
            CambiarEstadoJuego(EstadoJuego.JUEGO_TERMINADO);
            // y se cancela la invocación del método con nombre: CrearBarcosEnemigos
            CancelInvoke("CrearBarcosEnemigos");
        }
    }

    /// <summary>
    /// Este método se encarga de controlar el estado actual del juego
    /// De mostrar la pantalla de juego actual correspondiente al estado
    /// De configurar distintas "lógicas" dependiendo del estado actual del juego
    /// </summary>
    /// <param name="estadoJuego">Estado juego.</param>
    public void CambiarEstadoJuego(EstadoJuego estadoJuego)
    {
        estadoActualJuego = estadoJuego;
        // Controla la pantalla de juego (Elementos de UI) actual
        ControlarEstadoUI();

        switch (estadoActualJuego)
        {
            case EstadoJuego.INICIAR_JUEGO:
                // desactiva el barco jugador 
                barcoJugador.gameObject.SetActive(false);
                break;
            case EstadoJuego.JUGANDO:
                // Activa el barco jugador
                barcoJugador.gameObject.SetActive(true);
                // Realiza invocación repetidamente del método con nombre: CrearBarcosEnemigos, asignando valores aleatorios en los intervalos de tiempo de creación 
                InvokeRepeating("CrearBarcosEnemigos", 0f, Random.Range(3f, 6f));
                break;
            case EstadoJuego.JUEGO_TERMINADO:
                // Obtiene de la pantalla de juego terminado un componente Text y asigna el número de enemigos destruidos
                uiEstadosDeJuego[2].GetComponentInChildren<Text>().text = "Enemigos destruidos: " + enemigosDestruidos;

                // Destruye cada barco enemigo instanciado en la escena
                foreach (GameObject barcoEnemigoActual in barcosEnemigosInstanciados)
                {
                    Destroy(barcoEnemigoActual);
                }
                // Limpia la lsiata de barcos enemigos instanciados en la escena
                barcosEnemigosInstanciados.Clear();
                break;
        }
    }

    /// <summary>
    /// Cambia el estado actual del juego a: JUGANDO
    /// Se deja como método público para poder ser asignado en el inspector como listener al presionar la pantalla de IniciarJuego
    /// </summary>
    public void IniciarJuego()
    {
        CambiarEstadoJuego(EstadoJuego.JUGANDO);
    }

    /// <summary>
    /// Se encarga de activar o desactivar la pantalla de juego actual, en funcuón del estado de juego
    /// </summary>
    private void ControlarEstadoUI()
    {
        foreach (GameObject uiActual in uiEstadosDeJuego)
        {
            uiActual.SetActive(uiActual.name.Equals(estadoActualJuego.ToString()));
        }
    }

    /// <summary>
    /// Se encarga de crear barcos enemigos
    /// </summary>
    void CrearBarcosEnemigos()
    {
        // Si hay menos barcos que el número máximo permitido
        if (barcosEnemigosInstanciados.Count < numeroMaximoEnemigos)
        {
            // crea un barco enemigo, en una posición aleatoria dentro del arreglo de los spawnpoints, lo añade a la lista barcosEnemigosInstanciados
            GameObject barcoInstanciado = Instantiate(barcosEnemigos[Random.Range(0, barcosEnemigos.Length)], transform.position, Quaternion.identity);
            barcoInstanciado.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            barcosEnemigosInstanciados.Add(barcoInstanciado);
        }
    }

    /// <summary>
    /// Remueve el barco pasado como parámetro de la lista barcosEnemigosInstanciados
    /// siempre y cuando esté contenido dentro
    /// </summary>
    /// <param name="elBarco">El barco.</param>
    public void RemoverBarcoEnemigoDeLista(GameObject elBarco)
    {
        if (elBarco != null)
        {
            if (barcosEnemigosInstanciados.Contains(elBarco))
            {
                barcosEnemigosInstanciados.Remove(elBarco);
            }
        }
    }

    /// <summary>
    /// Se encargar de reiniciar la escena de juego principal
    /// </summary>
    public void ReiniciarEscena()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}
