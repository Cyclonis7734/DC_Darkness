using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class PlayerStatsManager : MonoBehaviour {

    private float floHealth;
    private float floMaxHealth;
    private RigidbodyFirstPersonController RFPC;
    private Slider sliderHealth;
    private Text txtHealth;
    private bool booHealRegen;

    private PlayerStatsManager _instance; //Declare instance variable

    //Begin Singleton Constructor
    public PlayerStatsManager Instance()
    {
        if (_instance == null) { _instance = new PlayerStatsManager(); }
        return _instance;
    }//End Singleton Constructor

    private void Awake()
    {
        booHealRegen = true;
        floMaxHealth = 100f;
        floHealth = floMaxHealth-10;
        RFPC = gameObject.GetComponent<RigidbodyFirstPersonController>();
        foreach (Slider slid in FindObjectsOfType<Slider>())
        {
            if (slid.name.Equals("Health Slider"))
            {
                sliderHealth = slid;
                sliderHealth.minValue = 0f;
                sliderHealth.maxValue = floMaxHealth;
                sliderHealth.value = floHealth;
            }
        }

        foreach (Text txt in FindObjectsOfType<Text>())
        {
            if (txt.name.Equals("HealthText"))
            {
                txtHealth = txt;
                txtHealth.text = Mathf.Round((floHealth / floMaxHealth) * 100).ToString();
            }
        }

        

    }//End Awake Method

    private void Update()
    {
        if (booHealRegen) { booHealRegen = false; StartCoroutine(HealthRegenStart()); }
    }

    public float GetHealth() { return floHealth; }

    public void EffectHealth(float floHealthChange)
    {
        floHealth += floHealthChange;
        if (floHealth <= 0) { floHealth = 0; KillPlayer("a Flesher!"); }
        else if (floHealth >= floMaxHealth) { floHealth = floMaxHealth; }
        //Debug.Log("Received Health Change: " + floHealthChange);
        sliderHealth.value = floHealth;
        txtHealth.text = Mathf.Round((floHealth / floMaxHealth) * 100).ToString();
    }

    public void KillPlayer(string strKiller)
    {
        GetComponent<EndGame>().EndTheGame("You were killed by " + strKiller);
    }

    public void UpdateMovementSpeeds(float floForward, float floBackward, float floStraff, float floMultiplier)
    {
        RFPC.movementSettings.ForwardSpeed = floForward;
        RFPC.movementSettings.BackwardSpeed = floBackward;
        RFPC.movementSettings.StrafeSpeed = floStraff;
        RFPC.movementSettings.RunMultiplier = floMultiplier;
    }//End UpdateMovementSpeeds Method - overload for All MoveTypes to change

    public void UpdateMovementSpeeds(MovementTypes MoveType, float floChange)
    {
        switch (MoveType)
        {
            case MovementTypes.Forward: RFPC.movementSettings.ForwardSpeed += floChange; break;
            case MovementTypes.Backward: RFPC.movementSettings.BackwardSpeed += floChange; break;
            case MovementTypes.Strafe: RFPC.movementSettings.StrafeSpeed += floChange; break;
            case MovementTypes.Multiplier: RFPC.movementSettings.RunMultiplier += floChange; break;
        }
    }//End UpdateMovementSpeeds Method - overload for One MoveType change

    public enum MovementTypes
    {
        Forward,
        Backward,
        Strafe,
        Multiplier
    }//End enum MovementType

    public float GetMoveSpd(MovementTypes MoveType)
    {
        float floResult = 0f;
        switch (MoveType)
        {
            case MovementTypes.Forward: floResult = RFPC.movementSettings.ForwardSpeed; break;
            case MovementTypes.Backward: floResult = RFPC.movementSettings.BackwardSpeed; break;
            case MovementTypes.Strafe: floResult = RFPC.movementSettings.StrafeSpeed; break;
            case MovementTypes.Multiplier: floResult = RFPC.movementSettings.RunMultiplier; break;
        }
        return floResult;
    }//End GetMovementSpeed Method

    public IEnumerator HealthRegenStart()
    {
        //Debug.Log("Hlth: " + floHealth.ToString() + " - Max: " + floMaxHealth.ToString());
        if (floHealth < floMaxHealth && floHealth > 0)
        {
            EffectHealth(1f);
        }
        yield return new WaitForSeconds(3f);
        booHealRegen = true;
    }

}
