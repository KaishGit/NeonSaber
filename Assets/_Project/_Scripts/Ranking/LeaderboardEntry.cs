using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardEntry
{
    public string id;
    public string name;
    public string time;

    public LeaderboardEntry()
    {
    }

    public LeaderboardEntry(string name, string time)
    {
        this.id = RandomString();
        this.name = name;
        this.time = time;
    }

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["id"] = id;
        result["name"] = name;
        result["time"] = time;

        return result;
    }

    public string RandomString()
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string id = string.Empty;
        for (int i = 0; i < 15; i++)
        {
            id += chars[Random.Range(0, chars.Length)].ToString();
        }

        return id;
    }
}
