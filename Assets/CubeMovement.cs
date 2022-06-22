using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine.Networking;
using SimpleJSON;
using Newtonsoft.Json;
using static EVM;

[System.Serializable]
public class BuildingData
{
    public int BuildingId;
    public Vector3 position;
}
[System.Serializable]
public class BuildingDetails
{
    public List<BuildingData> buildings = new List<BuildingData>();
}
public class CubeMovement : MonoBehaviour
{
    public GameObject[] buildings;
    [SerializeField]
    public BuildingDetails buildinglist = new BuildingDetails();
    public Button save_Btn;

    void RefreshBuilding(Vector3 pos)
    {
        buildinglist.buildings.RemoveAll(x=>x.position == pos);
    }
    JSONArray dataarr;
    private async Task Start()
    {
        Debug.LogError("start running");
        save_Btn.interactable = false;
        GetDataFromUrl("https://ssgame.000webhostapp.com/metaweb3/api/controller.php?control=1&id=" + PlayerPrefs.GetString("Account"));
    }

    public async Task GetDataFromUrl(string id)
    {
        string url = id;
        Debug.Log(url);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            await webRequest.SendWebRequest();
            Debug.Log(url);
            Debug.Log(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
            SetBuildingData(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
        }
    }
    public async Task SaveBuildingDetails(string id)
    {
        string url = id;
        Debug.Log(url);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            await webRequest.SendWebRequest();
        }
    }

    //    [DllImport("__Internal")]
    //  private static extern void GetBuildingData(string id);
    public void SetBuildingData(string data)
    {
        buildinglist = JsonConvert.DeserializeObject<BuildingDetails>(data);
        if(buildinglist != null)
            if(buildinglist.buildings != null)
            {
                foreach(BuildingData buildingData in buildinglist.buildings)
                {
                        GameObject go = Instantiate(buildings[buildingData.BuildingId]);
                        go.transform.position = buildingData.position;
                }
            }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.transform.position.x > -4)
                this.transform.position = new Vector3(this.transform.position.x - 0.01f, this.transform.position.y, this.transform.position.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (this.transform.position.x < 4)
                this.transform.position = new Vector3(this.transform.position.x + 0.01f, this.transform.position.y, this.transform.position.z);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (this.transform.position.z < 4)
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.01f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (this.transform.position.z > -4)
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.01f);
        }
        if(this.GetComponent<MeshRenderer>().material.color == Color.white)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                save_Btn.interactable = true;
                GameObject go = Instantiate(buildings[0]);
                go.transform.position = this.transform.position;
                BuildingData data = new BuildingData();
                data.position = go.transform.position;
                data.BuildingId = 0;
                buildinglist.buildings.Add(data);
            }
            else
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                save_Btn.interactable = true;
                GameObject go = Instantiate(buildings[1]);
                go.transform.position = this.transform.position;
                BuildingData data = new BuildingData();
                data.position = go.transform.position;
                data.BuildingId = 1;
                buildinglist.buildings.Add(data);
            }
            else
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                save_Btn.interactable = true;
                GameObject go = Instantiate(buildings[2]);
                go.transform.position = this.transform.position;
                BuildingData data = new BuildingData();
                data.position = go.transform.position;
                data.BuildingId = 2;
                buildinglist.buildings.Add(data);
            }
        }
        else
            if (Input.GetKeyDown(KeyCode.R))
        {
            RemoveBuilding();
        }
    }
    public void SaveBuildingData()
    {
        string data = JsonConvert.SerializeObject(buildinglist); ;

        SaveBuildingDetails("https://ssgame.000webhostapp.com/metaweb3/api/controller.php?control=2&data=" + data + "&id=" + PlayerPrefs.GetString("Account"));
    }
    public void RemoveBuilding()
    {
        if (this.GetComponent<MeshRenderer>().material.color == Color.red)
        {
            save_Btn.interactable = true;
            RefreshBuilding(lastSelecte.transform.position);
            Destroy(lastSelecte);
            this.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
    public GameObject lastSelecte;
    private void OnTriggerStay(Collider other)
    {
        lastSelecte = other.gameObject;
        this.GetComponent<MeshRenderer>().material.color = Color.red;
    }
    private void OnTriggerExit(Collider other)
    {
        this.GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
