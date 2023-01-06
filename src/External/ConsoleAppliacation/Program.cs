using Domain.Models;
using Domain.Models.Enumerations;
using Microsoft.ML;
using Microsoft.ML.Data;

public class Program
{
    public class Prediction
    {
        [ColumnName("Score")]
        public float Price { get; set; }
    }

    static void Main(string[] args)
    {
        MLContext mlContext = new MLContext();

        Car[] carData = new[]
        {
           new Car() { Make = "Audi", Model = "A4", Body = (int)Body.StationWagon, Fuel = (int)Fuel.Diesel, EmissionStandard = (int)EmissionStandard.Euro6, HorsePower = 150, Mileage = 215000, Year = 2017, Transmission = (int)Transmission.Automatic, Price = 32200 },
           new Car() { Make = "Audi", Model = "A4", Body = (int)Body.StationWagon, Fuel = (int)Fuel.Diesel, EmissionStandard = (int)EmissionStandard.Euro6, HorsePower = 150, Mileage = 190000, Year = 2017, Transmission = (int)Transmission.Automatic, Price = 27350 },
           new Car() { Make = "Audi", Model = "A4", Body = (int)Body.StationWagon, Fuel = (int)Fuel.Diesel, EmissionStandard = (int)EmissionStandard.Euro6, HorsePower = 252, Mileage = 79000, Year = 2017, Transmission = (int)Transmission.Automatic, Price = 38800 },
           new Car() { Make = "Audi", Model = "A4", Body = (int)Body.StationWagon, Fuel = (int)Fuel.Diesel, EmissionStandard = (int)EmissionStandard.Euro6, HorsePower = 150, Mileage = 200000, Year = 2018, Transmission = (int)Transmission.Automatic, Price = 39900 },
           new Car() { Make = "Audi", Model = "A4", Body = (int)Body.StationWagon, Fuel = (int)Fuel.Diesel, EmissionStandard = (int)EmissionStandard.Euro6, HorsePower = 252, Mileage = 124500, Year = 2017, Transmission = (int)Transmission.Automatic, Price = 39900 },
        };

        var model = Train(mlContext, carData);
        //Evaluate(mlContext, model);
        TestSinglePrediction(mlContext, model);
    }

    private static ITransformer Train(MLContext mlContext, Car[] carData)
    {
        IDataView dataView = mlContext.Data.LoadFromEnumerable(carData);

        var pipeline = mlContext.Transforms
            .CopyColumns(outputColumnName: "Label", inputColumnName: nameof(Prediction.Price))
            .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: nameof(Car.Make), inputColumnName: nameof(Car.Make)))
            .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: nameof(Car.Model), inputColumnName: nameof(Car.Model)))
            .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: nameof(Car.Year), inputColumnName: nameof(Car.Year)))
            .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: nameof(Car.Mileage), inputColumnName: nameof(Car.Mileage)))
            .Append(mlContext.Transforms.Concatenate("Features", nameof(Car.Make), nameof(Car.Model), nameof(Car.Year), nameof(Car.Mileage)))
            .Append(mlContext.Regression.Trainers.FastTree());

        var model = pipeline.Fit(dataView);

        return model;
    }

    private static void TestSinglePrediction(MLContext mlContext, ITransformer model)
    {
        var predictionFunction = mlContext.Model.CreatePredictionEngine<Car, Prediction>(model);

        var carSample = new Car
        {
            Make = "Audi",
            Model = "A4",
            Mileage = 215000,
            Year = 2017
        };

        var prediction = predictionFunction.Predict(carSample);
    }

    private static void Evaluate(MLContext mlContext, ITransformer model)
    {

    }
}