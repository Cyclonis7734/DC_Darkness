using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInHandsManager : MonoBehaviour {

    [SerializeField] GameObject gamoLightFlashlight;
    [SerializeField] GameObject gamoLightCandle;
    [SerializeField] GameObject gamoLightCell;
    [SerializeField] GameObject gamoWeaponSharpenedStick;
    [SerializeField] GameObject gamoWeaponKnife;
    [SerializeField] GameObject gamoWeaponAxe;
    private Backpack MainBP;
    public Text txtAM;
    public Text txtLeft;
    public Text txtRight;

    private GameObject gamoWeaponCurrent;
    private GameObject gamoLightCurrent;
    private List<GameObject> gamoLights = new List<GameObject>();
    private List<GameObject> gamoWeapons = new List<GameObject>();

    public bool booCanLeftClick { set; get; }

    private void Awake()
    {
        MainBP = gameObject.GetComponent<Backpack>();
        gamoLightCurrent = null;
        gamoWeaponCurrent = null;

        gamoLights.Add(gamoLightFlashlight);
        gamoLights.Add(gamoLightCandle);
        gamoLights.Add(gamoLightCell);

        gamoWeapons.Add(gamoWeaponSharpenedStick);
        gamoWeapons.Add(gamoWeaponKnife);
        gamoWeapons.Add(gamoWeaponAxe);
        foreach (GameObject gamo in FindObjectsOfType<GameObject>())
        {
            if (gamo.name == "Action Message") { txtAM = gamo.GetComponent<Text>(); }
            else if (gamo.name == "Left Hand") { txtLeft = gamo.GetComponent<Text>(); }
            else if (gamo.name == "Right Hand") { txtRight = gamo.GetComponent<Text>(); }
        }
        booCanLeftClick = true;
    }//End Awake Method

    
    private void Update()
    {
        //Get Next Light Source
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (gamoLightCurrent != null) { gamoLightCurrent.SetActive(false); }


            bool booSet = false;
            bool booHaveALight = false;
            bool[] booHave = new bool[gamoLights.Count];
            int intMatch = 0;
            int intCount = 0;

            for (int i = 0; i < gamoLights.Count; i++)
            {
                //Set Matching Light item's Check if Have picked up to true or false
                //If No light available, check to see if one is found during each iteration
                //If current light is found, set holder, to have start position
                booHave[i] = MainBP.HaveItem(gamoLights[i].name);
                if (!booHaveALight) { if (booHave[i]) { booHaveALight = true; } }
                if (gamoLights[i] == gamoLightCurrent) { intMatch = i; }
            }

            //Check to see if ANY light source is available
            //before attempting to loop through and find next
            if (booHaveALight)
            {
                //Set start position as current light position in array
                intCount = intMatch;
                do
                {
                    //Go to next available light source
                    //if light source is available, set to new current light and set bool to exit do loop
                    //If end of available light sources in array, restart back at array position 0;
                    if (intCount >= gamoLights.Count-1) { intCount = 0; } else { intCount++; }
                    if (booHave[intCount]) { gamoLightCurrent = gamoLights[intCount]; booSet = true; }
                } while (!booSet);
                gamoLightCurrent.SetActive(true);
                StartCoroutine(SwitchedTo("Pulled out " + gamoLightCurrent.name));
                txtLeft.text = "Left Hand: " + gamoLightCurrent.name;
            }
            else
            {
                StartCoroutine(SwitchedTo("No Light Source available..."));
            }


        
        }//End Get Next Light Source



        //Get Next Weapon
        if (Input.GetKeyDown(KeyCode.Q))
        {

            if (gamoWeaponCurrent != null) { gamoWeaponCurrent.SetActive(false); }

            bool booSet = false;
            bool booHaveAWeapon = false;
            bool[] booHave = new bool[gamoWeapons.Count];
            int intMatch = 0;
            int intCount = 0;

            for (int i = 0; i < gamoWeapons.Count; i++)
            {
                //Set Matching Light item's Check if Have picked up to true or false
                //If No light available, check to see if one is found during each iteration
                //If current light is found, set holder, to have start position
                booHave[i] = MainBP.HaveItem(gamoWeapons[i].name);
                if (!booHaveAWeapon) { if (booHave[i]) { booHaveAWeapon = true; } }
                if (gamoWeapons[i] == gamoWeaponCurrent) { intMatch = i; }
            }

            //Check to see if ANY light source is available
            //before attempting to loop through and find next
            if (booHaveAWeapon)
            {
                //Set start position as current light position in array
                intCount = intMatch;
                do
                {
                    //Go to next available light source
                    //if light source is available, set to new current light and set bool to exit do loop
                    //If end of available light sources in array, restart back at array position 0;
                    if (intCount >= gamoWeapons.Count - 1) { intCount = 0; } else { intCount++; }
                    if (booHave[intCount]) { gamoWeaponCurrent = gamoWeapons[intCount]; booSet = true; }
                } while (!booSet);
                gamoWeaponCurrent.SetActive(true);
                StartCoroutine(SwitchedTo("Pulled out " + gamoWeaponCurrent.name));
                txtRight.text = "Right Hand: " + gamoWeaponCurrent.name;
            }
            else
            {
                StartCoroutine(SwitchedTo("No Weapons available..."));
            }

        }//End Get Next Weapon


        if (Input.GetMouseButton(0))
        {
            if (booCanLeftClick)
            {
                Debug.Log("MOUSE CLICK LEFT!");
                if (gamoWeaponCurrent != null)
                {
                    booCanLeftClick = false;
                    gamoWeaponCurrent.GetComponent<Animation>().Play();
                    bool booHit = false;
                    RaycastHit rhit;
                    Vector3 v3Start = transform.position;
                    Vector3 v3Stop = transform.TransformDirection(Vector3.forward);
                    booHit = Physics.Raycast(v3Start, v3Stop, out rhit, 3f);
                    if (booHit && rhit.transform.gameObject.layer == 9) //.name.Equals("Flesh Sphere"))
                    {
                        //Debug.Log("Hit " + rhit.transform.gameObject.name + " - Beg: " + v3Start.ToString() + " End: " + v3Stop.ToString());
                        switch (gamoWeaponCurrent.name)
                        {
                            case "Sharpened Stick":
                                rhit.transform.gameObject.GetComponent<EnemySM>().EnemyChangesHealth(-Random.Range(1f, 3f));
                                break;
                            case "Knife":
                                rhit.transform.gameObject.GetComponent<EnemySM>().EnemyChangesHealth(-Random.Range(3f, 5f));
                                break;
                            case "Axe":
                                rhit.transform.gameObject.GetComponent<EnemySM>().EnemyChangesHealth(-Random.Range(6f, 9f));
                                break;
                        }
                    }
                    //else { Debug.Log("No Hit - Beg: " + v3Start.ToString() + " End: " + v3Stop.ToString()); }
                    StartCoroutine(ResetAttack());
                }
            }
        }


        if (Input.GetMouseButton(1))
        {
            Debug.Log("MOUSE CLICK RIGHT!");


        }


    }//End UPdate Method
    
    private IEnumerator SwitchedTo(string strMsg)
    {
        txtAM.text = strMsg;
        yield return new WaitForSeconds(2);
        txtAM.text = "";
    }

    private IEnumerator ResetAttack()
    {

        yield return new WaitForSeconds(1f);
        booCanLeftClick = true;
    }


}