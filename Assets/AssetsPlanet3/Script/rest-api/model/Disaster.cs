using planet3.rest_api.model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disaster
{
    public string Name { get; set; }

    public Coordinate Coordinates { get; set; }

    public string ShortDescription { get; set; }
    public string Reason { get; set; }


    public string TransitionText { get; set; }

    public string Consiquences { get; set; }

    public Texture ConsiquencesTexture { get; set; }

    public Texture Texture { get; set; }

    public Disaster(string name, 
        Coordinate coordinate, 
        string transitionText, 
        string shortDescription,
        string reason,
        string consiquences,
        Texture texture,
        Texture consiquencesTexture)
    {
        Name = name;
        Coordinates = coordinate;
        ShortDescription = transitionText;
        Reason = reason;
        Consiquences = consiquences;
        Texture = texture;
        ShortDescription = shortDescription;
        ConsiquencesTexture = consiquencesTexture;
    }

}
