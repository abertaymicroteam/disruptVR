using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using UnityEngine.Networking;
using Unity3dAzure.StorageServices;

/// <summary>
/// This tracks the storage account for new voice clips and plays them to the player
/// </summary>

[RequireComponent(typeof(AudioSource))]
public class Communicator : MonoBehaviour
{
	// Azure Storage Service Components
	[SerializeField]
	private string ACC_NAME = "dangerzonecommunication"; // Account Name
	[SerializeField]
	private string KEY = "oxNwpGrXuExAaY33F/4cgcXyA/RxwYFW5oWntusg0DzYePg3ZYyK/D2lxPzIaIHHbr0ckBdcQL5DEWmbUYcodQ=="; // Access Key

	// Storage REST Client and Service
	private StorageServiceClient _client;
	private BlobService _service;
	BlobResults latestResults = new BlobResults();

	// Things for conditions
	int lastKnownResults = 0;
	bool currentlyReading = false;

	// Audio Sources
    private AudioSource voiceInputAudio;

	private float readTimer = 0.05f; // Roughly every 3 frames

    void Start()
    {
		// Create Storage Service client with name and access key
		_client = new StorageServiceClient(ACC_NAME, KEY);

		// Create Service from client
		_service = _client.GetBlobService();

		// Create Audio Sources 
        voiceInputAudio = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
		if (latestResults.Blobs != null)
		{ // If there is more results than previously
			if (latestResults.Blobs.Length > lastKnownResults)
			{
				// Load and play new audio clip
				AudioSource newSource = gameObject.AddComponent<AudioSource> ();
				LoadAudioClip (_client.PrimaryEndpoint() + "voiceaudio/" + latestResults.Blobs[lastKnownResults].Name, newSource, latestResults.Blobs[lastKnownResults].Name);
				lastKnownResults = latestResults.Blobs.Length;
			}
		}

		// Read from Azure for new clips at set intervals
		if (readTimer > 0) 
		{
			readTimer -= Time.deltaTime;
		}
		else
		{
			if (!currentlyReading)
			{
				ReadAllAudio ("voiceaudio");
				readTimer = 0.05f;
			}
		}
    }

	// Audio/REST Functions
	public void ReadAllAudio(string containerName)
	{
		StartCoroutine(_service.ListBlobs(ReadAudioCompleted, containerName));
		currentlyReading = true;
	}

	private void ReadAudioCompleted(IRestResponse<BlobResults> response)
	{
		if (response.IsError)
		{
			Debug.Log("Read Audio Error Status: " + response.StatusCode + " Message: " + response.ErrorMessage);
		}
		else
		{
			Debug.Log ("Read Audio Completed");
			latestResults = response.Data;
			Debug.Log ("Read Audio Count: " + latestResults.Blobs.Length);
			currentlyReading = false;
		}
	}

	public void LoadAudioClip(string url, AudioSource src, string name)
	{
		StartCoroutine (LoadAudioURL (url, src, name));
	}

	public IEnumerator LoadAudioURL(string url, AudioSource src, string name)
	{
		UnityWebRequest www = UnityWebRequest.GetAudioClip (url, AudioType.WAV);
		yield return www.Send ();
		if (www.isError) {
			Debug.Log ("Error Loading Audio from URL: " + www.error);
		} 
		else 
		{
			AudioClip clip = ((DownloadHandlerAudioClip)www.downloadHandler).audioClip;
			src.clip = clip;
			src.Play ();
			DeleteAudioClip ("voiceaudio", name); // Delete clip from azure once played
		}
	}

	public void DeleteAudioClip(string containerName, string name)
	{
		StartCoroutine (_service.DeleteBlob(DeleteAudioCompleted, containerName, name));
	}

	private void DeleteAudioCompleted(RestResponse response)
	{
		if (response.IsError)
		{
			Debug.Log ("Delete blob error! Status: " + response.StatusCode + " Message: " + response.ErrorMessage);
			return;
		}
		Debug.Log("Deleted Blob! Status: " + response.StatusCode);
	}
}