using System.Collections.Generic;
using System.IO;
using Controller;
using Core.Networking;
using UnityEngine;
using View;

namespace Manager
{
    // ReSharper disable once IdentifierTypo
    public class Gamemanager : MonoBehaviour
    {
        [SerializeField] private CardListMenuView view;
        private void Awake()
        {
#pragma warning disable CS4014
            Internet.StartConnectionCheckService();
#pragma warning restore CS4014

            List<string> inputData = LoadInputData();
            if (inputData is not null)
            {
                // ReSharper disable once ObjectCreationAsStatement
                new CardListMenuController(view, inputData);
            }
        }

        private List<string>  LoadInputData()
        {
            string path = Application.streamingAssetsPath + "/pokemon.txt";
            if (!File.Exists(path))
            {
                view.DisplayMessage("Input file not found", false);
                return null;
            }
            var input =File.ReadAllLines(path);
            return new List<string>(input);
        }
    }
}