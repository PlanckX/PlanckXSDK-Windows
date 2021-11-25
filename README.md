# PlanckX SDK for CSharp

------
[![License](https://img.shields.io/badge/License-MIT-green)](https://raw.githubusercontent.com/PlanckX/PlanckXSDK-Windows/master/LICENSE)

## Welcome to the PlanckX Studio SDK！
> The PlanckX Studio SDK contains the basic SDK tools. You can embed the SDK into your game creation to support the mint and issuance of NFT (Non-fungible-token) assets in your game creation, Match the PlanckX platform account with your game account, And link the NFT assets holder（Usually the player who bought the NFT） by the asset owner to the game for use.


## First step:
> * Go to the **[PlanckX Studio](https://studio.planckx.io)** to register.
> * Get **APIKey** and **SecretKey**.

## Second step:
> * Import **[Planckx.dll](https://github.com/PlanckX/PlanckXSDK-Windows/releases)** reference.

## Example Usage
> * This example check a game player is bound to a PlanckX account:
```csharp
    string apiKey = "Go to the PlanckX studio website to get";
    string secretKey = "Go to the PlanckX studio website to get";

    PlanckxAccountClient account = PlanckxClient.AccountClient(apiKey, secretKey);
    ResponseEnum respEnum = account.BindState(PlayerId, out CheckBind bind);
    Console.WriteLine("Response: {0}, {1}", respEnum, bind);
    
```

> * This example gets NFT assets:
```csharp
    string apiKey = "Go to the PlanckX studio website to get";
    string secretKey = "Go to the PlanckX studio website to get";

    PlanckxNftClient nftClient = PlanckxClient.NftClient(apiKey, secretKey);
    
    // Get all Nfts
    nftClient.AllNfts(out IList<NftAsset> result);
    
    // Get a player's Nfts
    nftClient.NftByPlayer("Player id", out IList<NftAsset> result);
    
    // Get Nft by TokenId
    nftClient.NftByTokenId("Token Id", out NftAsset result);
    
    // Process your bussiness ....
```

## Other
> * Supports .NET Core 3.1+
> * [Release Notes](https://github.com/PlanckX/PlanckXSDK-Windows/releases)


