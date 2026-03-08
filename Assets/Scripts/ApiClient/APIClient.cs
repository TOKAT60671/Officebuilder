using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class APIClient : MonoBehaviour
{
    public WebClient webClient;
    //User registration and login
    public async Awaitable<IWebRequestReponse> Register(User user)
    {
        string route = "/account/register";
        string data = JsonConvert.SerializeObject(user, JsonHelper.CamelCaseSettings);

        return await webClient.SendPostRequest(route, data);
    }

    public async Awaitable<IWebRequestReponse> Login(User user)
    {
        string route = "/account/login";
        string data = JsonConvert.SerializeObject(user, JsonHelper.CamelCaseSettings);

        IWebRequestReponse response = await webClient.SendPostRequest(route, data);
        return ProcessLoginResponse(response);
    }

    private IWebRequestReponse ProcessLoginResponse(IWebRequestReponse webRequestResponse)
    {
        switch (webRequestResponse)
        {
            case WebRequestData<string> data:
                Debug.Log("Response data raw: " + data.Data);
                string token = JsonHelper.ExtractToken(data.Data);
                webClient.SetToken(token);
                return new WebRequestData<string>("Succes");
            default:
                return webRequestResponse;
        }
    }
    //World and object management
    public async Awaitable<IWebRequestReponse> GetWorlds()
    {
        string route = "/Worlds";

        IWebRequestReponse webRequestResponse = await webClient.SendGetRequest(route);
        return ParseWorldList(webRequestResponse);
    }

    public async Awaitable<IWebRequestReponse> CreateWorld(APIWorld world)
    {
        string route = "/Worlds";
        string data = JsonConvert.SerializeObject(world, JsonHelper.CamelCaseSettings);

        return await webClient.SendPostRequest(route, data);
    }

    public async Awaitable<IWebRequestReponse> DeleteWorld(Guid WorldId)
    {
        string route = $"/Worlds?WorldId={WorldId}";
        return await webClient.SendDeleteRequest(route);
    }
    private IWebRequestReponse ParseWorldList(IWebRequestReponse webRequestResponse)
    {
        switch (webRequestResponse)
        {
            case WebRequestData<string> data:
                Debug.Log("Response data raw: " + data.Data);
                List<APIWorld> worlds = JsonConvert.DeserializeObject<List<APIWorld>>(data.Data);
                WebRequestData<List<APIWorld>> parsedWebRequestData = new WebRequestData<List<APIWorld>>(worlds);
                return parsedWebRequestData;
            default:
                return webRequestResponse;
        }
    }
    public async Awaitable<IWebRequestReponse> LoadObjects(Guid WorldId)
    {
        // Include WorldId as query parameter so the backend receives it
        string route = $"/objects?WorldId={WorldId}";

        IWebRequestReponse webRequestResponse = await webClient.SendGetRequest(route);
        return ParseObjectListResponse(webRequestResponse);
    }

    public async Awaitable<IWebRequestReponse> SaveObjects(Guid WorldId, List<APIObject> objects)
    {
        // Include WorldId as query parameter so the backend can associate the posted objects
        string route = $"/objects?WorldId={WorldId}";
        string data = JsonConvert.SerializeObject(objects, JsonHelper.CamelCaseSettings);

        return await webClient.SendPostRequest(route, data);
    }
    private IWebRequestReponse ParseObjectListResponse(IWebRequestReponse webRequestResponse)
    {
        switch (webRequestResponse)
        {
            case WebRequestData<string> data:
                Debug.Log("Response data raw: " + data.Data);
                List<APIObject> objects = JsonConvert.DeserializeObject<List<APIObject>>(data.Data);
                WebRequestData<List<APIObject>> parsedData = new WebRequestData<List<APIObject>>(objects);
                return parsedData;
            default:
                return webRequestResponse;
        }
    }
}

