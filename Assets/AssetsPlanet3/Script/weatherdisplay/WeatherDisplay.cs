using planet3.rest_api.model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Assets.planet3.rest_api.model;
using AssetsPlanet3.Script.weatherdisplay;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class WeatherDisplay : MonoBehaviour
{
    public GameObject weatherDisplay;

    public Calculator calculator;

    public WeatherConstantTextures weatherConstantTextures;
    public Disaster[] disasters;
    public GameObject textures;
    public GameObject disasterIcon;

    void Start()
    {
        disasters = new Disaster[]
        {
            new Disaster(
                "Deepwater Horizon",
                new Coordinate(28.738056f, -88.366111f),
                "Oh yes, Deepwater Horizon� The wrong decisions by the staff, technical malfunctions, and design flaws of the oil platform, and much, much more�",
                "The Deepwater Horizon oil spill (also referred to as the \"BP oil spill\") was an environmental disaster which began on 20 April 2010, off the coast of the United States in the Gulf of Mexico on the BP-operated Macondo Prospect, considered the largest marine oil spill in the history of the petroleum industry and estimated to be 8 to 31 percent larger in volume than the previous largest, the Ixtoc I oil spill, also in the Gulf of Mexico. Caused in the aftermath of a blowout and explosion on the Deepwater Horizon oil platform, the United States federal government estimated the total discharge at 4.9 MMbbl (210,000,000 US gal; 780,000 m3). After several failed efforts to contain the flow, the well was declared sealed on 19 September 2010.[10] Reports in early 2012 indicated that the well site was still leaking. The Deepwater Horizon oil spill is regarded as one of the largest environmental disasters in world history.",
                "Numerous investigations explored the causes of the explosion and record-setting spill. The United States Government report, published in September 2011, pointed to defective cement on the well, faulting mostly BP, but also rig operator Transocean and contractor Halliburton. Earlier in 2011, a White House commission likewise blamed BP and its partners for a series of cost-cutting decisions and an inadequate safety system, but also concluded that the spill resulted from \"systemic\" root causes and \"absent significant reform in both industry practices and government policies, might well recur\".",
                "The Deepwater Horizon oil spill of 2010 caused significant environmental damage, affecting 8,332 species including fish, polychaetes, birds, mollusks, crustaceans, sea turtles, and marine mammals. The spill resulted in a 40-fold increase in polycyclic aromatic hydrocarbons (PAHs) in the water, posing health risks to marine life and humans due to carcinogens and oxygen depletion. Methane in the oil contributed to suffocation of marine life and the creation of dead zones. Studies linked oil toxins to irregular heartbeats, cardiac arrest, and organ deformities in fish. The dispersant Corexit exacerbated the issue, permeating the food chain and causing mutations in marine life, with chemicals reaching migratory birds and even insect populations. Impact assessments revealed a decline in seafood catches, lesions in fish, and over a million coastal bird deaths. Dolphin deaths surged, with many stranded and exhibiting illness. Tar balls and oil sheens persisted along the Gulf coast, alongside severe erosion and a decrease in marine life. Oil on the seafloor persisted, with long-term effects on the food chain. Further studies revealed increased toxicity of dispersed oil and its link to dolphin deaths and abnormal lung development in newborn dolphins within the spill area.",
                textures.GetComponent<TexturesConstants>().deepWaterHorizon,
                textures.GetComponent<TexturesConstants>().deepWaterHorizonConsiquences
            ),
            new Disaster(
                "Chernobyl nuclear disaster",
                new Coordinate(51.2763f, 30.2219f),
                "The Chernobyl nuclear power plant accident � the largest man-made disaster, leaving a mark for hundreds of years to come�",
                "The Chernobyl disaster occurred on April 26, 1986, when the No. 4 reactor of the Chernobyl Nuclear Power Plant exploded, leading to a catastrophic nuclear accident. It is one of two incidents rated at the maximum severity level on the International Nuclear Event Scale. Over 500,000 personnel were involved in the emergency response and mitigation efforts, costing an estimated $68 billion in 2019. The accident resulted from a test of emergency systems, which led to a power surge, steam explosions, and a meltdown, destroying the reactor's containment building. A core fire released airborne radioactive contaminants across the USSR and Europe until May 4, 1986. Following the accident, a 10-kilometer exclusion zone was established, evacuating 49,000 people, later expanded to a 30-kilometer radius, evacuating an additional 68,000. The disaster is considered the worst nuclear accidents in history, with long-lasting environmental consequences.",
                "The Chernobyl disaster of 1986 was triggered by a combination of design flaws in the RBMK reactor, procedural errors during a safety test, inadequate training of personnel, and a culture of secrecy within the Soviet nuclear industry. These factors culminated in a series of explosions, a massive fire, and the release of radioactive materials into the atmosphere, leading to widespread contamination and long-term health consequences for the surrounding population.",
                "The Chernobyl disaster of 1986 unleashed a catastrophic chain of events with far-reaching consequences. The explosion and fire at the nuclear power plant released large amounts of radioactive materials into the atmosphere, contaminating vast areas of land. Immediate effects included the deaths of plant workers, acute radiation sickness in emergency responders, and the evacuation of nearby towns and villages. The long-term impacts were profound, spanning health crises, environmental devastation, and socio-economic disruption. Thousands suffered from radiation-related illnesses, including cancer, birth defects, and thyroid disorders. The disaster shattered public trust in nuclear energy, prompting heightened safety regulations and scrutiny worldwide. Chernobyl's legacy serves as a stark reminder of the potential dangers of nuclear power and the importance of rigorous safety measures in preventing similar catastrophes in the future.",
                textures.GetComponent<TexturesConstants>().chernobyl,
                textures.GetComponent<TexturesConstants>().chernobylConsiquences
            ),
            new Disaster(
                "Bhopal disaster",
                new Coordinate(23.2599f, 77.4126f),
                "The Bhopal disaster � the darkest chapter in human history, claiming thousands of lives in one night�",
                "The Bhopal disaster or Bhopal gas tragedy was a chemical accident on the night of 2�3 December 1984 at the Union Carbide India Limited (UCIL) pesticide plant in Bhopal, Madhya Pradesh, India. In what is considered the world's worst industrial disaster,[3] over 500,000 people in the small towns around the plant were exposed to the highly toxic gas methyl isocyanate (MIC).[4] Estimates vary on the death toll, with the official number of immediate deaths being 2,259. In 2008, the Government of Madhya Pradesh paid compensation to the family members of 3,787 victims killed in the gas release, and to 574,366 injured victims.[1] A government affidavit in 2006 stated that the leak caused 558,125 injuries, including 38,478 temporary partial injuries and approximately 3,900 severely and permanently disabling injuries.[5] Others estimate that 8,000 died within two weeks, and another 8,000 or more have since died from gas-related diseases.",
                "The Bhopal disaster is debated between \"corporate negligence\" and \"worker sabotage\" theories. The former suggests poor maintenance, weak safety measures, and under-trained staff caused water to enter MIC tanks. The latter, supported by Union Carbide, claims a rogue employee deliberately connected a water hose to a tank valve. Despite differing explanations, evidence remains inconclusive. The Indian government's control over investigations further complicates understanding, leaving the true cause of the tragedy unresolved.",
                "The Bhopal disaster of 1984 left a devastating legacy of consequences. Immediate effects included the deaths of thousands and injuries to countless others due to exposure to toxic gas. Survivors suffered from long-term health issues such as respiratory problems, neurological disorders, and chronic illnesses. The disaster resulted in widespread environmental contamination, affecting soil and water sources in the surrounding area. Livelihoods were destroyed as agricultural land became unusable, and economic hardship plagued the community. Social structures were disrupted, with families torn apart and communities fractured. Legal battles for compensation and justice have dragged on for decades, often without adequate resolution. The tragedy underscored the need for stricter industrial safety regulations and corporate accountability. It remains a poignant reminder of the human cost of industrial negligence and the importance of prioritizing the safety and well-being of communities over profit.",
                textures.GetComponent<TexturesConstants>().bhopal,
                textures.GetComponent<TexturesConstants>().bhopalConsiquences
            ),
            new Disaster(
                "Dust Bowl",
                new Coordinate(35.839711f, -112.330173f),
                "We rarely take the blame for the Dust Bowl, blaming the drought for everything. However, we forget about the failure to apply dryland farming methods to prevent wind erosion, most notably the destruction of the natural topsoil by settlers in the region and so on�",
                "The Dust Bowl was the result of a period of severe dust storms that greatly damaged the ecology and agriculture of the American and Canadian prairies during the 1930s. The phenomenon was caused by a combination of natural factors (severe drought) and human-made factors: a failure to apply dryland farming methods to prevent wind erosion, most notably the destruction of the natural topsoil by settlers in the region. The drought came in three waves: 1934, 1936, and 1939�1940, but some regions of the High Plains experienced drought conditions for as long as eight years.\r\nThe Dust Bowl has been the subject of many cultural works, including John Steinbeck's 1939 novel The Grapes of Wrath, the folk music of Woody Guthrie, and Dorothea Lange's photographs depicting the conditions of migrants, particularly Migrant Mother, taken in 1936.",
                "The Dust Bowl was caused by federal land policies that encouraged farming in the Great Plains, a misunderstanding of the region’s ecology, and economic factors. Settlers believed that farming would permanently change the climate, making it more conducive to agriculture. This led to the cultivation of lands that couldn’t be reached by irrigation. When drought struck in the 1930s, these crops failed, leaving the soil exposed to the wind, causing severe soil erosion and the infamous dust storms of the Dust Bowl.",
                "The Dust Bowl caused widespread human displacement and economic devastation. Families abandoned farms due to drought-induced financial ruin, triggering mass migration in search of work. Over 500,000 Americans were left homeless, with many forced to tear down houses after dust storms. Illness and death from dust-related pneumonia and malnutrition afflicted residents. Around 3.5 million people left the Plains states, with over 86,000 migrating to California. The migration, often internal, led to the coining of terms like \"Okies\" and \"Arkies\" for the displaced. Government interventions included soil conservation programs and relief efforts like the Civilian Conservation Corps. Long-term economic impacts persisted, with erosion reducing agricultural land values and failing to return to pre-Dust Bowl levels. The crisis influenced art, literature, and music, capturing the struggles and resilience of those affected. Despite challenges, changes in agriculture and population on the Plains were spurred, shaping the region's future trajectory.",
                textures.GetComponent<TexturesConstants>().dustBowl,
                textures.GetComponent<TexturesConstants>().dustBowlConsiquences
            ),
            new Disaster(
                "Exxon Valdez oil spill",
                new Coordinate(60.8400f, -146.8625f),
                "We would never have achieved what we have now without oil. But the Exxon Valdez oil spill – that was unnecessary.",
                "The Exxon Valdez oil spill occurred on March 24, 1989, in Prince William Sound, an inlet in the Gulf of Alaska, Alaska, U.S. The incident happened after an Exxon Corporation tanker, the Exxon Valdez, ran aground on Bligh Reef during a voyage from Valdez, Alaska, to California. Delayed efforts to contain the spill and naturally strong winds and waves dispersed nearly 11,000,000 gallons of North Slope crude oil across the sound. The spill eventually polluted 1,300 miles of indented shoreline, as well as adjacent waters.",
                "The incident happened after an Exxon Corporation tanker, the Exxon Valdez, ran aground on Bligh Reef during a voyage from Valdez, Alaska, to California. Delayed efforts to contain the spill and naturally strong winds and waves dispersed nearly 11,000,000 gallons of North Slope crude oil across the sound[^3^][2]. The National Transportation Safety Board (NTSB) eventually assigned most of the blame for the oil spill to Exxon, citing its incompetent and overworked crew.",
                "The Exxon Valdez oil spill had profound and lasting effects on the environment and wildlife of Prince William Sound and beyond. The immediate aftermath saw the death of hundreds of thousands of seabirds, otters, seals, and whales. The oil slick covered 1,300 miles of coastline, devastating the habitats it touched. Nearly 30 years later, pockets of crude oil still remain in some locations, continuing to pose a threat to wildlife and the ecosystem. The cleanup efforts involved over 11,000 Alaska residents and Exxon employees, costing Exxon about $2 billion, with an additional $1.8 billion for habitat restoration and personal damages. However, only about 14% of the oil was recovered or cleaned up, leaving the rest in the environment. The aggressive cleanup measures, such as high-pressure, hot water hoses, were somewhat effective in removing oil but caused additional ecological damage, killing plants and animals in the process. One notable location, Mearn’s Rock, was oiled but never cleaned, serving as a study site for the long-term effects of the spill. The disaster led to legislative changes and a reevaluation of industry practices, but the long-term environmental impacts, particularly on the local fisheries and indigenous communities, have been significant and enduring.",
                textures.GetComponent<TexturesConstants>().exxonValdez,
                textures.GetComponent<TexturesConstants>().exxonValdezConsiquences
            ),
            new Disaster(
                "Pacific Gyre Garbage Patch",
                new Coordinate(38.0000f, -145.0000f),
                "Why recycle trash or at least throw it in the right place? Nothing bad will happen from one little bottle, right? That’s how the Pacific Gyre Garbage Patch comes about…",
                "The Pacific Gyre Garbage Patch, also known as the Great Pacific Garbage Patch, is a massive floating collection of marine debris in the North Pacific Ocean. This vast expanse of trash, primarily composed of plastics, spans waters from the West Coast of North America to Japan. It is formed by the convergence of ocean currents in the North Pacific Subtropical Gyre, which draws in debris from across the Pacific into two main areas: the Western Garbage Patch near Japan and the Eastern Garbage Patch between Hawaii and California.",
                "The Pacific Gyre Garbage Patch, also known as the Great Pacific Garbage Patch, is a massive accumulation of marine debris in the North Pacific Ocean. The patch is formed by the convergence of ocean currents in the North Pacific Subtropical Gyre, which acts like a vortex, drawing in debris from across the Pacific. This gyre is made up of four main currents: the California Current, the North Equatorial Current, the Kuroshio Current, and the North Pacific Current.",
                "The consequences of the Pacific Gyre Garbage Patch are extensive and multifaceted. The accumulation of debris poses a significant threat to marine life, with animals mistaking plastic for food, leading to ingestion, entanglement, and often death. Microplastics, the tiny fragments resulting from the breakdown of larger pieces of plastic, permeate marine ecosystems, entering the food chain at multiple levels. This not only impacts the health and survival of marine species but also has implications for human health, as these plastics and associated toxins make their way into the seafood that people consume. The presence of the garbage patch also hinders navigation and poses risks to vessels, further complicating efforts to study and mitigate the problem. The visual and ecological blight of the patch stands as a stark reminder of the enduring legacy of human waste, with cleanup and recovery efforts projected to take decades, if not longer, to restore the affected areas to their natural state.",
                textures.GetComponent<TexturesConstants>().pacificGyreGarbagePatch,
                textures.GetComponent<TexturesConstants>().pacificGyreGarbagePatchConsiquences
            ),
            new Disaster(
                "Castle bravo",
                new Coordinate(11.6916639f, 165.269832254f),
                "War drives people to do crazy things. Like nuclear testing of the world’s largest bombs…",
                "Castle Bravo was the code name for the largest nuclear device ever detonated by the United States, with a yield of 15 megatons. Conducted on March 1, 1954, at Bikini Atoll, Marshall Islands, it was part of Operation Castle. The test was intended to advance thermonuclear weapon designs but resulted in significant radioactive contamination due to a miscalculation involving lithium-7 isotope. The fallout affected inhabitants of nearby atolls and a Japanese fishing boat, causing acute radiation syndrome and one fatality. The incident sparked international outcry over atmospheric thermonuclear testing and raised questions about the safety and ethics of nuclear experimentation",
                "Castle Bravo was part of Operation Castle, a series of tests to advance thermonuclear weapon designs. The test’s unexpectedly high yield of 15 megatons, 2.5 times the predicted, was due to a miscalculation involving lithium-7 isotope. This led to significant radioactive contamination, affecting nearby atolls and a Japanese fishing boat. The incident highlighted the unpredictability and potential dangers of nuclear testing",
                "The explosion vaporized three islands and created a crater 6,500 feet in diameter and 250 feet deep. The shock wave from the blast caused significant destruction and generated a massive mushroom cloud that reached over 60,000 feet into the atmosphere.\r\nThe immediate aftermath saw fallout raining down on the surrounding atolls, leading to significant radioactive contamination. The inhabitants of Rongelap, Utirik, and other atolls were exposed to high levels of radiation, resulting in cases of radiation sickness. The fallout spread across the world, with traces of radioactive material detected as far as Australia, India, Japan, Europe, and the United States.\r\nThe legacy of Castle Bravo is a sobering reminder of the destructive power of nuclear weapons and the long-lasting consequences of nuclear testing on human health and the environment",
                textures.GetComponent<TexturesConstants>().nuclearBomb,
                textures.GetComponent<TexturesConstants>().nuclearBombConsiquences
            ),
            new Disaster(
                "Door to Hell",
                new Coordinate(40.2525f, 58.4396f),
                "Another example of a “brilliant” solution to a problem! Fearing the environmental impact due to the substantial methane gas release, the geologists decided to burn it off. Unfortunately, the gas in the “Door to Hell” is still burning today…",
                "The 'Door to Hell', also known as the Darvaza gas crater, is a notorious geological oddity in Turkmenistan’s Karakum Desert. This fiery pit has been burning continuously since 1971, when geologists set it alight to prevent the spread of methane gas. The crater, measuring roughly 70 meters wide, has become a bizarre tourist attraction, despite its dangerous emissions. Its flames offer a stark visual of the potential consequences of industrial accidents and natural resource exploitation",
                "The 'Door to Hell' was created after a Soviet drilling operation in 1971 went wrong, causing a natural gas field to collapse into a large crater. To prevent the spread of methane gas, geologists decided to set it on fire, expecting the flames to extinguish after a few weeks. However, the fire has continued to burn for over 50 years, becoming a symbol of the long-term impact of industrial accidents on the environment",
                "Its continuous burn has several consequences:\r\n\r\nGreenhouse Gas Emissions: The burning of methane gas releases significant amounts of carbon dioxide and methane, potent greenhouse gases that contribute to global warming1.\r\nAir Pollution: The combustion process generates pollutants that can degrade air quality and pose health risks to humans and animals1.\r\nSoil Degradation: The intense heat can alter the chemical composition of the soil, making it less fertile and disrupting the local ecosystem2.\r\nThreat to Wildlife: The heat and pollutants from the fire can harm local wildlife, potentially leading to reduced biodiversity2.\r\nVisual Pollution: The constant blaze is a form of visual pollution, altering the natural landscape of the area2.\r\nWaste of Natural Resources: The continuous burning of natural gas represents a significant waste of a non-renewable energy source2.\r\nImpact on Water Resources: The heat and pollutants may affect local water resources, impacting both quality and availability2.\r\nClimate Change Contribution: The emissions from the crater contribute to climate change, which can lead to more extreme weather events globally1.\r\nTourism Impact: While it attracts tourists, increased human activity can lead to further environmental degradation2.\r\nResearch Challenges: The extreme conditions make it difficult for researchers to study the crater and its environmental impact3.\r\nPotential for Accidents: The unstable ground and intense heat pose safety risks to visitors and researchers3.\r\nCultural Impact: The site has become a part of local folklore, potentially altering cultural practices and perceptions3.\r\nEconomic Costs: Managing the site and mitigating its impacts require financial resources3.\r\nGlobal Attention: The crater has brought international attention to Turkmenistan, influencing its environmental policies2.\r\nInspiration for Media: The dramatic visuals of the crater have inspired artists and media, influencing public perception3.\r\nEducational Value: It serves as a real-world example of the long-term consequences of industrial accidents3.\r\nScientific Interest: The crater provides a unique opportunity to study the combustion of natural gases3.\r\nInfrastructure Impact: The heat may damage nearby infrastructure over time3.\r\nPolicy Influence: The existence of the crater has led to discussions on environmental policy and energy use2.\r\nLongevity of Impact: The enduring burn serves as a reminder of the durability and impact of certain human actions on the environment",
                textures.GetComponent<TexturesConstants>().doorToHell,
                textures.GetComponent<TexturesConstants>().doorToHellConsiquences
            ),
            new Disaster(
                "Amoco Cadiz",
                new Coordinate(47.578f, 3.212f),
                "Too many sunken tankers, right? Well, there’s another one! ",
                "The Amoco Cadiz, an oil tanker, suffered a catastrophic spill on March 16, 1978, near Brittany, France. A steering failure during a storm led to its grounding and breakup. It spilled 1.6 million barrels of crude oil, devastating marine life and local industries. The incident prompted global maritime law reforms. It remains one of the worst oil spills in history.\r\n",
                "The Amoco Cadiz disaster was precipitated by a severe storm that damaged the ship's rudder, causing a loss of steering control. The failure was due to the shearing of studs in the steering gear, resulting in hydraulic fluid loss. Despite attempts at repair and towing, the efforts were thwarted by the storm's intensity. The vessel's massive size and the force of the storm made it impossible to prevent its grounding. Ultimately, the ship ran aground on Portsall Rocks, leading to one of the largest oil spills in history.",
                "The Amoco Cadiz oil spill had far-reaching consequences that extended well beyond the immediate environmental damage. The spill released 1.6 million barrels of crude oil into the sea, causing one of the largest oil spills in maritime history. The environmental impact was devastating, with the oil slick spreading across 300 kilometers of the French coastline. The spill affected a wide variety of marine life, including fish, birds, and mammals, leading to massive die-offs. The local fishing industry suffered greatly, with the contamination leading to a ban on fishing, which lasted for an extended period. The tourism industry also took a significant hit, as the picturesque beaches of Brittany were marred by the oil. Cleanup efforts were extensive and costly, requiring a massive mobilization of resources and personnel. The disaster prompted a reevaluation of maritime safety regulations and led to the implementation of new policies aimed at preventing similar incidents. The French government revised its oil response plan, known as the Polmar Plan, and acquired new equipment stocks to better deal with such disasters. Traffic lanes in the English Channel were imposed to reduce the risk of ship groundings. The incident also led to the creation of Cedre, an organization dedicated to addressing maritime pollution. The legal aftermath of the spill lasted for over a decade, with numerous lawsuits filed against the Amoco Corporation. The spill highlighted the need for better international cooperation and preparedness in dealing with environmental disasters. It also raised public awareness about the ecological risks associated with the transportation of oil by sea. The long-term ecological consequences are still being felt today, with some areas yet to fully recover from the damage. The Amoco Cadiz disaster remains a stark reminder of the potential dangers of oil spills and the importance of safeguarding our oceans.",
                textures.GetComponent<TexturesConstants>().amocoCadiz,
                textures.GetComponent<TexturesConstants>().amocoCadizConsiquences
            ),
            new Disaster(
                "Seveso Disaster",
                new Coordinate(45.6524f, 9.1423f),
                "Yes, Europe has always been more careful with the environment, preferring insurance over destruction. However, this was after the Seveso Disaster.",
                "The Seveso Disaster of 1976 was a devastating industrial accident in Italy. A chemical plant released a toxic cloud containing TCDD, a highly dangerous dioxin. The exposure was the highest ever for a residential population. This led to severe health effects and environmental contamination. The disaster prompted the EU's stringent Seveso Directives for industrial safety.\r\n",
                "The Seveso Disaster was triggered by a chemical plant overheating, causing a pressure release valve to fail and emit a toxic cloud. Lack of temperature monitoring for operators and halted weekend operations prevented reaction control. The plant produced 2,4,5-trichlorophenol, which, when overheated, generated dioxin-rich gases. The accident exposed systemic flaws in industrial safety and emergency response protocols. It underscored the dire consequences of inadequate chemical process management and safety measures.\r\n",
                "The Seveso Disaster of 1976 had profound and lasting consequences on the affected communities and the environment. The release of TCDD, a highly toxic dioxin, led to the contamination of a large area, rendering it uninhabitable and necessitating the evacuation of over 600 people. Thousands of animals died immediately, and over 77,000 were slaughtered to prevent the spread of contamination. The disaster resulted in long-term health effects for the exposed population, including skin lesions, cancer, and reproductive issues. Vegetation wilted and the area's biodiversity suffered greatly, with some species experiencing long-term population declines. The local economy, particularly agriculture, was devastated as crops were destroyed and land became unusable. Cleanup operations were extensive and expensive, involving decontamination of the soil and destruction of buildings. The disaster led to the implementation of the Seveso Directives, Europe's stringent industrial safety regulations. It also prompted a reevaluation of chemical safety and emergency response procedures worldwide. The legal and financial repercussions for the companies involved lasted for many years. The Seveso Disaster became a symbol of industrial negligence and the potential human and environmental cost of chemical manufacturing. It raised public awareness about the risks associated with chemical plants and the importance of regulatory oversight. The affected areas took decades to recover, with some still showing signs of damage. The disaster highlighted the need for better communication and planning in industrial areas close to residential zones. It underscored the importance of proper maintenance and safety measures in preventing such accidents. The Seveso Disaster remains a case study in environmental science and policy, illustrating the need for sustainable and safe industrial practices.\r\n",
                textures.GetComponent<TexturesConstants>().sevesoDisaster,
                textures.GetComponent<TexturesConstants>().sevesoDisasterConsiquences
            ),
        };
        
        foreach (var disaster in disasters)
        {
            var newDisaster = Instantiate(disasterIcon, transform.position, transform.rotation);
            newDisaster.SetActive(true);
            var position = calculator.CalculatePositionFromLatAndLon(disaster.Coordinates, 0.03f);
            newDisaster.transform.SetParent(transform.parent);

            newDisaster.transform.SetPositionAndRotation(position, Quaternion.FromToRotation(new Vector3(0, 0, 1), position));
            Debug.Log("setting for " + disaster.Name);

            var panel = newDisaster.GetComponent<ShowDisasterPanel>();
            panel.Disaster = disaster;
            panel.SetDisplay(newDisaster.GetComponentInChildren<RawImage>());
        }

        WeatherAPI.OnWeatherDataReceived += OnWeatherDataReceived;
        
        weatherDisplay.SetActive(false);
        disasterIcon.SetActive(false);
    }

    private void OnWeatherDataReceived(Dictionary<Coordinate, Country> coordinates, CurrentWeather weatherData)
    {
        for (int i = 0; i < weatherData.weatherDatas.Length; ++i) 
        {
            var curWeatherData = weatherData.weatherDatas[i];
            var countryCoordinates = coordinates.Keys.ElementAt(i);
            var country = coordinates.Values.ElementAt(i);
            
            Debug.Log("Weather received for country: " + country.Name + ", Coordinates: " + country.Coordinates);

            WeatherCondition condition = WeatherCondition.GetWeatherConditionFromIndexes(curWeatherData.current.is_day, curWeatherData.current.weather_code);

            GameObject newWeatherDisplay = Instantiate(weatherDisplay, transform.position, transform.rotation);
            newWeatherDisplay.SetActive(true);

            var targetTexture = weatherConstantTextures.GetTextureByName(condition.Name);

            newWeatherDisplay.GetComponentInChildren<RawImage>().texture = targetTexture;

            newWeatherDisplay.GetComponentInChildren<VideoPlayer>().targetTexture = targetTexture;
            newWeatherDisplay.GetComponentInChildren<VideoPlayer>().url = "file://" + condition.AnimationLink;
            newWeatherDisplay.GetComponentInChildren<VideoPlayer>().Play();
            newWeatherDisplay.transform.SetParent(transform.parent);
            newWeatherDisplay.GetComponentsInChildren<Text>()[0].text = country.Name;
            Debug.Log($"Country name: {country.Name}");
            newWeatherDisplay.GetComponentsInChildren<Text>()[1].text = curWeatherData.current.temperature_2m.ToString(CultureInfo.CurrentCulture) + "\n" + curWeatherData.current_units.temperature_2m;

            var weatherPanel = newWeatherDisplay.GetComponent<ShowWeatherPanel>();
            weatherPanel.SetWeatherData(curWeatherData);
            weatherPanel.SetWeatherCondition(condition);
            weatherPanel.SetCountry(country);
            
            var climatePanel = newWeatherDisplay.GetComponent<ShowClimatePanel>();
            climatePanel.Country = country;
            
            var position = calculator.CalculatePositionFromLatAndLon(countryCoordinates, 0.03f);

            newWeatherDisplay.transform.SetPositionAndRotation(position, Quaternion.FromToRotation(new Vector3(0, 0, 1), position));

            Debug.Log("I have set condition to " + condition.AnimationLink);
        }
    }
}

public class WeatherCondition
{
    public const string WEATHER_ICONS_PATH = "/AssetsPlanet3/Script/plugins/weather-icons/480/";
    public const string TEXTURE_PATH = "/textures/";

    public List<int> Codes { get; }

    public string Description { get; }

    public string Name { get; }

    public string AnimationLink { get => Application.dataPath + WEATHER_ICONS_PATH + Name + ".webm"; }

    public string TextureLink { get =>TEXTURE_PATH + Name + "_texture.renderTexture"; }

    private WeatherCondition(List<int> codes, string name, string description)
    {
        Codes = codes;
        Name = name;
        Description = description;
    }

    public static readonly WeatherCondition ClearSkyDay = new WeatherCondition(new List<int> { 0 }, "clear_day", "Clear day");
    public static readonly WeatherCondition ClearSkyNight = new WeatherCondition(new List<int> { 0 }, "clear_night", "Clear night");

    public static readonly WeatherCondition PartlyCloudDay = new WeatherCondition(new List<int> { 1, 2 }, "partly_cloudy_day", "Partialy clear day");
    public static readonly WeatherCondition PartlyCloudNight = new WeatherCondition(new List<int> { 1, 2 }, "partly_cloudy_night", "Partialy clear night");

    public static readonly WeatherCondition OvercastDay = new WeatherCondition(new List<int> { 3 }, "overcast_day", "Overcast day");
    public static readonly WeatherCondition OvercastNight = new WeatherCondition(new List<int> { 3 }, "overcast_night", "Overcast night");

    public static readonly WeatherCondition FogDay = new WeatherCondition(new List<int> { 45 }, "fog_day", "Fog day");
    public static readonly WeatherCondition FogNight = new WeatherCondition(new List<int> { 45 }, "fog_night", "Fog night");

    public static readonly WeatherCondition RimeDay = new WeatherCondition(new List<int> { 48 }, "rime_day", "Rime day");
    public static readonly WeatherCondition RimeNight = new WeatherCondition(new List<int> { 48 }, "rime_night", "Rime night");

    public static readonly WeatherCondition LightDrizzleDay = new WeatherCondition(new List<int> { 51 }, "light_drizzle_day", "Light drizzle day");
    public static readonly WeatherCondition LightDrizzleNight = new WeatherCondition(new List<int> { 51 }, "light_drizzle_night", "Light drizzle night");

    public static readonly WeatherCondition MediumDrizzleDay = new WeatherCondition(new List<int> { 52, 53 }, "medium_drizzle_day", "Medium drizzle day");
    public static readonly WeatherCondition MediumDrizzleNight = new WeatherCondition(new List<int> { 52, 53 }, "medium_drizzle_night", "Medium drizzle night");

    public static readonly WeatherCondition FreezingDrizzleDay = new WeatherCondition(new List<int> { 56, 57 }, "freezing_drizzle_day", "Freezing drizzle day");
    public static readonly WeatherCondition FreezingDrizzleNight = new WeatherCondition(new List<int> { 56, 57 }, "freezing_drizzle_night", "Freezing drizzle night");

    public static readonly WeatherCondition RainDay = new WeatherCondition(new List<int> { 61, 63, 65 }, "rain_day", "Rain day");
    public static readonly WeatherCondition RainNight = new WeatherCondition(new List<int> { 61, 63, 65 }, "rain_night", "Rain night");

    public static readonly WeatherCondition FreezingRainDay = new WeatherCondition(new List<int> { 66, 67 }, "freezing_rain_day", "Freezing rain day");
    public static readonly WeatherCondition FreezingRainNight = new WeatherCondition(new List<int> { 66, 67 }, "freezing_rain_night", "Freezing rain night");

    public static readonly WeatherCondition LightSnowDay = new WeatherCondition(new List<int> { 71, 72 }, "light_snow_day", "Light snow day");
    public static readonly WeatherCondition LightSnowNight = new WeatherCondition(new List<int> { 71, 72 }, "light_snow_night", "Light snow night");

    public static readonly WeatherCondition HeavySnowDay = new WeatherCondition(new List<int> { 73 }, "heavy_snow_day", "Heavy snow day");
    public static readonly WeatherCondition HeavySnowNight = new WeatherCondition(new List<int> { 73 }, "heavy_snow_night", "Heavy snow night");

    public static readonly WeatherCondition HailDay = new WeatherCondition(new List<int> { 77 }, "hail_day", "Hail day");
    public static readonly WeatherCondition HailNight = new WeatherCondition(new List<int> { 77 }, "hail_night", "Hail night");

    public static readonly WeatherCondition thunderstormRainDay = new WeatherCondition(new List<int> { 80, 81, 82, 95 }, "thunderstorm_rain_day", "Thunderstorm rain day");
    public static readonly WeatherCondition thunderstormRainNight = new WeatherCondition(new List<int> { 80, 81, 82, 95 }, "thunderstorm_rain_night", "Thunderstorm rain night");

    public static readonly WeatherCondition thunderstormSnowDay = new WeatherCondition(new List<int> { 85, 86, 956, 99 }, "thunderstorm_snow_day", "Thunderstorm snow day");
    public static readonly WeatherCondition thunderstormSnowNight = new WeatherCondition(new List<int> { 85, 86, 956, 99 }, "thunderstorm_snow_night", "Thunderstorm snow night");


    private static WeatherCondition[] GetWeatherConditions()
    {
        return new WeatherCondition[]
        {
            ClearSkyDay, ClearSkyNight,
            PartlyCloudDay, PartlyCloudNight,
            OvercastDay, OvercastNight,
            FogDay, FogNight,
            RimeDay, RimeNight,
            LightDrizzleDay, LightDrizzleNight,
            MediumDrizzleDay, MediumDrizzleNight,
            FreezingDrizzleDay, FreezingDrizzleNight,
            RainDay, RainNight,
            FreezingRainDay, FreezingRainNight,
            LightSnowDay, LightSnowNight,
            HeavySnowDay, HeavySnowNight,
            HailDay, HailNight,
            thunderstormRainDay, thunderstormRainNight,
            thunderstormSnowDay, thunderstormSnowNight
        };
    }

    public static WeatherCondition GetWeatherConditionFromIndexes(bool isNight, int weatherIndex)
    {
        WeatherCondition[] conditions = GetWeatherConditions();
        foreach (var condition in conditions)
        {
            if (condition.Codes.Contains(weatherIndex) && (isNight ? condition.AnimationLink.Contains("_night") : condition.AnimationLink.Contains("_day")))
            {
                return condition;
            }
        }

        return isNight ? ClearSkyNight : ClearSkyDay;
    }


}
