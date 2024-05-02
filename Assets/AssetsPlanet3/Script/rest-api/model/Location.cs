using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planet3.rest_api.model
{

    public class Country
    {
        public string Name { get; set; }
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public Coordinate Coordinates { get; set; }
        public List<Town> Towns { get; private set; } = new List<Town>();

        public Country()
        {
            
        }

        public Country(string name, string alpha2code, string alpha3code, Coordinate coordinates)
        {
            this.Name = name;
            this.Alpha2Code = alpha2code;
            this.Alpha3Code = alpha3code;
            this.Coordinates = coordinates;
        }

        public override string ToString()
        {
            return Name + " " + Alpha2Code + " " + Alpha3Code + "\n" + Coordinates;
        }

        public void addTown(Town town)
        {
            Towns.Add(town);
        }

        public void removeTown(Town town)
        {
            Towns.Remove(town);
        }

        public void clearTowns()
        {
            Towns.Clear();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Country c = (Country)obj;
            return Name.Equals(c.Name);
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
    public class Town
    {
        public string Name { get; set; }
        public string LocalName { get; set; }
        public Country Country { get; set; }
        public Coordinate Coordinates { get; set; }

        public Town()
        {
            
        }

        public Town(string name, string localName, Country country, Coordinate coordinates)
        {
            this.Name = name;
            this.LocalName = localName;
            this.Country = country;
            this.Coordinates = coordinates;
        }

        public override string ToString()
        {
            return Name + " " + LocalName + "\n" + Country + "\n" + Coordinates;
        }
    }
    public class Coordinate
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public Coordinate()
        {
            
        }

        public Coordinate(float latitude, float longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public override string ToString() { return Latitude + " " + Longitude;}

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Coordinate c = (Coordinate)obj;

            return Latitude == c.Latitude && Longitude == c.Longitude;
        }

        public override int GetHashCode()
        {
            return Latitude.GetHashCode() + Longitude.GetHashCode();
        }
    }
}
