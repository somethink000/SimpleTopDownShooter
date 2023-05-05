
using UnityEngine;


[CreateAssetMenu(fileName = "mapObj", menuName = "Gameplay/New_mapObj")]
public class mapObj : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _chance;


    public string id => this._id;
    public GameObject ptrfab => this._prefab;
    public int chance => this._chance;

}
