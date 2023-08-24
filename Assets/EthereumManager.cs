using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Numerics;
using UnityEngine;

public class EthereumManager : MonoBehaviour
{
    public string nodeUrl = "https://avalanche-fuji.infura.io/v3/9adf983e23e74d2cba129f28a81d699a";
    public string accountAddress = "0x01e24d130cf4c5599954115c5026276b4797a171";
    public string contractAddress = "0x703A7c280Ea07749Ba5D72973Bc3B4805915493b";
    public string abi = @"{
                        'inputs': [{'internalType': 'address','name': 'account','type': 'address'}],
                        'name': 'getBalance',
                        'outputs': [{'internalType': 'uint256','name': '','type': 'uint256'}],
                        'stateMutability': 'view',
                        'type': 'function'
                    }";

    async void Start()
    {
        // Initialize Web3
        Web3 web3 = new Web3(nodeUrl);

        // Get the ETH balance
        var balance = await web3.Eth.GetBalance.SendRequestAsync(accountAddress);
        decimal etherAmount = Web3.Convert.FromWei(balance.Value);

        // Output the balance
        Debug.Log($"Balance of address {accountAddress}: {etherAmount} ETH");

        // If you also want to interact with a smart contract to check a token balance
        var contract = web3.Eth.GetContract(abi, contractAddress);

        // Assuming the contract has a standard ERC20 'balanceOf' function
        var balanceOfFunction = contract.GetFunction("balanceOf");
        var tokenBalance = await balanceOfFunction.CallAsync<BigInteger>(accountAddress);

        // Output the token balance
        Debug.Log($"Token Balance of address {accountAddress}: {tokenBalance}");
    }
}
