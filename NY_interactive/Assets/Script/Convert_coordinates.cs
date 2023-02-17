using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;






public static class Convert_coordinates
{
    public static string coordinates;
    public static double xfrom1= 18.785160;
    public static double xto1= 6.362218;
    public static int xfrom2= 895;
    public static int xto2= -851;
    public static double yfrom1 = 46.574408;
    public static double yto1 = 37.027325;
    public static int yfrom2 = 950;
    public static int yto2 = -965;


    public static int[] remapLatLng(string coord)
        {
            string[] subs = coord.Split(',');
            int[] xy = new int[2];
            for (int i = 0; i < subs.Length; i++)
            {
                double v = double.Parse(subs[i], System.Globalization.CultureInfo.InvariantCulture);
                if (i == 0)
                 {
                     xy[i] = ExtensionMethods.Remap(v, xfrom1, xto1, xfrom2, xto2);
                     //Debug.Log(xy[i]);
            }
                else
                {
                     xy[i] = ExtensionMethods.Remap(v, yfrom1, yto1, yfrom2, yto2);
                    // Debug.Log(xy[i]);
                }

            }
            return xy;
        }

     




    // Start is called before the first frame update
    //void Start()
    //{
    //    int[] coord = remapLatLng(coordinates,from1,to1,from2,to2);
    //    Debug.Log(coord[0]);
    //    Debug.Log(coord[1]);
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}



public static class ExtensionMethods
{

    public static int Remap(this double value, double from1, double to1, float from2, float to2)
    {
        int n = System.Convert.ToInt32((value - from1) / (to1 - from1) * (to2 - from2) + from2);
        return n;
    }

}

