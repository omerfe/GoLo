using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class GoloContextSeed
    {
        public static async Task SeedAsync(GoloContext db)
        {
            if (db.Genres.Any() || db.Platforms.Any() || db.Products.Any() || db.Games.Any()) return;

            #region Genres
            var g1 = new Genre() { GenreName = "Action" };
            var g2 = new Genre() { GenreName = "Adventure" };
            var g3 = new Genre() { GenreName = "RPG" };
            var g4 = new Genre() { GenreName = "Simulator" };
            var g5 = new Genre() { GenreName = "Indie" };
            var g6 = new Genre() { GenreName = "Casual" };
            var g7 = new Genre() { GenreName = "MMO" };
            var g8 = new Genre() { GenreName = "Sports" };
            var g9 = new Genre() { GenreName = "Arcade" };
            var g10 = new Genre() { GenreName = "Strategy" };
            var g11 = new Genre() { GenreName = "Fighting" };
            var g12 = new Genre() { GenreName = "Racing" };
            var g13 = new Genre() { GenreName = "FPS" };
            #endregion

            #region Platforms
            var p1 = new Platform() { PlatformName = "Steam", LogoPath = "steam.png" };
            var p2 = new Platform() { PlatformName = "Epic", LogoPath = "epic.png" };
            var p3 = new Platform() { PlatformName = "Origin", LogoPath = "origin.png" };
            var p4 = new Platform() { PlatformName = "Ubisoft", LogoPath = "ubisoft.png" };
            var p5 = new Platform() { PlatformName = "Xbox1", LogoPath = "xbox.png" };
            var p6 = new Platform() { PlatformName = "Ps5", LogoPath = "ps5.png" };
            #endregion

            #region Games
            var game1 = new Game()
            {
                GameName = "NBA 2K22 PC",
                ReleaseDate = new DateTime(2021, 09, 10),
                Description = @"NBA 2K22 is a basketball sports game developed by Visual Concepts and published by 2K Games. It is the latest installment in the series of basketball video games. The player is given the opportunity to become a part of one of the world's leading basketball associations, playing against the best teams in the United States of America. 2K22 introduces several changes and new gameplay mechanics, introducing new locations for the Neighborhood and the City modes, new tactics, and much more. The gameplay features are complemented by stunning visuals and excellent tracks, accompanying the player throughout the game. NBA 2K22 was met with a positive initial reception, with the critics praising the changes to the games modes as well as improved graphics.",
                Developer = "VISUAL CONCEPTS",
                Publisher = "2K",
                MinimumAge = 3,
                ImagePath = "NBA2k22.jpg",
                TrailerUrl = @"https://www.youtube.com/watch?v=OcUzwnA569M",
                GameRequirements = @"MINIMUM
OSWindows 7 64 - bit,
                Windows 8.1 64 - bit or Windows 10 64 - bit
PROCESSOR Intel® Core™ i3 - 2100 @ 3.10 GHz / AMD FX - 4100 @ 3.60 GHz or better
MEMORY4 GB RAM
GRAPHICSNVIDIA® GeForce® GT 450 1GB / ATI® Radeon™ HD 7770 1 GB or better
STORAGE110 GB available space",
                Genres = new List<Genre>() { g4, g9 }
            };

            var game2 = new Game()
            {
                GameName = "RED DEAD REDEMPTION 2 PC",
                ReleaseDate = new DateTime(2019, 11, 5),
                Description = @"Red Dead Redemption 2 is an action-adventure game it was released on Xbox One and PlayStation 4, and since 2019 it is also available on the PC. The game is a prequel to the Red Dead Redemption released in 2010 and focuses on the life of the outlaw Arthur Morgan. Enter and experience a truly massive world with an astounding amount of activities, interactions, beautiful visuals and more. If you have ever played the first RDR game, you might find a number of references to the characters and events from this game.
            The story
            The action takes place in 1899 in the Western,
                Southern and Midwestern parts of the United States,
                where you as the player take the role of Arthur Morgan,
                who is a member of the Van der Linde gang.Arthur must face many hardships such as the decline of the wild west culture and lifestyle,
                fighting the government that wants to get rid of the last bastion of the wild west culture as well as fight other adversaries like different gangs.
            Gameplay
            Set in the open world and played from first or third - person perspective Red Dead Redemption 2 includes heists,
                bounties,
                hunting,
                and horseback riding.Be careful with your choices as honor is something that can be easily lost.The player takes control over Arthur during progression through the story and questlines.Play and explore the enormous areas of the open world created to resemble as closely as possible real - world locations with some extra features.Unexplored lands full of bandits,
                travelers,
                stunning landscapes and animals can give you adventurous vibes while exploring the Wild West.Urban and farming settlements give you a chance to interact with interesting NPCs and uncover their secrets, often lurking in unexpected places, and also contribute to the true western atmosphere. Imagine yourself walking into a saloon and experiencing a fight breaking out of nowhere. In this game, you can engage in combat using various types of weapons such as melee, explosives or firearms, with a number of options to upgrade.In comparison to the original, this game features swimming, dual - wielding, improved combat and more!",
                Developer = "ROCKSTAR STUDIOS",
                Publisher = "ROCKSTAR GAMES",
                MinimumAge = 16,
                ImagePath = "RedDeadRedemption.jpg",
                TrailerUrl = @"https://www.youtube.com/watch?v=eaW0tYpxyp0&t=1s",
                GameRequirements = @"MINIMUM
OS Windows 7 - Service Pack 1(6.1.7601)
PROCESSORIntel Core i5 - 2500K / AMD FX - 6300
MEMORY8GB
GRAPHICSNvidia GeForce GTX 770 2GB / AMD Radeon R9 280 3GB HDD
STORAGE150GB",
                Genres = new List<Genre>() { g1, g2, g4 }
            };

            var game3 = new Game()
            {
                GameName = "GOD OF WAR PC",
                ReleaseDate = new DateTime(2022, 1, 14),
                Description = @"God of War is an action video game with RPG elements, developed by Santa Monica Studio and released thanks to PlayStation PC LLC in 2022 for personal computers. It is an adaptation of the title from 2018, which was available exclusively for PlayStation 4 users. In this installment, Kratos, having completed his violent mission in Greece, goes to the far North to once again face mythological beasts and mighty gods. This time, however, he will not be traveling alone.
    

God of War Story
            Although the harsh Scandinavia seems to be an excellent place to start a peaceful life,
                it soon turns out that Kratos has to face another battle.Together with her growing son,
                he must embark on a long journey into the heart of Viking myths.This trip is the beginning of a new legend about forging relationships,
                searching for good,
                and understanding destiny.
    
            Gameplay
    
            God of War video game is a third - person gameplay experience that introduces a few changes to the series.In this title,
                the camera is a bit closer to the hero,
                and the combat is slower and based on the style of fights known from the Souls series.Use combinations of attacks,
                dodges,
                and blocks to reset your opponent's health bar. You can also practice the unique Spartan Rage skill, which will grant you the option of using epic finishers.
    

    Kratos wields a battle axe forged from divine materials.You can hit with it,
                throw it,
                or beat your opponents up with fists.You can also choose from a whole arsenal of runic powers and crafting schemes that you can use to upgrade weapons by providing them with unique properties.Kratos' son Atreus also turns out to be a great help in the fight. Equipped with a bow, he stuns enemies with arrows from a distance, significantly affecting the result of battles with even the most powerful beasts.",

                Developer = "Santa Monica Studio",
                Publisher = "Sony Interactive Entertainment",
                MinimumAge = 16,
                ImagePath = "GodOfWar.jpg",
                TrailerUrl = @"https://www.youtube.com/watch?v=HqQMh_tij0c",
                GameRequirements = @"MINIMUM
    
            OSWindows 10 64 - bit
    
            PROCESSORIntel i5 - 2500k(4 core 3.3 GHz) or AMD Ryzen 3 1200(4 core 3.1 GHz)
    
            MEMORY8 GB RAM
    
            GRAPHICSNVIDIA GTX 960(4 GB) or AMD R9 290X(4 GB)
    
            STORAGE70 GB available space",

                Genres = new List<Genre>() { g1, g2, g3 }
            };

            var game4 = new Game()
            {
                GameName = "BATTLEFIELD 2042 GOLD EDITION PC",
                ReleaseDate = new DateTime(2021, 11, 12),
                Description = @"Battlefield 2042 Gold Edition PC (EN) includes:
Early access to game launch.
Year 1 Pass: 4 New Specialists(1 per Season), 4 Battle Passes(1 per Season), and 3 Epic Skin Bundles(”Blistered Earth”, ”Tempest”, and ”Cold Blood”).
Irish Battle Hardened Legendary Skin
Baku ACB - 90 melee takedown knife
'Mr. Chompy' epic weapon charm
'Landfall' player card background and 'Old Guard' tag
The top 3 reasons to play Battlefield 2042 Gold Edition PC(EN)
Battlefield 2042 is a first - person shooter that marks the return to the iconic all -out warfare of the franchise.
Disorder, adapt and overcome dynamically-changing battlegrounds with the help of your squad and a cutting-edge arsenal.
Take on several massive experiences, from updated multiplayer modes like Conquest and Breakthrough to the all-new Hazard Zone.
About Battlefield 2042 Gold Edition PC(EN)
Battlefield 2042 is a first - person shooter that marks the return to the iconic all -out warfare of the franchise.
In a near - future world transformed by disorder, adapt and overcome dynamically - changing battlegrounds with the help of your squad and a cutting-edge arsenal.
With support for 128 players, Battlefield 2042 brings unprecedented scale on vast battlegrounds across the globe.
Players will take on several massive experiences, from updated multiplayer modes like Conquest and Breakthrough to the all - new Hazard Zone.",
                Developer = "ELECTRONIC ARTS",
                Publisher = "DICE",
                MinimumAge = 16,
                ImagePath = "Battlefield.jpg",
                TrailerUrl = @"https://youtu.be/ASzOzrB-a9E",
                GameRequirements = @"MINIMUM
OSWindows 7 / 8 / 10(64 - bit OS required)
PROCESSORIntel Core i5 - 2300, 2.80 GHz / AMD A10 - 7850K, 3.70 GHz
MEMORY4 GB RAM
GRAPHICSGeForce GTX 660 Ti, 3 GB / Radeon HD 7950, 3 GB
STORAGE17 GB available space
RECOMMENDED
OSWindows 7 / 8 / 10(64 - bit OS required)
PROCESSORIntel Core i7 - 6700 / AMD Ryzen 5 1400
MEMORY8 GB RAM
GRAPHICSGeForce GTX 1060 / Radeon R9 Fury
STORAGE17 GB available space",
                Genres = new List<Genre>() { g1, g2, g6 }
            };

            var game5 = new Game()
            {
                GameName = "JUMP FORCE - ULTIMATE EDITION PC (EMEA)",
                ReleaseDate = new DateTime(2019, 2, 15),
                Description = @"Fight the most dangerous threat, the Jump Force will bear the fate of the entire humankind.
            Create your own avatar and jump into an original Story Mode to fight alongside the most powerful Manga heroes.
            About JUMP FORCE - Ultimate Edition PC(EMEA)
            The most famous Manga heroes are thrown into a whole new battleground: our world.Uniting to fight the most dangerous threat,
                the Jump Force will bear the fate of the entire humankind.
            Create your own avatar and jump into an original Story Mode to fight alongside the most powerful Manga heroes from DRAGON BALL Z,
                ONE PIECE,
                NARUTO,
                BLEACH,
                HUNTER X HUNTER,
                YU - GI - OH!,
                YU YU HAKUSHO,
                SAINT SEIYA and many others.
            Or head to the Online Lobby to challenge other players and discover lots of modes and activities.",
                Developer = "SPIKE CHUNSOFT",
                Publisher = "BANDAI NAMCO ENTERTAINMENT",
                MinimumAge = 13,
                ImagePath = "JumpForce.jpg",
                TrailerUrl = @"https://youtu.be/4TlNy3yG5LY",
                GameRequirements =
            @"MINIMUM
            OS64 - bit Windows 10
            PROCESSORAMD Ryzen 5 3600,
                Core i5 6600K
            MEMORY8 GB
            GRAPHICSAMD Radeon RX 560,
                Nvidia GeForce GTX 1050 Ti
            STORAGE100 GB
            RECOMMENDED
            OS64 - bit Windows 10
            PROCESSORAMD Ryzen 7 2700X,
                Intel Core i7 4790
            MEMORY16 GB
            GRAPHICSAMD Radeon RX 6600 XT,
                Nvidia GeForce RTX 3060
            STORAGE100 GB",
                Genres = new List<Genre>() { g1, g11 }
            };

            var game6 = new Game()
            {
                GameName = "MINECRAFT PC (JAVA EDITION)",
                ReleaseDate = new DateTime(2013, 12, 18),
                Description = @"Experience one of the most popular and best-selling games of all time.
            Minecraft is virtually limitless. The game’s Lego - like world is just perfect for creativity,
                experimentation,
                and modding.
            Let your imagination run wild in creative mode,
                or fend off dangerous mods in survival mode!
            Explore a variety of randomly generated biomes.
            Customize your game.The variety and types of mods currently available are nearly limitless",
                Developer = "MOJANG",
                Publisher = "MICROSOFT STUDIOS",
                MinimumAge = 0,
                ImagePath = "Minecraft.jpg",
                TrailerUrl = @"https://youtu.be/MmB9b5njVbA ",
                GameRequirements =
            @"MINIMUM for Minecraft
            PLATFORMWindows,
                Mac & Linux
            PROCESSORIntel Core i3 - 3210 3.2 GHz / AMD A8 - 7600 APU 3.1 GHz or equivalent
            GRAPHICSNvidia GeForce 400 Series or AMD Radeon HD 7000 series with OpenGL 4.4
            MEMORY4GB
            DISK SPACE1 GB
            RECOMMENDED for Minecraft
            PLATFORMWindows,
            
                Mac & Linux
            PROCESSORIntel Core i5 - 4690 3.5GHz / AMD A10 - 7800 APU 3.5 GHz or equivalent
            GRAPHICSGeForce 700 Series or AMD Radeon Rx 200 Series(excluding integrated chipsets) with OpenGL 4.5
            MEMORY8GB
            DISK SPACE4GB",
                Genres = new List<Genre>() { g6 }
            };

            var game7 = new Game()
            {
                GameName = "Prince of Persia : The Sands of Time Remake (Xbox)",
                ReleaseDate = new DateTime(2021, 03, 18),
                Description = @"The 2021 remake of developed by Ubisoft in 2003 and highly acclaimed game Prince of Persia= The Sands of Time. Over the years, many editions of Prince of Persia game have been released and met with very mixed reactions in the gaming environment. And then it was Assassin's Creed which happened to be a great Ubisoft's success, although it was based on Prince of Persia title. Nevertheless, the creators decided to renew the magical world of the Sands of Time.

This time rich in significantly improved graphics and combat system.In this third - person action - adventure game,
                you will relive the same story in a completely new version.The game was created and published entirely by Ubisoft in cooperation with the creators of the original title.",
                Developer = "UBISOFT",
                Publisher = "UBISOFT",
                MinimumAge = 12,
                ImagePath = "PrinceOfPersia.jpg",
                TrailerUrl = @"https://www.youtube.com/watch?v=htzq7EEXQs8",
                Genres = new List<Genre>() { g1, g2 },
                GameRequirements = @"CPU : Core i5-4670 or Ryzen 3 1200 or better.
RAM : 8 GB.
OS : Windows 10(64 - bit versions only)
VIDEO CARD : NVIDIA GeForce GTX 960 or AMD Radeon R9 280 or better.
PIXEL SHADER : 5.1.
VERTEX SHADER : 5.1.
SOUND CARD : Yes."
            };

            var game8 = new Game()
            {
                GameName = "Football Manager 2022",
                ReleaseDate = new DateTime(2021, 11, 08),
                Description = @"To plan ahead your tactics and development move, you'll require information. One of the many new features in Football Manager 2022 is Data Hub that will allow you to browse through many metrics and statistics, much like the biggest and the best real-life clubs do. You'll be able to gather the data based on your performance, as well as how the data regarding how opposing teams react to your tactics. All spreadsheets and heat maps are presented in a clear, easy-to navigate-manner. Use all of that information to your advantage and make better tactical and development decisions!",
                Developer = "Sega",
                Publisher = "Sega",
                MinimumAge = 3,
                ImagePath = "FootballManager2022.jpg",
                TrailerUrl = @"https://youtu.be/66-BT930evI",
                Genres = new List<Genre>() { g8, g4, g10 },
                GameRequirements = @"OS : Windows 7 64-bit, 8/8.1, 10.
Processor : Intel Core 2 or AMD Athlon 64 1.8GHz +
Memory : 4 GB RAM.
Graphics: Intel GMA X4500, NVIDIA GeForece 9600M GT, AMD / ATI Mobility Raedon HD 3650 - 256MB VRAM.
DirectX: Version 11.
Storage : 7 GB available space."
            };

            var game9 = new Game()
            {
                GameName = "FIFA 2022",
                ReleaseDate = new DateTime(2021, 09, 27),
                Description = "FIFA 22 is the latest installment of the FIFA series developed by EA Canada and published by Electronic Arts. The game takes you back to the world of international football, offering improved visuals and gameplay mechanics, bringing the virtual matches even closer to their real-life counterparts. FIFA 22 was met with positive initial reception thanks to its improved motion systems, mang other features.",
                Developer = "EA Sports",
                Publisher = "EA Sports",
                MinimumAge = 3,
                ImagePath = "FIFA2022.jpg",
                TrailerUrl = @"https://youtu.be/o1igaMv46SY",
                Genres = new List<Genre>() { g8 },
                GameRequirements = @"OS : Windows 10 - 64-Bit
PROCESSOR : Intel Core i3 - 6100 @ 3.7GHz or AMD Athlon X4 880K @4GHz
  MEMORY : 8GB
  GRAPHICS : NVIDIA GTX 660 2GB or AMD Radeon HD 7850 2GB
  STORAGE : 50 GB available space"
            };

            var game10 = new Game()
            {
                GameName = "STREET FIGHTER V",
                ReleaseDate = new DateTime(2000, 12, 30),
                Description = @"The legendary fighting franchise returns with Street Fighter V! 

Powered by Unreal Engine 4 technology,
                stunning visuals depict the next generation of World Warriors in unprecedented detail, while exciting and accessible battle mechanics deliver endless fighting fun that both beginners and veterans can enjoy.Challenge friends online, or compete for fame and glory on the Capcom Pro Tour.

Street Fighter V will be released exclusively for the PlayStation 4 and PC.Through a strategic partnership between Sony Computer Entertainment Inc.and Capcom, the next generation Street Fighter experience will offer cross - platform play that will unite fans into a centralized player base for the first time ever.The path to greatness begins here = RISE UP!

New and Returning Characters = Birdie and Charlie Nash make their return to the Street Fighter universe, where they join classic characters like Ryu, Chun-Li, Cammy, and M. Bison.Many more new and returning characters will be added to the diverse roster, offering a wide variety of fighting styles for players to choose from.
New Strategies and Battle Mechanics = Highly accessible new battle mechanics, which revolve around the V - Gauge and EX Gauge, provide an unprecedented layer of strategy and depth to the franchise that all players can enjoy.
V - Trigger = Unique abilities that use the entire V - Gauge, giving players the opportunity to inflict damage when activated.
V - Skill = Utility skills(such as evasion) for each character that can be activated at any time.
V - Reversal = Unique counterattacks that use one stock of the V - Gauge.
Critical Arts = Ultimate attacks that use the entire EX Gauge.",
                Developer = "CAPCOM",
                Publisher = "CAPCOM CO., LTD.",
                MinimumAge = 16,
                ImagePath = "StreetFighterV.jpg",
                TrailerUrl = @"https://youtu.be/KIOMBc6E_MQ",
                Genres = new List<Genre>() { g1, g2, g11 },
                GameRequirements = @"PROCESSOR : Intel Core i3-4160 @ 3.60GHz
GRAPHICS : NVIDIA GeForce GTX 480, GTX 570, GTX 670
MEMORY : 6 GB RAM
Storage : 50 GB
OS : Windows 7 64 - bit"
            };

            var game11 = new Game()
            {
                GameName = "ELDEN RING",
                ReleaseDate = new DateTime(2022, 2, 25),
                Description = @"Elden Ring is a dark fantasy RPG developed by FromSoftware and published thanks to Bandai Namco Entertainment. It is an epic and challenging adventure created by the developers of Demon's Souls in collaboration with the author of A Song of Ice and Fire series - R. R. Martin. Discover the secrets of the Lands Between and embark on the most heroic mission to discover the purpose of the magical artifact and your destiny. Elden Ring release is set for 2022.
	Story
	The Elden Ring is destroyed. Its fragments, scattered all over the world, arouse in the gods a desire to possess them. The incredible power of the artifacts transforms the gods into raging creatures taken over by madness. Return from exile and rise as one of those worthy of collecting the Elden Ring pieces.
	Gameplay
	Create a character and take care of every element of appearance, equipment, and personality. Rise as a fearless warrior or a clever treasure hunter. The Elden Ring game allows players to fully customize the heroes, which makes the gameplay even more unique.

	Explore the stunning open world on the foot or back of a trusted horse. Discover the six kingdoms of the Lands Between to learn scraps of the history of this mystical place. As with other soulslike titles, nothing is clear in this video game. It's up to you to gather information and understand it.",
                Developer = "FROMSOFTWARE, INC",
                Publisher = "BANDAI NAMCO ENTERTAINMENT",
                MinimumAge = 16,
                ImagePath = "eldenRing.jpg",
                TrailerUrl = @"https://www.youtube.com/watch?v=E3Huy2cdih0&t=1s",
                GameRequirements = @"MINIMUM
	OS Windows: 10
	PROCESSOR: INTEL CORE I5-8400 or AMD RYZEN 3 3300X
	MEMORY: 12 GB RAM
	GRAPHICS: NVIDIA GEFORCE GTX 1060 3 GB or AMD RADEON RX 580 4 GB
	STORAGE: 60 GB available space",
                Genres = new List<Genre>() { g1, g2, g3 }
            };

            var game12 = new Game()
            {
                GameName = "FAR CRY 6",
                ReleaseDate = new DateTime(2021, 10, 7),
                Description = @"This time it is about a dictatorship in a paradise. The place seems to be “frozen in time” because of the strict ruler, Anton Castillo. His aim is to bring Yara to its former glory. However, he will stop at nothing to realize his dreams.

	He wants his son, Diego, to follow him on his merciless path of taking full control of the island. The atmosphere is very dense, so the revolution is inevitable. Play as Dani Rojas, a citizen of Yara and a rebel fighting for freedom. Their (the protagonist can be either male or female) ultimate goal is to liberate their home.
	Gameplay
	As it is typical of the series, new Far Cry will welcome players with bunch of new weapons and tons of customization options. Attach new mods and upgrades in a very far-cryish style you love so much – sights made of empty cans, guns shooting nails, disc launcher, and many more.

	Don’t forget about your means of transport. Drive a car or even mount a horse (which is a novelty in the whole series) and explore the huge capital city of Esperanza, called “Lion’s Den” by the locals.",
                Developer = "UBISOFT",
                Publisher = "UBISOFT",
                MinimumAge = 16,
                ImagePath = "farCry6.jpg",
                TrailerUrl = @"https://www.youtube.com/watch?v=ILHFhLjIXpY",
                GameRequirements = @"MINIMUM
	OS: Windows 10 (64-bit)
	PROCESSOR: AMD Ryzen 3 1200 3.1 GHz / Intel Core i5-4460 3.2 GHz
	MEMORY: 8GB
	GRAPHICS: AMD RX 460 /NVIDIA GeForce GTX 960
	STORAGE: 60GB",
                Genres = new List<Genre>() { g1, g2, g7, g13 }
            };

            var game13 = new Game()
            {
                GameName = "DYING LIGHT 2: STAY HUMAN",
                ReleaseDate = new DateTime(2022, 2, 4),
                Description = @"Dying Light 2 is an upcoming survival horror action role-playing video game developed by Techland and set to be released in 2022 by Techland Publishing. It is the sequel to the Dying Light from 2015 and it takes place 15 years after the story presented in its predecessor. As an infected survivor, you will be tasked with bringing back the order and ensuring peace in the city – you are the last hope of the society being on the brink of the collapse.
	Story
	In Dying Light 2 game you are playing the role of an infected survivor, Aiden Caldwell, as he tries to make a change in The City, which serves as a home for the local groups of both infected and survivors alike. In the post-apocalyptic world, where people desperately fighting for resources can be as dangerous as the monsters lurking in the shadows, you are the last hope of the falling society, and your decisions will determine its fate.
	Gameplay
	Dying Light 2 Stay Human is a survival horror action RPG played from the first-person perspective. It takes place in a post-apocalyptic open-world environment which is four times bigger than it was in the previous game. It also changes according to certain decisions you make. As a result, certain parts of the world may become closed and inaccessible, encouraging multiple playthroughs to fully appreciate everything the game has to offer.

	Both parkour movement and melee combat are playing a huge role in the title, though ranged weapons such as crossbows and shotguns are also present. Weapons can be upgraded to increase their stats and improve their efficiency. The weapons are also susceptible to damage and will slowly lose their effectiveness – they can be broken if overused, forcing the player to be prepared to quickly find an alternative weapon to fight with. Zombies present in the game are weak to the UV light, which combined with the day and night cycle lets players deal with dangers in a more creative way, rather than blindly rushing forward.",
                Developer = "TECHLAND",
                Publisher = "TECHLAND PUBLISHING",
                MinimumAge = 16,
                ImagePath = "dyingLight2StayHuman.jpg",
                TrailerUrl = @"https://www.youtube.com/watch?v=UwJAAy7tPhE",
                GameRequirements = @"MINIMUM
	OS: Windows 7
	PROCESSOR: Intel Core i3-9100 / AMD Ryzen 3 2300X
	MEMORY: 8 GB RAM
	GRAPHICS: NVIDIA GeForce GTX 1050 Ti / AMD Radeon™ RX 560 (4GB VRAM)
	STORAGE: 60 GB available space",
                Genres = new List<Genre>() { g1, g2, g7, g13 }
            };

            var game14 = new Game()
            {
                GameName = "DETROIT: BECOME HUMAN",
                ReleaseDate = new DateTime(2000, 12, 30),
                Description = @"Interactive gameplay
	The gameplay of Detroit: Become Human draws upon Heavy Rain’s best features. Those who played the previous hit will recognize the mechanics easily while those who encounter Quantic Dream’s games for the first time will quickly grasp the essence.

	The interactive storyline revolves around exploring the environment, and learning how your actions and decisions may affect your and others’ destiny. Highly developed dialogue system is a line between the facts and evidence you already know and the ones you may discover.

	You will frequently find that the key to solve the issue may lay in a properly handled conversation. Each characters’ special traits and features will help you achieve their individual goals. Make use of Connor’s unique ability to immediately analyze complex data, connected to, for instance, the crime scenes.

	Check out Kara’s indispensable ability of speaking 300 different languages, or cooking more than 9000 dishes. Figure out how to make the best use of them and lead your characters to happiness. Regardless, whether you will align yourself with good or bad, as long as you make your protagonists happy, you will be able to freely follow your path.",
                Developer = "QUANTIC DREAM",
                Publisher = "QUANTIC DREAM",
                MinimumAge = 18,
                ImagePath = "detroitBecomeHuman.jpg",
                TrailerUrl = @"https://www.youtube.com/watch?v=MkmVsCj1xEQ",
                GameRequirements = @"MINIMUM
	OS: Windows 10 (64 bit)
	PROCESSOR: Intel Core i5-2300 @ 2.8 GHz or AMD Ryzen 3 1200 @ 3.1GHz or AMD FX-8350 @ 4.2GHz
	MEMORY: 8 GB RAM
	GRAPHICS: Nvidia GeForce GTX 780 or AMD HD 7950 with 3GB VRAM minimum (Support of Vulkan 1.1 required)
	STORAGE: 55 GB available space",
                Genres = new List<Genre>() { g2, g7 }
            };
            #endregion

            #region Products
            var product1 = new Product() { Game = game1, Platform = p1, ProductUnitPrice = 10.99m, IsAvailable = true };
            var product2 = new Product() { Game = game2, Platform = p2, ProductUnitPrice = 20.99m, IsAvailable = true };
            var product3 = new Product() { Game = game3, Platform = p3, ProductUnitPrice = 30.99m, IsAvailable = true };
            var product4 = new Product() { Game = game4, Platform = p4, ProductUnitPrice = 40.99m, IsAvailable = true };
            var product5 = new Product() { Game = game5, Platform = p5, ProductUnitPrice = 50.99m, IsAvailable = true };
            var product6 = new Product() { Game = game6, Platform = p6, ProductUnitPrice = 60.99m, IsAvailable = true };
            var product7 = new Product() { Game = game7, Platform = p1, ProductUnitPrice = 70.99m, IsAvailable = true };
            var product8 = new Product() { Game = game8, Platform = p2, ProductUnitPrice = 80.99m, IsAvailable = true };
            var product9 = new Product() { Game = game9, Platform = p3, ProductUnitPrice = 90.99m, IsAvailable = true };
            var product10 = new Product() { Game = game10, Platform = p4, ProductUnitPrice = 10.99m, IsAvailable = true };
            var product11 = new Product() { Game = game11, Platform = p5, ProductUnitPrice = 20.99m, IsAvailable = true };
            var product12 = new Product() { Game = game12, Platform = p6, ProductUnitPrice = 30.99m, IsAvailable = true };
            var product13 = new Product() { Game = game13, Platform = p1, ProductUnitPrice = 40.99m, IsAvailable = true };
            var product14 = new Product() { Game = game14, Platform = p2, ProductUnitPrice = 50.99m, IsAvailable = true };
            var product15 = new Product() { Game = game1, Platform = p2, ProductUnitPrice = 60.99m, IsAvailable = true };
            var product16 = new Product() { Game = game2, Platform = p3, ProductUnitPrice = 70.99m, IsAvailable = true };
            var product17 = new Product() { Game = game3, Platform = p4, ProductUnitPrice = 80.99m, IsAvailable = true };
            var product18 = new Product() { Game = game4, Platform = p5, ProductUnitPrice = 90.99m, IsAvailable = true };
            var product19 = new Product() { Game = game5, Platform = p6, ProductUnitPrice = 10.99m, IsAvailable = true };
            var product20 = new Product() { Game = game6, Platform = p1, ProductUnitPrice = 20.99m, IsAvailable = true };
            var product21 = new Product() { Game = game7, Platform = p2, ProductUnitPrice = 30.99m, IsAvailable = true };
            var product22 = new Product() { Game = game8, Platform = p3, ProductUnitPrice = 40.99m, IsAvailable = true };
            var product23 = new Product() { Game = game9, Platform = p4, ProductUnitPrice = 50.99m, IsAvailable = true };
            var product24 = new Product() { Game = game10, Platform = p5, ProductUnitPrice = 60.99m, IsAvailable = true };
            var product25 = new Product() { Game = game11, Platform = p6, ProductUnitPrice = 70.99m, IsAvailable = true };
            var product26 = new Product() { Game = game12, Platform = p1, ProductUnitPrice = 80.99m, IsAvailable = true };
            var product27 = new Product() { Game = game13, Platform = p2, ProductUnitPrice = 90.99m, IsAvailable = true };
            var product28 = new Product() { Game = game14, Platform = p3, ProductUnitPrice = 10.99m, IsAvailable = true };
            #endregion

            db.AddRange(g1, g2, g3, g4, g5, g6, g7, g8, g9, g10, g11, g12, g13, p1, p2, p3, p4, p5, p6, game1, game2, game3, game4, game5, game6, game7, game8, game9, game10, game11, game12, game13, game14, product1, product2, product3, product4, product5, product6, product7, product8, product9, product10, product11, product12, product13, product14, product15, product16, product17, product18, product19, product20, product21, product22, product23, product24, product25, product26, product27, product28);

            await db.SaveChangesAsync();
        }
    }
}