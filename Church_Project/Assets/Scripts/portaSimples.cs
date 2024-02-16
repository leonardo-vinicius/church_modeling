using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portaSimples : MonoBehaviour
{   
    public Animator _animator;

    private bool _colidindo;
    private bool _portaAberta = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && _colidindo){
            _portaAberta = true;
            _animator.SetTrigger("Abrir");
        }
    }

    void OnTriggerEnter(Collider _col)
    {
        if(_col.gameObject.CompareTag("Player")){
            _colidindo = true;
        }
    }

    void OnTriggerExit(Collider _col)
    {
        if(_col.gameObject.CompareTag("Player")){

            if(_portaAberta){
                _animator.SetTrigger("Fechar");
            }
            _colidindo = false;
        }
    }
}
