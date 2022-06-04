using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : SingletonPersistent<InterfaceManager>
{
    public List<InterfaceObject> RegisteredPrefabs = new List<InterfaceObject>();
    public Dictionary<string, GameObject> InterfaceElements = new Dictionary<string, GameObject>();

    public Canvas Canvas;

    public bool CreateInterfaceObject(string _Name, out GameObject _InterfaceObject)
    {
        _InterfaceObject = null;

        if(Canvas == null)
        {
            return false;
        }

        if(InterfaceElements.ContainsKey(_Name))
        {
            _InterfaceObject = InterfaceElements[_Name];
            return true;
        }

        if(!RegisteredPrefabs.Any(o => o.Name == _Name))
        {
            Debug.LogError($"The Interface Manager does not have a prefab registered for: {_Name}");
            return false;
        }

        _InterfaceObject = GameObject.Instantiate(RegisteredPrefabs.First(o => o.Name == _Name).Prefab, Canvas.transform);

        InterfaceElements.Add(_Name, _InterfaceObject);

        return true;
    }

    public bool CreateInterfaceObject(string _Name, Vector3 _Position, out GameObject _InterfaceObject)
    {
        _InterfaceObject = null;

        if (Canvas == null)
        {
            return false;
        }

        if (InterfaceElements.ContainsKey(_Name))
        {
            _InterfaceObject = InterfaceElements[_Name];
            return true;
        }

        if (!RegisteredPrefabs.Any(o => o.Name == _Name))
        {
            Debug.LogError($"The Interface Manager does not have a prefab registered for: {_Name}");
            return false;
        }

        _InterfaceObject = GameObject.Instantiate(RegisteredPrefabs.First(o => o.Name == _Name).Prefab, Canvas.transform);

        InterfaceElements.Add(_Name, _InterfaceObject);

        _InterfaceObject.transform.position = _Position;

        return true;
    }

    public bool DestroyInterfaceObject(string _Name)
    {
        if (!InterfaceElements.ContainsKey(_Name)) return false;

        GameObject obj = InterfaceElements[_Name];

        InterfaceElements.Remove(_Name);

        Destroy(obj);

        return true;
    }
}

[System.Serializable]
public class InterfaceObject
{
    public string Name;
    public GameObject Prefab;
}
