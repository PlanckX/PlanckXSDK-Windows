using Planckx.Sdk.Bean;
using Planckx.Sdk.Client;
using Planckx.Sdk.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Planckx.Sdk.Bean.ResponseCode;

namespace Planckx.Example
{
    static class PlanckxExample
    {
        static void Main()
        {
            string key = "JISQ97ih";
            string secretKey = "3e04178595c34ee6131f10a81bbdb99fbb421e8b";

            PlanckxAccountClient account = PlanckxClient.AccountClient(key, secretKey);
            ResponseEnum enum1 = account.BindState("123456", out CheckBind bind);
            Console.WriteLine("Response: {0}, {1}", enum1, bind);
            Console.WriteLine("------------------------");


            PlanckxNftClient nftClient = PlanckxClient.NftClient(key, secretKey);
            enum1 = nftClient.AllNfts(out IList<NftAsset> result);
            Console.WriteLine("Response: {0}", enum1);
            if (result != null)
            {
                foreach (NftAsset asset in result)
                {
                    Console.WriteLine("{0}", asset.ToString());
                }
            }
            Console.WriteLine("------------------------");

            enum1 = nftClient.NftByPlayer("123456", out IList<NftAsset> result1);
            Console.WriteLine("Response: {0}", enum1);
            if (result1 != null)
            {
                foreach (NftAsset asset in result1)
                {
                    Console.WriteLine("{0}", asset.ToString());
                }
            }
            Console.WriteLine("------------------------");

            enum1 = nftClient.NftByTokenId("123456", out NftAsset result2);
            Console.WriteLine("Response: {0}", enum1);
            if (result2 != null)
                Console.WriteLine("{0}", result2.ToString());

        }

    }
}
