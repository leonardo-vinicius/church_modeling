using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Interruptor : MonoBehaviour
{
    public Transform player; // Transform do jogador
    public AudioClip somBotao; // Clipe de áudio para o som do interruptor
    public KeyCode teclaAcenderLuz = KeyCode.Q; // Tecla para acender a luz
    [Range(1, 5)]
    public float distanciaMinima = 2; // Distância mínima do jogador para interagir
    public bool luzLigada; // Estado da luz (ligada ou desligada)

    [Space(15)]
    public GameObject objInterruptorOn; // GameObject para o interruptor ligado
    public GameObject objInterruptorOff; // GameObject para o interruptor desligado

    [Space(15)]
    public Light luz; // Luz a ser controlada
    public GameObject objLuzAcessa; // GameObject para a luz acesa
    public GameObject objLuzApagada; // GameObject para a luz apagada

    private float distancia; // Distância entre o jogador e o interruptor
    private AudioSource aud; // AudioSource para tocar o som do interruptor

    void Awake()
    {
        aud = GetComponent<AudioSource>();

        if (somBotao)
        {
            aud.clip = somBotao;
        }

        aud.playOnAwake = false;
        aud.loop = false;

        // Atualiza o estado da luz e dos objetos no início
        AtualizarEstadoLuz();
    }

    void Update()
    {

        if ((objInterruptorOn.gameObject.CompareTag("Player") || objInterruptorOff.gameObject.CompareTag("Player")) && Input.GetKeyDown(teclaAcenderLuz))
        {   
            luzLigada = !luzLigada;
            AtualizarEstadoLuz();

            if (aud.clip != null)
            {
                aud.PlayOneShot(aud.clip);
            }
        }
    }

    private void AtualizarEstadoLuz()
    {
        if (objLuzAcessa)
        {
            objLuzAcessa.SetActive(luzLigada);
        }

        if (objLuzApagada)
        {
            objLuzApagada.SetActive(!luzLigada);
        }

        if (luz)
        {
            luz.enabled = luzLigada;
        }

        if (objInterruptorOn)
        {
            objInterruptorOn.SetActive(luzLigada);
        }

        if (objInterruptorOff)
        {
            objInterruptorOff.SetActive(!luzLigada);
        }
    }
}