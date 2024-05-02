using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CSVFile;
using planet3.rest_api.model;
using UnityEngine;
using UnityEngine.UI;

public class LoadData : MonoBehaviour
{
    private static readonly string CountriesDbUrl =
    Application.dataPath + "/AssetsPlanet3/Script/plugins/country-coord.csv";

    public static Dictionary<Coordinate, Country> Countries { get; private set; } = new();

    // Start is called before the first frame update
    private void Awake()
    {
        ReadCsvFile();
    }

    private static void ReadCsvFile()
    {
        using var csvReader = CSVReader.FromFile(CountriesDbUrl);
        foreach (var line in csvReader) {
            var coordinates = new Coordinate(float.Parse(line[4], CultureInfo.InvariantCulture), float.Parse(line[5], CultureInfo.InvariantCulture));
            var country = new Country(line[0], line[1], line[2], coordinates);
            Countries.Add(country.Coordinates, country);
        }
    }
}