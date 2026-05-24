using UnityEngine;
using System.Collections;

// Script para abrir e fechar a porta no VR
// Usa animação via código para evitar bugs de física com o Character Controller
[RequireComponent(typeof(HingeJoint))]
public class PortaVR : MonoBehaviour
{
    private HingeJoint dobradica;
    
    [Header("Configuracoes")]
    [Tooltip("Tempo em segundos para a porta abrir ou fechar totalmente.")]
    public float tempoDeMovimento = 1.0f; 
    
    // Controles de estado
    private bool estaAberta = false;
    private bool estaAnimando = false;
    
    // Variaveis para o giro da porta
    private Vector3 pontoDeGiro;
    private Vector3 eixoDeGiro;
    private float anguloMaximo;

    // Guarda a posicao inicial da porta
    private Vector3 posFechada;
    private Quaternion rotFechada;

    void Start()
    {
        dobradica = GetComponent<HingeJoint>();
        
        // Pega as configuracoes do Hinge Joint antes de apagar ele
        pontoDeGiro = dobradica.anchor;
        eixoDeGiro = dobradica.axis;
        anguloMaximo = dobradica.limits.max; // Ex: 90 graus

        posFechada = transform.localPosition;
        rotFechada = transform.localRotation;

        // Remove a fisica da dobradica para a porta nao bugar
        Destroy(dobradica);
    }

    // Funcao chamada quando o player clica/pega na porta
    public void Interagir_AgarrarPorta()
    {
        if (estaAnimando) return; // Nao faz nada se a porta ja estiver se movendo
        
        if (estaAberta)
        {
            StartCoroutine(AnimarPorta(-anguloMaximo)); // Fecha
            estaAberta = false;
        }
        else
        {
            StartCoroutine(AnimarPorta(anguloMaximo)); // Abre
            estaAberta = true;
        }
    }

    // Funcao vazia caso o evento de soltar seja chamado por engano
    public void Interagir_SoltarPorta() { }

    private IEnumerator AnimarPorta(float grausParaGirar)
    {
        estaAnimando = true;
        float tempoDecorrido = 0f;
        
        // Converte a posicao local do pivo para o mundo
        Vector3 anchorWorld = transform.TransformPoint(pontoDeGiro);
        Vector3 axisWorld = transform.TransformDirection(eixoDeGiro);

        float velocidadeGiro = grausParaGirar / tempoDeMovimento;

        while (tempoDecorrido < tempoDeMovimento)
        {
            // Gira a porta aos poucos
            float passo = velocidadeGiro * Time.deltaTime;
            transform.RotateAround(anchorWorld, axisWorld, passo);
            
            tempoDecorrido += Time.deltaTime;
            yield return null;
        }

        // Garante que a porta volte exatamente pro lugar original quando fechar
        if (!estaAberta)
        {
            transform.localPosition = posFechada;
            transform.localRotation = rotFechada;
        }

        estaAnimando = false;
    }
}
