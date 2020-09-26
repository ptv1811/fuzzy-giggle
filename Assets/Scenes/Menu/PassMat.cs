using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PassMat 
{
    public static Material material;
    public static int index;
    public static void passmat(Material pass){
            material = pass ;
    }
     public static void passindex(int ind){
            index = ind ;
    }
}
