using System;
using NN.Core;
using NN.Saver;
using NN.Training;
using NN.Training.Data;
using NN.Training.Propagation;
using UnityEngine;
using UnityEngine.Events;

namespace NN.Samples.MNIST.NN
{
    public class BackPropagationController : MonoBehaviour
    {
        [SerializeField] private int startIterationCapacity = 100;
        [SerializeField] private NeuralNetworkSaverUnity nnSaver;

        private BackPropagation propagation;
        private DataSetEnumerator dataSetEnumerator;
        private GenerationEnumerator generationEnumerator;
        
        [Space]
        [SerializeField] private UnityEvent<int> iterationChanged;
        [SerializeField] private UnityEvent<int> dataSetsInIterationCountChanged;
        [SerializeField] private UnityEvent<int> dataSetsOverallCountChanged;
        [SerializeField] private UnityEvent<int> NNanswered;
        [SerializeField] private UnityEvent<float> iterationErrorChanged;
        [SerializeField] private UnityEvent<float> currentNNErrorChanged;

        private void Awake()
        {
            //Parser here
            generationEnumerator = new GenerationEnumerator(startIterationCapacity);//add here Action<T>
            dataSetEnumerator = new DataSetEnumerator();//and here
        }
        private void Start()
        {
            propagation = new BackPropagation(nnSaver.Load());
            //async study here
        }
    }
}
