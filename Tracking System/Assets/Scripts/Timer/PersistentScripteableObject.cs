using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PersistentScripteableObject : ScriptableObject{
    public void Safe(string fileName=null){
        var bf= new BinaryFormatter();
        var file = File.Create(getPath(fileName));
        var json=JsonUtility.ToJson(this);
        bf.Serialize(file, json);
        file.Close();
    }
    public void Load(string fileName=null){
        if (File.Exists(getPath(fileName))){
            var bf= new BinaryFormatter();
            var file = File.Open(getPath(fileName),FileMode.Open);

            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), this);
            file.Close();
        }
    }
    private string getPath(string fileName = null)
    {
        var fullFileName = string.IsNullOrEmpty(fileName) ? name : fileName;
        return string.Format("{0}/{1}",Application.persistentDataPath, fullFileName);
    }
}
