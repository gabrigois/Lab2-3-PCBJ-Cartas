using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageCartas : MonoBehaviour
{
    public GameObject carta;        // A carta a ser descartada
    private bool primeiraCartaSelecionada, segundaCartaSelecionada, terceiraCartaSelecionada, quartaCartaSelecionada;   // indicadores para cada carta escolhida em cada linha
    private GameObject carta1, carta2, carta3, carta4;      // gameObjects da 1ª e 2ª carta selecionada
    private string linhaCarta1, linhaCarta2, linhaCarta3, linhaCarta4;   // linha da carta selecionada

    bool timerPausado, timerAcionado;       // indicador de pausa no Timer ou Start Timer
    float timer;                            // variável de tempo

    int numTentativas = 0;                  // número de tentativas na rodada
    int numAcertos = 0;                     // número de match de pares acertados
    int recorde;                            // recorde de menos tentativas
    AudioSource somOK;                      // som de acerto

    int ultimoJogo = 0;


    // Start is called before the first frame update
    void Start()
    {
        MostraCartas();
        UpDateTentativas();
        somOK = GetComponent<AudioSource>();
        ultimoJogo = PlayerPrefs.GetInt("Jogadas", 0);
        recorde = PlayerPrefs.GetInt("Recorde");
        GameObject.Find("ultimaJogada").GetComponent<Text>().text = "Jogo Anterior = " + ultimoJogo;
        GameObject.Find("recordeAtual").GetComponent<Text>().text = "Recorde = " + recorde;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerAcionado)
        {
            timer += Time.deltaTime;
            print(timer);
            if (timer > 1)
            {
                timerPausado = true;
                timerAcionado = false;
                if(carta1.tag == carta2.tag && carta1.tag == carta3.tag && carta1.tag == carta4.tag)
                {
                    Destroy(carta1);
                    Destroy(carta2);
                    Destroy(carta3);
                    Destroy(carta4);
                    numAcertos++;
                    somOK.Play();
                    if (numAcertos == 13)
                    {
                        if (numTentativas < recorde)
                        {
                            PlayerPrefs.SetInt("Recorde", numTentativas);
                            SceneManager.LoadScene("Lab3_congrats");        // Leva para a tela de congratulações por quebrar o Recorde
                        }
                        else
                        {
                            PlayerPrefs.SetInt("Jogadas", numTentativas);
                            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                            SceneManager.LoadScene("Lab3_restart");
                        }
                    }    
                }
                else
                {
                    carta1.GetComponent<Tile>().EscondeCarta();
                    carta2.GetComponent<Tile>().EscondeCarta();
                    carta3.GetComponent<Tile>().EscondeCarta();
                    carta4.GetComponent<Tile>().EscondeCarta();
                }
                primeiraCartaSelecionada = false;
                segundaCartaSelecionada = false;
                terceiraCartaSelecionada = false;
                quartaCartaSelecionada = false;
                carta1 = null;
                carta2 = null;
                carta3 = null;
                carta4 = null;
                linhaCarta1 = "";
                linhaCarta2 = "";
                linhaCarta3 = "";
                linhaCarta4 = "";
                timer = 0;
            }
        }
    }

    void MostraCartas()
    {
        int[] arrayEmbaralhado = criarArrayEmbaralhado();
        int[] arrayEmbaralhado2 = criarArrayEmbaralhado();
        int[] arrayEmbaralhado3 = criarArrayEmbaralhado();
        int[] arrayEmbaralhado4 = criarArrayEmbaralhado();
        // Instantiate(carta, new Vector3(0, 0, 0), Quaternion.identity);
        // AddUmaCarta();
        for (int i=0; i<13; i++)
        {
            // AddUmaCarta(i);
            // AddUmaCarta(i, arrayEmbaralhado[i]);         
            AddUmaCarta(0, i, arrayEmbaralhado[i]);
            AddUmaCarta(1, i, arrayEmbaralhado2[i]);
            AddUmaCarta(2, i, arrayEmbaralhado3[i]);
            AddUmaCarta(3, i, arrayEmbaralhado4[i]);
        }
    }

    void AddUmaCarta(int linha, int rank, int valor)
    {
        GameObject centro = GameObject.Find("centroDaTela");
        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorEscalaX = (650 * escalaCartaOriginal) / 110.0f;
        float fatorEscalaY = (945 * escalaCartaOriginal) / 110.0f;
        // Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank - 13 / 2)*1.3f), centro.transform.position.y, centro.transform.position.z);
        // Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank - 13 / 2) * fatorEscalaX), centro.transform.position.y, centro.transform.position.z);
        Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank - 13 / 2) * fatorEscalaX), centro.transform.position.y + ((linha - 2) * fatorEscalaY), centro.transform.position.z);
        // GameObject c = (GameObject)(Instantiate(carta, new Vector3(0, 0, 0), Quaternion.identity));
        // GameObject c = (GameObject)(Instantiate(carta, new Vector3(rank * 1.5f, 0, 0), Quaternion.identity));
        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity));
        c.tag = "" + (valor+1);
        // c.name = "" + valor;
        c.name = "" + linha + "_" + valor;
        string nomeDaCarta = "";
        string numeroCarta = "";
        /* if (rank == 0)
            numeroCarta = "ace";
        else if (rank == 10)
            numeroCarta = "jack";                   usado no Lab3 Parte A
        else if (rank == 11)
            numeroCarta = "queen";
        else if (rank == 12)
            numeroCarta = "king";
        else
            numeroCarta = "" + (rank + 1); */       // else if para array ordenado no deck

        if (valor == 0)
            numeroCarta = "ace";
        else if (valor == 10)
            numeroCarta = "jack";
        else if (valor == 11)
            numeroCarta = "queen";
        else if (valor == 12)
            numeroCarta = "king";
        else
            numeroCarta = "" + (valor + 1);
        // nomeDaCarta = numeroCarta + "_of_clubs";
        if (linha == 1)
            nomeDaCarta = numeroCarta + "_of_hearts";
        else if (linha == 0)
            nomeDaCarta = numeroCarta + "_of_clubs";
        else if (linha == 2)
            nomeDaCarta = numeroCarta + "_of_spades";
        else
            nomeDaCarta = numeroCarta + "_of_diamonds";
        Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeDaCarta));
        print("S1: " + s1);
        // GameObject.Find("" + rank).GetComponent<Tile>().setCartaOriginal(s1);        usado no Lab3 Parte A
        //GameObject.Find("" + valor).GetComponent<Tile>().setCartaOriginal(s1);
        GameObject.Find("" + linha + "_" + valor).GetComponent<Tile>().setCartaOriginal(s1);
    }

    public int[] criarArrayEmbaralhado()
    {
        int[] novoArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        int temp;
        for (int t=0; t<13; t++)
        {
            temp = novoArray[t];
            int r = Random.Range(t, 13);           //algoritmo para embaralhar as cartas
            novoArray[t] = novoArray[r];
            novoArray[r] = temp;
        }
        return novoArray;
    }

    public void CartaSelecionada(GameObject carta)
    {
        if (!primeiraCartaSelecionada)
        {
            string linha = carta.name.Substring(0, 1);
            linhaCarta1 = linha;
            primeiraCartaSelecionada = true;
            carta1 = carta;
            carta1.GetComponent<Tile>().RevelaCarta();
        }
        else if (primeiraCartaSelecionada && !segundaCartaSelecionada)
        {
            string linha = carta.name.Substring(0, 1);
            linhaCarta2 = linha;
            segundaCartaSelecionada = true;
            carta2 = carta;
            carta2.GetComponent<Tile>().RevelaCarta();
        }
        else if (primeiraCartaSelecionada && segundaCartaSelecionada && !terceiraCartaSelecionada)
        {
            string linha = carta.name.Substring(0, 1);
            linhaCarta3 = linha;
            terceiraCartaSelecionada = true;
            carta3 = carta;
            carta3.GetComponent<Tile>().RevelaCarta();
        }
        else if (primeiraCartaSelecionada && segundaCartaSelecionada && terceiraCartaSelecionada && !quartaCartaSelecionada)
        {
            string linha = carta.name.Substring(0, 1);
            linhaCarta4 = linha;
            quartaCartaSelecionada = true;
            carta4 = carta;
            carta4.GetComponent<Tile>().RevelaCarta();
            VerificaCartas();
        }
    }

    public void VerificaCartas()
    {
        DisparaTimer();
        numTentativas++;
        UpDateTentativas();
    }

    public void DisparaTimer()
    {
        timerPausado = false;
        timerAcionado = true;
    }

    void UpDateTentativas()
    {
        GameObject.Find("numTentativas").GetComponent<Text>().text = "Tentativas = " + numTentativas;
    }
}
