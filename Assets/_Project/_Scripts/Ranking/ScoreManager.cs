#if !UNITY_WEBGL || UNITY_EDITOR
using Firebase.Database;
#else
using FirebaseWebGL.Scripts.FirebaseBridge;
#endif
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
#if !UNITY_WEBGL || UNITY_EDITOR
    DatabaseReference mDatabase;
#endif

    public static ScoreManager instance;

    public string id = string.Empty;

    public bool updateScore = false;
#if !UNITY_WEBGL || UNITY_EDITOR
    DataSnapshot snapshot;
#endif

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

#if !UNITY_WEBGL || UNITY_EDITOR
        // Get the root reference location of the database.
        mDatabase = FirebaseDatabase.DefaultInstance.RootReference;
#else
        // FirebaseDatabase.ListenForValueChanged("/scores/", gameObject.name, "HandleValueChanged", "");
#endif
    }

    public void RefreshScores()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        FirebaseDatabase.DefaultInstance
          .GetReference("scores")
          .GetValueAsync().ContinueWith(task =>
          {
              if (task.IsFaulted)
              {
                  // Handle the error...
              }
              else if (task.IsCompleted)
              {
                  updateScore = true;
                  snapshot = task.Result;
              }
          });
#else
        FirebaseDatabase.GetJSON("/scores/", gameObject.name, "GetValueScores", "");
#endif
    }

    void GetValueScores(string json)
    {
        JObject parsedJObject = null;
        if (!json.Equals("NULL"))
            parsedJObject = JObject.Parse(json);

        List<LeaderboardEntry> list = new List<LeaderboardEntry>();

        foreach (var data in parsedJObject)
        {
            list.Add(JsonUtility.FromJson<LeaderboardEntry>(data.Value.ToString()));
        }

        CanvasScore.instance.UpdateScoreboard(list);
    }

    private void Update()
    {

#if !UNITY_WEBGL || UNITY_EDITOR
        if (updateScore)
        {
            updateScore = false;
            //DataSnapshot snapshot = task.Result;
            // Do something with snapshot...

            List<LeaderboardEntry> list = new List<LeaderboardEntry>();

            foreach (DataSnapshot data in snapshot.Children)
            {
                list.Add(JsonUtility.FromJson<LeaderboardEntry>(data.GetRawJsonValue()));
            }


            // Do something with the data in args.Snapshot
            CanvasScore.instance.UpdateScoreboard(list);
        }
#endif
    }

    void ReceiveMessage(string json)
    {
        // nothing
    }

    public void WriteNewScore(LeaderboardEntry entry)
    {

        this.id = entry.id;

#if UNITY_WEBGL && !UNITY_EDITOR
        FirebaseDatabase.PostJSON("/scores/" + entry.id, entry.id, entry.name, entry.time, gameObject.name, "ReceiveMessage", "ReceiveMessage");
#else
        Dictionary<string, object> entryValues = entry.ToDictionary();

        Dictionary<string, object> childUpdates = new Dictionary<string, object>();
        childUpdates["/scores/" + entry.id] = entryValues;

        mDatabase.UpdateChildrenAsync(childUpdates);
#endif
    }
}
