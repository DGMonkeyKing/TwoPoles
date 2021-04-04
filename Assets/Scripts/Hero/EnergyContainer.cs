using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyContainer : MonoBehaviour
{
    //The full value of energy is 100 (200 with augmentation)
    /*
        Empieza gastando la parte central del circulo.
        Cuando llega a 360:
         - Si se ha desbloqueado más:
            - Seguimos con los modulos
         - Si no
            - Marcamos OnCharge
    */

    public enum EnergyStates
    {
        ZERO,
        OK,
        FULL
    }
    
    [SerializeField]
    private SpriteRenderer _CenterSprite;
    private SpriteRenderer _CenterEnergySprite;
    [SerializeField]
    private SpriteRenderer _ModuleSprite;
    private SpriteRenderer _ModuleEnergySprite;
    [SerializeField]
    private float changeWaste = 0.5f;
    [SerializeField]
    private float changeCharge = 0.5f;

    
    private bool charging = true;
    private float shaderValue = 0f;
    private float energyLevel = 100f;
    private EnergyStates energyState = EnergyStates.FULL;

    private PlayerController _PlayerController;

    // Start is called before the first frame update
    void Start()
    {
        _CenterEnergySprite = _CenterSprite.gameObject.GetComponentsInChildren<SpriteRenderer>()[1];
        _ModuleEnergySprite = _ModuleSprite.gameObject.GetComponentsInChildren<SpriteRenderer>()[1];

        Debug.Log("_ModuleEnergySprite: " + _ModuleEnergySprite);

        //Check global variables to set up the energy conditions.
        var _moduleMaterial = _ModuleSprite.material;
        _moduleMaterial.SetFloat("_Arc2", 360f - GameDataSingleton.ENERGY_LEVEL * 3.6f);
        var _moduleEnergyMaterial = _ModuleEnergySprite.material;
        _moduleEnergyMaterial.SetFloat("_Arc2", 360f - GameDataSingleton.ENERGY_LEVEL * 3.6f);
        
        energyLevel += GameDataSingleton.ENERGY_LEVEL;

        _PlayerController = transform.parent.GetComponent<PlayerController>();
        
        //OnWaste subscribe
        _PlayerController.OnWaste.AddListener(Waste);
        //OnCharge subscribe
        _PlayerController.OnCharge.AddListener(Charge);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Waste()
    {
        if(energyLevel <= 0f)
        {
            _PlayerController.NoEnergy = true;
            energyLevel = 0f;
            return;
        }

        gameObject.SetActive(true);
        var _material = _ModuleEnergySprite.material;
        if(energyLevel < 100f)
        {
            _material = _CenterEnergySprite.material;
        }
        shaderValue = _material.GetFloat("_Arc1") + changeWaste;
        _material.SetFloat("_Arc1", shaderValue);
        energyLevel -= (changeWaste*100)/360;
        //Check availability of energy
    }

    void Charge()
    {
        if(energyLevel >= (100+GameDataSingleton.ENERGY_LEVEL))
        {
            _PlayerController.NoEnergy = false;
            energyLevel = (100+GameDataSingleton.ENERGY_LEVEL);
            Disappear();
            return;
        }

        var _material = _CenterEnergySprite.material;
        if(energyLevel > 100f)
        {
            _material = _ModuleEnergySprite.material;
        }
        shaderValue = _material.GetFloat("_Arc1") - changeCharge;
        _material.SetFloat("_Arc1", shaderValue);
        energyLevel += (changeCharge*100)/360;
    }

    void Disappear()
    {
        // TODO: ANIMATION
        gameObject.SetActive(false);
    }

}
