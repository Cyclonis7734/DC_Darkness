using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ReachedEnd : EndGame
{
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == gamoPlayer)
        {
            EndTheGame("That's enough... We've got the data... Cut the connection and get him out of there...",true);
        }
    }

}
