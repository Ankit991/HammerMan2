using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage 
{
   public static int totalhealth=100;
  public static void DamageTaken(int damege)
    {
        totalhealth -= damege;
    }
}
