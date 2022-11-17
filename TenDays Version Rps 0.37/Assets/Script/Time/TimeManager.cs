using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [Header("Internal Clock")]
    [SerializeField] GameTimestamp timestamp;

    public float timeScale = 1.0f;

    [Header("Day and Night Circle")]
    // The transform of the direction light
    public Transform sunTransform;

    // List of Objects to inform of changes to the time
    List<ITimeTracker> listeners = new List<ITimeTracker>();
    

    private void Awake()
    {
        // if there is more than one instance, destroy the extra.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        } 
        else
        {
            //set the static instance to this instance
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // initialise the time stamp;
        timestamp = new GameTimestamp(1, 6, 0);
        StartCoroutine(TimeUpdate());
    }

    IEnumerator TimeUpdate()
    {
        while(true)
        {
            Tick();
            yield return new WaitForSeconds(1/timeScale);
        }
        
    }

 
    // When tick, update the clock
    public void Tick()
    {
        timestamp.UpdateClock();

        // Inform the listeners of the new time state
        foreach(ITimeTracker listener in listeners)
        {
            listener.ClockUpdate(timestamp);
        }

        UpdateSunMovement();

    }

    // Day and night circle
    void UpdateSunMovement()
    {
        // Convert the current time to minutes
        int timeInMinutes = GameTimestamp.HourToMinute(timestamp.hour) + timestamp.minute;

        // Sun moves 15 degrees in an hour
        // .25 degrees in a minute
        // At midnight, the angle of the sun should be -90
        float sunAngle = .25f * timeInMinutes - 90;

        // Apply the angle to the direction light
        sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);
    }

    // Get the timestep
    public GameTimestamp GetGameTimestamp()
    {
        // Return a clonned instance
        return new GameTimestamp(timestamp);
    }

    // Handles Listeners

    // Add the object to the list of listeners
    public void RegisterTracker(ITimeTracker listener)
    {
        listeners.Add(listener); 
    }

    // Remove the object from the list of listeners
    public void UnregisterTracker(ITimeTracker listener)
    {
        listeners.Remove(listener);
    }
}
