using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrentEventObject 
{
    public static string eventId { get; set; }
    public static string numOfOrgs { get; set; }
    public static string uid { get; set; }
    public static List<string> orgs { get; set; }

    static CurrentEventObject()
    {
        eventId = "";
        numOfOrgs = "";
        uid = "";
        orgs = new List<string>();
    }

    public static void AddOrg(string value)
    {
        // Record this value in the list.
        orgs.Add(value);
    }

    public static void Display()
    {
        // Write out the results.
        foreach (var value in orgs)
        {
            Debug.Log(value);
        }
    }
}
