using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Seed")]
public class SeedData : ItemData
{
    // Time it takes before the seed  matures into a crop
    public int hoursToGrow;

    // The crop the seed will yield
    public ItemData cropToYield;

    // The seed GameObject
    public GameObject seed;

    // The seedling GameObject
    public GameObject seedling;
}
