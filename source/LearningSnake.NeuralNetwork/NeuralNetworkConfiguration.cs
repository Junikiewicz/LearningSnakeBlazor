namespace LearningSnake.NeuralNetwork
{
    public class NeuralNetworkConfiguration
    {
        public int OutputNodes { get; set; }

        public int InputNodes { get; set; }

        public int HiddenNeuronLayers { get; set; }

        public int NeuronsPerHiddenLayer { get; set; }

        public ActivactionFunction HiddenLayersActivacionFunction { get; set; }

        public ActivactionFunction OutputLayerActivactionFunction { get; set; }

        public static bool HaveSameValues(NeuralNetworkConfiguration n1, NeuralNetworkConfiguration n2)
        {
            return (n1.OutputNodes == n2.OutputNodes) &&
                (n1.InputNodes == n2.InputNodes) &&
                (n1.HiddenNeuronLayers == n2.HiddenNeuronLayers) &&
                (n1.NeuronsPerHiddenLayer == n2.NeuronsPerHiddenLayer) &&
                (n1.HiddenLayersActivacionFunction == n2.HiddenLayersActivacionFunction) &&
                (n1.OutputLayerActivactionFunction == n2.OutputLayerActivactionFunction);
        }
    }
}