using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using UnityEngine.UI;
using System.IO;
using System.Data;

public class DBManager : MonoBehaviour
{
    [SerializeField]
    DataService dataService;

    [SerializeField]
    Text testText;

    void Start()
    {
        dataService = new DataService("test.db");
        var test = dataService._connection.Table<TEST_TABLE>();
        ToConsole (test);
    }

    	private void ToConsole(IEnumerable<TEST_TABLE> people){
		foreach (var person in people) {
			ToConsole(person.ToString());
		}
	}

	private void ToConsole(string msg){
		Debug.Log (msg);
        testText.text = msg;
	}
}

public class TEST_TABLE
{
    [PrimaryKey, AutoIncrement]
	public int ID { get; set; }
	public int HP { get; set; }

    public int ATK { get; set; }

	public override string ToString ()
	{
		return string.Format ("[TEST_TABLE: ID={0}, HP={1}, Age={2}]", ID, HP, ATK);
	}
}
