using System;
using NN.Core;
using NN.Saver;
using UnityEngine;

namespace NN.Samples.MNIST.NN
{
    public class NeuralNetworkProvider : MonoBehaviour
    {
        [SerializeField] private NeuralNetworkSaverUnity saver;

        private NeuralNetwork nn;

        public NeuralNetwork NN => nn;

        private void Awake()
        {
            nn = saver.Load();
        }
    }
}
