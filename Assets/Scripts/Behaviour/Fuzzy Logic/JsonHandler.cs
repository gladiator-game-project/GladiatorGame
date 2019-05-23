using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Behaviour.Fuzzy_Logic
{
    public class JsonHandler<T>
    {
        public static T GetFromJson(string fileName)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                return JsonUtility.FromJson<T>(dataAsJson);
            }
            else
                Debug.Log("File not Found");

            return default;
        }
    }
}
