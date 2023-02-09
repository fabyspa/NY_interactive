using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Convert_coordinates : MonoBehaviour
{
    public string coordinates;
    public double from1, to1;
    public int from2, to2;

    
        public int[] remapLatLng(string coord, double from1, double to1, float from2, float to2)
        {
            string[] subs = coord.Split(',');
            int[] xy = new int[2];
            for (int i = 0; i < subs.Length; i++)
            {
                double v = double.Parse(subs[i], System.Globalization.CultureInfo.InvariantCulture);
                xy[i]= ExtensionMethods.Remap(v, from1, to1, from2, to2);
            }
            return xy;
        }

     




    // Start is called before the first frame update
    void Start()
    {
        int[] coord = remapLatLng(coordinates,from1,to1,from2,to2);
        Debug.Log(coord[0]);
        Debug.Log(coord[1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



public static class ExtensionMethods
{

    public static int Remap(this double value, double from1, double to1, float from2, float to2)
    {
        int n = System.Convert.ToInt32((value - from1) / (to1 - from1) * (to2 - from2) + from2);
        return n;
    }

}

