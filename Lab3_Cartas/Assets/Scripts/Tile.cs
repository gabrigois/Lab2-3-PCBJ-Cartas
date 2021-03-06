using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tile : MonoBehaviour
{
    private bool tileRevelada = false;      // indicador da carta virada ou n�o
    public Sprite originalCarta;            // Sprite da carta desejada
    // public Sprite novaCarta;                // update da Carta
    public Sprite backCarta;                // Sprite do avesso da carta

    // Start is called before the first frame update
    void Start()
    {
        EscondeCarta();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        print("Voc� pressionou num Tile");
        /*if (tileRevelada)
            EscondeCarta();
        else
            RevelaCarta();*/           // aqui n�o se guardava n�mero de cartas
        GameObject.Find("gameManager").GetComponent<ManageCartas>().CartaSelecionada(gameObject);
    }

    public void EscondeCarta()
    {
        GetComponent<SpriteRenderer>().sprite = backCarta;
        tileRevelada = false;
    }

    public void RevelaCarta()
    {
        GetComponent<SpriteRenderer>().sprite = originalCarta;
        tileRevelada = true;
    }

    public void setCartaOriginal(Sprite novaCarta)
    {
        originalCarta = novaCarta;
    }
}
