using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage 
{
   public static float totalhealth=100;
  public static void DamageTaken(float damage)
    {
        totalhealth -= damage;
       
       
    }
}
