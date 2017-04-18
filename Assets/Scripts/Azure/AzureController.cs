using UnityEngine;
using System;
using System.Net;
using System.Collections.Generic;
using RestSharp;
using Pathfinding.Serialization.JsonFx;
using Unity3dAzure.AppServices;
using UnityEngine.UI;
//using Tacticsoft;
//using Prefabs;
using UnityEngine.SceneManagement;

public class AzureController : MonoBehaviour//, ITableViewDataSource 
{
	// This script handles the communication with the azure app service

	// App URL
	[SerializeField] // Forces unity to serialise this field
	private string APP_URL = "http://dangerzoneabertay.azurewebsites.net";

	// App Service REST Client
	private MobileServiceClient _client;

	// App Service Tables defined using a DataModel
	private MobileServiceTable<SpawnFlag> _SFtable;

    // Local spawn flag model for updating the server
    private SpawnFlag purge = new SpawnFlag();
	private SpawnFlag shield = new SpawnFlag();
	private SpawnFlag shock = new SpawnFlag();

	// These bools have final say on whether to spawn power ups or not
    bool spawnPurge, spawnShield, spawnShock;

    // Prefabs
    public GameObject ShieldPrefab;
	public GameObject PurgePrefab;
	public GameObject ShockwavePrefab;

    // Use this for initialization
    void Start () 
	{
		// Create App Service client (Using factory Create method to force 'https' url)
		_client = MobileServiceClient.Create(APP_URL); // new MobileServiceClient(APP_URL);

		// Get App Service 'SpawnFlags' table
		_SFtable = _client.GetTable<SpawnFlag>("SpawnFlags");

        // Purge false
        spawnPurge = false;
		purge.name = "Purge";
		purge.flag = false;

		// Shock false
		spawnShock = false;
		shock.name = "Shockwave";
		shock.flag = false;

		// Shield false
		spawnShield = false;
		shield.name = "Shield";
		shield.flag = false;

        // Check server whether to spawn tree every 1 second
        InvokeRepeating("CheckTables", 0.0f, 1.0f);
    }
	
	// Update is called once per frame
	void Update () 
	{
        
	}
		
    private void CheckTables()
    {
        Debug.Log("Checking Flags");

        // Read from both tables
		Read(_SFtable);

		// Check bools to spawn power ups
        if (spawnShield)
        {
			Debug.Log ("Spawning Shield");
            // Spawn tree
            Vector3 SpawnLocation = new Vector3(0.0f, 0.0f, 0.0f);
            Instantiate(ShieldPrefab, SpawnLocation, Quaternion.identity);

            // We now need to reset the flag 
            spawnShield = false;
            shield.flag = false;
            UpdateFlag(_SFtable, shield);
        }

		if (spawnPurge)
		{
			// Spawn tree
			Vector3 SpawnLocation = new Vector3(0.0f, 0.0f, 0.0f);
			Instantiate(PurgePrefab, SpawnLocation, Quaternion.identity);

			// We now need to reset the flag 
			spawnPurge = false;
			purge.flag = false;
			UpdateFlag(_SFtable, purge);
		}

		if (spawnShock)
		{
			// Spawn tree
			Vector3 SpawnLocation = new Vector3(0.0f, 0.0f, 0.0f);
			Instantiate(ShockwavePrefab, SpawnLocation, Quaternion.identity);

			// We now need to reset the flag 
			spawnShock = false;
			shock.flag = false;
			UpdateFlag(_SFtable, shock);
		}
    }

	// REST FUNCTIONS =========================================================================================

	public void Lookup(MobileServiceTable<SpawnFlag> table, SpawnFlag item)
	{
		table.Lookup<SpawnFlag>(item.id, OnLookupCompleted);
	}

	private void OnLookupCompleted(IRestResponse<SpawnFlag> response)
	{
		Debug.Log("OnLookupItemCompleted: " + response.Content);
		if (response.StatusCode == HttpStatusCode.OK)
		{
			SpawnFlag item = response.Data;
			if (item.flag == true)
			{
//				spawn = true;
			}
		}
		else
		{
			ResponseError err = JsonReader.Deserialize<ResponseError>(response.Content);
			Debug.Log("Lookup Error Status:" + response.StatusCode + " Code:" + err.code.ToString() + " " + err.error);
		}
	}


	public void Insert(MobileServiceTable<SpawnFlag> table, SpawnFlag item)
	{ // Inserts new spawn flag into Azure table

        if (Validate(item))
        {
            table.Insert<SpawnFlag>(item, OnInsertCompleted);
        }
	}

	private void OnInsertCompleted(IRestResponse<SpawnFlag> response)
	{
		if (response.StatusCode == HttpStatusCode.Created)
		{
			Debug.Log( "OnInsertItemCompleted: " + response.Data );
			// Which power up was inserted?
			if (response.Data.name == "Purge")
				purge = response.Data; // If inserted successfully, azure will return an ID
			else if (response.Data.name == "Shield")
				shield = response.Data;
			else if (response.Data.name == "Shockwave")
				shock = response.Data;
		}
		else
		{
			Debug.Log("Insert Error Status:" + response.StatusCode + " Uri: "+response.ResponseUri );
		}
	}

	public void Delete(MobileServiceTable<SpawnFlag> table, SpawnFlag item)
    {
        // Deletes spawn flag from azure table
        if (Validate(item))
        {
            table.Delete<SpawnFlag>(item.id, OnDeleteCompleted);
        }
       
    }

    private void OnDeleteCompleted(IRestResponse<SpawnFlag> response)
    {
        if (response.StatusCode == HttpStatusCode.OK)
        {
            Debug.Log("OnDeleteItemCompleted");
        }
        else
        {
            Debug.Log("Delete Error Status:" + response.StatusCode + " " + response.ErrorMessage + " Uri: " + response.ResponseUri);
        }
    }

	public void UpdateFlag(MobileServiceTable<SpawnFlag> table, SpawnFlag item)
    {
        // Updates item in the table
        if (Validate(item))
        {
            table.Update<SpawnFlag>(item, OnUpdateFlagCompleted);
        }
    }

    private void OnUpdateFlagCompleted(IRestResponse<SpawnFlag> response)
    {
        if (response.StatusCode == HttpStatusCode.OK)
        {
            Debug.Log("OnUpdateItemCompleted: " + response.Content);
        }
        else
        {
            Debug.Log("Update Error Status:" + response.StatusCode + " " + response.ErrorMessage + " Uri: " + response.ResponseUri);
        }
    }

	public void Read(MobileServiceTable<SpawnFlag> table)
	{
		table.Read<SpawnFlag>(OnReadCompleted);
	}

	private void OnReadCompleted(IRestResponse<List<SpawnFlag>> response)
	{
		if (response.StatusCode == HttpStatusCode.OK)
		{
			Debug.Log("OnReadCompleted data: " + response.ResponseUri +" data: "+ response.Content);
			List<SpawnFlag> items = response.Data;
			Debug.Log("Read items count: " + items.Count);

			foreach (SpawnFlag item in items) 
			{
				if (item.name == "Purge") 
				{
					purge.id = item.id;
					if (item.flag == true) 
					{
						spawnPurge = true;
					}
				} else if (item.name == "Sheild") 
				{
					shield.id = item.id;
					if (item.flag == true) 
					{
						spawnShield = true;
					}
				} else if (item.name == "Shockwave") 
				{
					shock.id = item.id;
					if (item.flag == true) 
					{
						spawnShock = true;
					}
				}
			}

		}
		else
		{
			Debug.Log("Read Error Status:" + response.StatusCode + " Uri: "+response.ResponseUri );
		}
	}

    // Checks if flag is valid before sending
    private bool Validate(SpawnFlag flag)
    {
        bool isUsernameValid = true;//, isScoreValid = true;
        // Validate username
        if (String.IsNullOrEmpty(flag.name))
        {
            isUsernameValid = false;
            Debug.Log("Error, player username required");
        }

        return (isUsernameValid);// && isScoreValid);
    }
}