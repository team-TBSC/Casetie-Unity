using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ConcertHall : MonoBehaviour
{
    public GameObject Hall;
    public Texture ballad;
    public Texture hiphop;
    public Texture dance;
    public Texture rock;
    public Texture trot;
    public Texture indie;
    public Texture pop;
    public Texture none;

    private Texture texture;

    [System.Serializable]
    public class Api
    {
        public string name;
        public string result;
    }
    
    public void hallUpdate(){
        StartCoroutine(concertHallUpdate());
    }

    IEnumerator concertHallUpdate(){
        UnityWebRequest request = UnityWebRequest.Get("http://digger.works:12023/getLastDB");

        yield return request.SendWebRequest();
        
        if (request.result == UnityWebRequest.Result.Success)
        {
            string data = request.downloadHandler.text;
            Api apiResult = JsonUtility.FromJson<Api>(data);

            if(apiResult.result[2].Equals('0')) texture = rock;
            else if(apiResult.result[2].Equals('1')) texture = ballad;
            else if(apiResult.result[2].Equals('2')) texture = indie;
            else if(apiResult.result[2].Equals('3')) texture = trot;
            else if(apiResult.result[2].Equals('4')) texture = hiphop;
            else if(apiResult.result[2].Equals('5')) texture = dance;
            else if(apiResult.result[2].Equals('6')) texture = pop;
            else texture = none;

            Renderer renderer;
            renderer = Hall.transform.Find("pPlane11").GetComponent<Renderer>();
            renderer.material.mainTexture = texture;
            renderer = Hall.transform.Find("group5/polySurface429").GetComponent<Renderer>();
            renderer.material.mainTexture = texture;
            renderer = Hall.transform.Find("group5/polySurface431").GetComponent<Renderer>();
            renderer.material.mainTexture = texture;
            renderer = Hall.transform.Find("group5/polySurface453").GetComponent<Renderer>();
            renderer.material.mainTexture = texture;
            renderer = Hall.transform.Find("group5/polySurface454").GetComponent<Renderer>();
            renderer.material.mainTexture = texture;
            renderer = Hall.transform.Find("group5/polySurface455").GetComponent<Renderer>();
            renderer.material.mainTexture = texture;
            renderer = Hall.transform.Find("ConcertHallBannerRigged/Geometry/ConcertHall/pPlane10").GetComponent<Renderer>();
            renderer.material.mainTexture = texture;
            renderer = Hall.transform.Find("ConcertHallBannerRigged2/Geometry/ConcertHall/pPlane10").GetComponent<Renderer>();
            renderer.material.mainTexture = texture;
        }
    }
}
