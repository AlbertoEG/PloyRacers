using System;
using System.Collections.Generic;
using System.IO;
using CarScripts;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

namespace DebugScripts
{
    public static class AiMonitor
    {
        private static string filePath;
        private static string filePathMedian;
        private static string stats;
        private static string carModel;

        public static bool writeMedian;
        
        public static void GetCarTextPath(AiCarController _aiCar, string _carModel)
        {
            carModel = _carModel;
            stats = carModel;
            Debug.Log(stats);
            CreateTxtFile(stats, _aiCar);
        }

        private static void CreateTxtFile(string _stats, AiCarController _aiCar)
        {
            filePath = Application.dataPath + "/Metrics/" + SceneManager.GetActiveScene().name + "/" + _stats + ".txt";
            if (!File.Exists(filePath)) File.WriteAllText(filePath, "Car Model: " + _stats + "\n");
            
            if(writeMedian) WriteCarMetricsMedian(_stats);
        }

        public static void WriteCarMetrics(List<float> _carTimes)
        {
            string statsText = "-\n";

            float totalTime = 0;
            for (int i = 0; i < _carTimes.Count; i++)
            {
                totalTime += _carTimes[i];
                statsText += "Lap" + (i+1) + "|" + Utillities.FormatTime(_carTimes[i]) + "|" + _carTimes[i] + "\n";
            }
            statsText += "TotalTime|" + Utillities.FormatTime(totalTime) + "|" + totalTime + "\n";
                
            File.AppendAllText(filePath, statsText);
            
            RestartLevel();
        }
        
        public static void WriteCarMetricsMedian(string _stats)
        {
            filePathMedian = Application.dataPath + "/Metrics/" + SceneManager.GetActiveScene().name + "/" + _stats + "_otherMetrics.txt";
            if (!File.Exists(filePathMedian)) File.WriteAllText(filePathMedian, "Car Model: " + _stats + "\n");
            
            List<float> lapTimes = new List<float>();
            
            List<float> totalTimes = new List<float>();

           float bestLap = 100000f;

            StreamReader inp_stm = new StreamReader(filePath);
            
            while(!inp_stm.EndOfStream)
            {
                string inp_ln = inp_stm.ReadLine( );
                //Debug.Log(inp_ln);
                //Aqui lo que hay que hacer con esa linea
                if (!inp_ln.Equals("-"))
                {
                    string[] lineSplit = inp_ln.Split('|');
                    //Debug.Log(lineSplit.Length);

                    if (lineSplit.Length > 1)
                    {
                        if (lineSplit[0].Equals("TotalTime")) totalTimes.Add(float.Parse(lineSplit[2]));
                        else
                        {
                            lapTimes.Add(float.Parse(lineSplit[2]));
                            Debug.Log(float.Parse(lineSplit[2]));
                            if (bestLap > float.Parse(lineSplit[2])) bestLap = float.Parse(lineSplit[2]);
                        }
                    }
                }
            }

            inp_stm.Close( );

            float media = 0f;

            for (int i = 0; i < lapTimes.Count; i++) media += lapTimes[i];

            media = media / lapTimes.Count;

            float mediana = lapTimes[(int) (lapTimes.Count / 2)];
            
            float mediaTotal = 0f;

            for (int i = 0; i < totalTimes.Count; i++) mediaTotal += totalTimes[i];

            mediaTotal = mediaTotal / totalTimes.Count;
            
            string statsText = "-\n";
            statsText += "Media (Vuelta)|" + Utillities.FormatTime(media) + "|" + media + "\n";
            statsText += "Mediana (Vuelta)|" + Utillities.FormatTime(mediana) + "|" + mediana + "\n";
            statsText += "Media (Circuito)|" + Utillities.FormatTime(mediaTotal) + "|" + mediaTotal + "\n";
            statsText += "Mejor vuelta|" + Utillities.FormatTime(bestLap) + "|" + bestLap + "\n";
            
            File.WriteAllText(filePathMedian, "Car Model: " + statsText + "\n");
        }
        
        public static void RestartLevel()
        {
            if(Time.timeScale != 1f) Time.timeScale = 1f;

            Laderboard.resetLadder();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
