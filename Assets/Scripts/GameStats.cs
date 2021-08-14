
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class GameStats
{
    public static float MeleeDamage;
    public static float RangedDamage;
    public static float MLADamage, MHADamage, RLADamage, RHADamage;
    public static float gameVol;
    public static bool isBossAlive, isPaused;
    public static float ShieldTimer, shakeMagnitude, ShakeTime;
    public static SinputSystems.InputDeviceSlot MeleeSlot, RangedSlot, localPlayerSlot;
    public static bool bothPlayersKB, isBattle, bossShielded;
    public static int items, minions;
    public static GameObject playerPrefab;
    
    public static Transform spawnPoint;

}