using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using System;
using Unity.Services.Core;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class Authentication : MonoBehaviour
{
    [SerializeField]
    UIDocument UIdoc;
    VisualElement root;
    void OnEnable()
    {
        root = UIdoc.rootVisualElement;
    }

    async void Start()
    {
       
        Button signin = root.Q<Button>("anonymousSign");
        Label playerId = root.Q<Label>("playerIdValue");
        Label accessToken = root.Q<Label>("accessTokenValue");
        VisualElement playerInfoContainer = root.Q<VisualElement>("playerInfoContainer");
        signin.clicked += () =>
        {
            OnClickSignIn();
        };
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            const string successMessage = "Sign in anonymously succeeded!";
            Debug.Log(successMessage);
            playerId.text = $"PlayerID: {AuthenticationService.Instance.PlayerId}";
            accessToken.text = $"Access Token: {AuthenticationService.Instance.AccessToken}";
            signin.style.display = DisplayStyle.None;
            playerInfoContainer.style.display = DisplayStyle.Flex;
            //playerId.text = $"PlayedID: {AuthenticationService.Instance.PlayerId}";
            //accessToken.text = $"Access Token: {AuthenticationService.Instance.AccessToken}";
        };

    }

    public async void OnClickSignIn()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log(await AuthenticationService.Instance.GetPlayerInfoAsync());
        }
        catch (Exception e) 
        {
            Debug.LogException(e);
        }
    }
}
