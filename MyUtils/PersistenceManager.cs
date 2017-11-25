// Used for creating and loading basic save games in Unity game engine
// For now you if you want to change the location for saves and location for reading text files
// then you need to change the static strings directly

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// Future reimplementation: Should a container like this only contain a single obj reference, and not a dictionary?
// Therefore when you serialize an object, you can create a list or array of data containers,
// and pass the list/array object to serializer?
[System.Serializable]
public struct PersistentDataContainer {
    public string name;
    public string version;
    public Dictionary<string, object> objs;

    public PersistentDataContainer(string name, string version) {
        this.name = name;
        this.version = version;
        objs = new Dictionary<string, object>();
    }

    public void AddObject(string objName, object obj) {
        objs.Add(objName, obj);
    }

    public object FetchObject(string objName) {
        object obj;
        objs.TryGetValue(objName, out obj);
        return obj;
    }
}

public static class PersistenceManager {
    // save location on the file system
    public static string savesDataLoc = Application.dataPath + "/../Saves/";
    public static string fileExtension = ".pdc";

    public static void SaveGameBinaryData(string fileName, object obj) {
        try {
            if (!Directory.Exists(savesDataLoc)) {
                Directory.CreateDirectory(savesDataLoc);
            }
        }
        catch (System.Exception e) {
            Debug.LogException(e);
            return;
        }

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(savesDataLoc + fileName + fileExtension, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, obj);
        stream.Close();
    }

    // todo: check pdc file for version
    public static object LoadGameBinaryData(string fileName) {
        try {
            if (!File.Exists(savesDataLoc + fileName + fileExtension)) {
                Debug.LogWarning("LoadDataFromFS -> Returning null:");
                Debug.LogError(" * -> No such file: " + savesDataLoc + fileName + fileExtension);
                return null;
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(savesDataLoc + fileName + fileExtension, FileMode.Open, FileAccess.Read, FileShare.Read);
            object obj = formatter.Deserialize(stream);
            stream.Close();
            return obj;
        }
        catch (System.Exception e) {
            Debug.LogWarning("LoadDataFromFS -> Returning null:");
            Debug.LogError(" * -> " + e);
            return null;
        }
    }

    public static string publicDataLoc = Application.dataPath + "/../Public_Data/";

    /// <summary>
    /// Starts at root of Public_Data, i.e don't start fileName with a forward slash (/)
    /// </summary>
    /// <param name="editor">If playing in editor, this must be true to load the external data from assets dir</param>
    /// <param name="fileName">.ext, e.g .txt</param>
    /// <returns></returns>
    public static string[] LoadTxtFile(bool editor, string fileName) {
        if (editor) {
            // load from assets/public_data (editor)
            Debug.Log("Loading text data from editor");
            string editorPublicDataLoc = publicDataLoc.Remove(publicDataLoc.Length - 16, 3);
            return File.ReadAllLines(editorPublicDataLoc + fileName);
        }
        else {
            // load from public_data (build)
            Debug.Log("Loading text data externally");
            return File.ReadAllLines(publicDataLoc + fileName);
        }
    }

    public static void WriteTxtFile(string file, string[] content) {
        File.WriteAllLines(file, content);
    }
}

// Example usage:
//[System.Serializable]
//public class CrewMember
//{
//	public string name;

//	public CrewMember ( string name )
//	{
//		this.name = name;
//	}
//}

//public class Example
//{
//	public void ExampleRun ()
//	{
//		CrewMember saveCM = new CrewMember( "John Doe" );
//		PersistentDataContainer savePDC = new PersistentDataContainer( "CrewMember", "0.1" );
//		savePDC.AddObject( "cm", saveCM );
//		PersistenceManager.SaveDataToFS( "CrewMember", savePDC );

//		PersistentDataContainer loadPDC = (PersistentDataContainer) PersistenceManager.LoadDataFromFS( "CrewMember" );

//		Debug.Log( "Loaded: " + loadPDC.name + " version: " + loadPDC.version );
//		object tmpCM;
//		loadPDC.objs.TryGetValue( "cm", out tmpCM );
//		CrewMember loadedCM = (CrewMember) tmpCM;
//		Debug.Log( loadedCM.name );

//	}
//}