using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro.Examples;

public struct PlayerStats
{
    public string name;
    public float position;
    public float finalPos;
    public int points;
    public int carNum;
    public int carColor;
    public int id;

    public PlayerStats(int _id, string _name, float _position, int _points, int _carNum, int _carColor, float _finalPos)
    {
        id = _id;
        name = _name;
        position = _position;
        points = _points;
        carNum = _carNum;
        carColor = _carColor;
        finalPos = _finalPos;
    }
}

public class Laderboard
{
    static Dictionary<int, PlayerStats> lb = new Dictionary<int, PlayerStats>();
    static int carIndex = -1;

    public static int RegisterCar(string _name, int _carNum, int _carColor)
    {
        int pID = 0;
        bool isRegisted = false;
        foreach (KeyValuePair<int, PlayerStats> player in lb)
        {
            if (_name.Equals(player.Value.name))
            {
                isRegisted = true;
                pID = player.Key;
                break;
            }
        }
        //Debug.Log(isRegisted + "-" + _name);
        if (!isRegisted)
        {
            carIndex++;
            lb.Add(carIndex, new PlayerStats(carIndex, _name, 0, 0, _carNum, _carColor, 0));
            return carIndex;
        }
        
        return pID;
    }

    public static void SetPosition(int _carIndex, int _lap, int _checkpoint, float _distanceToNextCheckpoint)
    {
        float position = _lap * 1000 + _checkpoint * 10 + _distanceToNextCheckpoint * 0.1f;
        lb[_carIndex] = new PlayerStats(_carIndex, lb[_carIndex].name, position, lb[_carIndex].points, lb[_carIndex].carNum, lb[_carIndex].carColor, lb[_carIndex].finalPos);
    }

    public static string GetPosition(int _carIndex)
    {
        int index = 0;
        foreach (KeyValuePair<int, PlayerStats> pos in lb.OrderByDescending(key => key.Value.position))
        {
            index++;
            if(pos.Key == _carIndex)
            {
                switch (index)
                {
                    case 1: return "1";
                    case 2: return "2";
                    case 3: return "3";
                    case 4: return "4";
                    case 5: return "5";
                    case 6: return "6";
                }
            }
        }
        return "???";
    }

    public static int getCarCount()
    {
        return lb.Count;
    }

    public static void ClearLaderboard()
    {
        List<int> keys = new List<int>(lb.Keys);
        
        foreach (int player in keys)
        {
            lb[player] = new PlayerStats(player, lb[player].name, 0f, lb[player].points, lb[player].carNum, lb[player].carColor, 0);
        }
    }

    public static Dictionary<int, PlayerStats> getCars()
    {
        return lb;
    }

    public static string getCarName(int id)
    {
        return lb[id].name;
    }

    public static int getCarModel(int _id)
    {
        return lb[_id].carNum;
    }

    public static PlayerStats[] getOrderedCars()
    {
        PlayerStats[] cars = lb.Values.ToArray();

        Array.Sort(cars, delegate(PlayerStats x, PlayerStats y) { return x.finalPos.CompareTo(y.finalPos); });
        //Array.Reverse(cars);
        
        for (int i = 0; i < cars.Length; i++)
        {
            Debug.Log(i + "º - " + cars[i].finalPos);
        }
        
        return cars;
    }
    
    public static PlayerStats[] getOrderedCarsPoints()
    {
        PlayerStats[] cars = lb.Values.ToArray();

        Array.Sort(cars, delegate(PlayerStats x, PlayerStats y) { return x.points.CompareTo(y.points); });
        Array.Reverse(cars);
        
        /*for (int i = 0; i < cars.Length; i++)
        {
            Debug.Log(i + "º - " + cars[i].points);
        }*/
        
        return cars;
    }

    public static void setRaceStats(int _id, int _points)
    {
        lb[_id] = new PlayerStats(_id, lb[_id].name, lb[_id].position, lb[_id].points + _points, lb[_id].carNum, lb[_id].carColor, lb[_id].finalPos);
    }

    public static void resetLadder()
    {
        carIndex = -1;
        lb = new Dictionary<int, PlayerStats>();
    }

    public static void setFinalPos(int _id, float _finalPos)
    {
        lb[_id] = new PlayerStats(_id, lb[_id].name, lb[_id].position, lb[_id].points, lb[_id].carNum, lb[_id].carColor, _finalPos);
    }

    public static float GetFinalPos(int _id)
    {
        return lb[_id].finalPos;
    }
}
