using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySM : MonoBehaviour {

    //Declare Objects
    public GameObject gamoPlayer;
    public NavMeshAgent nav;
    public ParticleSystem psysBloodAttack;
    public ParticleSystem psysDeath;
    public ParticleSystem psysBloodDrop;
    public ParticleSystem psysBloodSplat;
    public AudioSource asMoans;
    public AudioSource asSquish;
    public AudioSource asTakesHit;
    public AudioSource asDeath;
    protected float floDetectionDistance { get; set; }
    protected float floHealthMax;
    protected float floHealth;

    //Declare SM Objects
    IEnemyActions IsMoving;
    IEnemyActions IsAttacking;
    IEnemyActions IsDeathSet;
    IEnemyActions IsIdle;
    IEnemyActions IsPursuing;
    IEnemyActions emCurrent;

    //******************************************************************************
    //***************************BEGIN NON-SM METHODS*******************************
    //******************************************************************************

    //Constructor
    public EnemySM()
    {
        //Create Interface Options at Construction
        IsMoving = new Moving(this);
        IsAttacking = new Attacking(this);
        IsDeathSet = new DeathSet(this);
        IsIdle = new Idle(this);
        IsPursuing = new Pursuing(this);
        //Set Current State
        emCurrent = IsIdle;
        StartIdle();
    }

    //------------------------------------------------------------------------------
    protected virtual void Awake()
    {
        floHealthMax = Random.Range(10f, 20f);
        floHealth = floHealthMax;
        //Find and Set Player object via a foreach loop
        //Setup Nav Mesh Agent object using getComponent Method
        foreach (GameObject gamo in FindObjectsOfType<GameObject>())
        {
            if (gamo.name.Equals("RigidBodyFPSController"))
            { gamoPlayer = gamo; }
        }

        //foreach (ParticleSystem psys in FindObjectsOfType<ParticleSystem>())
        //{
        //    if (psys.name.Equals("Blood Attack")) { psysBloodAttack = psys; }
        //}

        nav = gameObject.GetComponent<NavMeshAgent>();

        floDetectionDistance = 10;

    }//End Awake Method

    //------------------------------------------------------------------------------

    protected void FixedUpdate()
    {

        float floDist = Vector3.Distance(transform.position, gamoPlayer.transform.position);
        if (floDist <= floDetectionDistance)
        {
            //Debug.Log(floDist.ToString());
            PlayerSpotted(floDist);
        }
        else
        {
            if(emCurrent == IsAttacking ||
               emCurrent == IsPursuing)
            {
                StartIdle();
            }
            else if(emCurrent == IsIdle)
            {
                StartMoving();
            }
        }
    }


    //------------------------------------------------------------------------------

    public virtual void EnemyChangesHealth(float floAmount)
    {
        floHealth += floAmount;
        if(floAmount < 0) { asTakesHit.Play(); }
        if (floHealth <= 0) { floHealth = 0; StartDeathSet(); }
        else if (floHealth >= floHealthMax) { floHealth = floHealthMax; }
    }

    //------------------------------------------------------------------------------

    protected virtual void PlayerSpotted(float floCurDist)
    {
        if (floCurDist <= 3) { StartAttacking(); } else { StartPursuit(); }
    }
    //******************************************************************************
    //******************************END NON-SM METHODS******************************
    //******************************************************************************


    //******************************************************************************
    //******************************BEGIN STATE MACHINE*****************************
    //******************************************************************************
    //------------------------------------------------------------------------------
    //Set Current Status
    public void SetEnemyState(IEnemyActions newState)
    {
        emCurrent = newState;
    }//End Set Enemy Movement State

    //------------------------------------------------------------------------------

    //Methods to pass state changes
    public void StartMoving() { emCurrent.IsMoving(); } //intCounter++; Debug.Log("Moving - " + intCounter.ToString()); 
    public void StartAttacking() { emCurrent.IsAttacking(); } //intCounter++; Debug.Log("Attacking - " + intCounter.ToString()); 
    public void StartIdle() { emCurrent.IsIdle(); } //intCounter++; Debug.Log("Idle - " + intCounter.ToString()); 
    public void StartDeathSet() {emCurrent.IsDying(); } // intCounter++; Debug.Log("Death - " + intCounter.ToString()); 
    public void StartPursuit() {emCurrent.IsPursuing(); } // intCounter++; Debug.Log("Pursuit - " + intCounter.ToString()); 

    //------------------------------------------------------------------------------

    //Methods to get Statuses
    public IEnemyActions getIsMoving() { return IsMoving; }
    public IEnemyActions getIsAttacking() { return IsAttacking; }
    public IEnemyActions getIsIdle() { return IsIdle; }
    public IEnemyActions getIsDeathSet() { return IsDeathSet; }
    public IEnemyActions getIsPursuing() { return IsPursuing; }

    //------------------------------------------------------------------------------

    public virtual IEnumerator IHandleAttack()
    {
        //Debug.Log("Attacking...");
        psysBloodAttack.transform.LookAt(gamoPlayer.transform);
        psysBloodAttack.Play();
        float floDist = Vector3.Distance(transform.position, gamoPlayer.transform.position);
        if(floDist <= 3) { gamoPlayer.GetComponent<PlayerStatsManager>().EffectHealth(-floDist * Random.Range(1f,2f)); }
        yield return new WaitForSeconds(1);
        psysBloodAttack.Stop();
        StartIdle();
        //Debug.Log("Finished Attack, back to Idle.");
    }

    //------------------------------------------------------------------------------

    public virtual void HandleWandering()
    {
        Vector3 v3Rot = new Vector3(0, Random.Range(0f,60f), 0);
        float floRnd = Random.Range(0f, 10f);
        transform.Rotate(v3Rot, Space.Self);
        Vector3 v3Cur = transform.position;
        if (floRnd > 6)
        {
            nav.destination = new Vector3(v3Cur.x + Random.Range(-2f,2f),0f, v3Cur.z + Random.Range(-2f, 2f));
        }
        StartCoroutine(IHandleLostPlayer(floRnd));
    }

    //------------------------------------------------------------------------------

    //Method to pursue player
    public void PursuePlayer() { nav.destination = gamoPlayer.transform.position; }
    public void HandleAttacking() { StartCoroutine(IHandleAttack()); }
    public void HandleLostPlayer() { StartCoroutine(IHandleLostPlayer()); }
    //------------------------------------------------------------------------------

    public virtual IEnumerator IHandleLostPlayer(float floTime = 5f)
    {
        GetComponent<Rigidbody>().velocity.Set(0f,0f,0f);
        yield return new WaitForSeconds(floTime);
        StartMoving();
    }


    //------------------------------------------------------------------------------

    //Method to start death sequence of Enemy
    public void KillThisEnemy()
    {
        Debug.Log("Killing Enemy...");
        StartCoroutine(IHandleEnemyDeath());
    }
    //------------------------------------------------------------------------------

    public virtual IEnumerator IHandleEnemyDeath()
    {
        
        psysDeath.Play();
        asDeath.Play();
        yield return new WaitForSeconds(2f);
        GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(AudioFadeOut.FadeOut(asMoans, 4));
        StartCoroutine(AudioFadeOut.FadeOut(asSquish, 4));
        psysBloodDrop.Stop();
        psysBloodSplat.Stop();
        yield return new WaitForSeconds(5f);
        psysDeath.Stop();
        Destroy(this.gameObject);
    }

    //------------------------------------------------------------------------------

    //******************************************************************************
    //******************************END STATE MACHINE ******************************
    //******************************************************************************

}//End EnemyMovement class
