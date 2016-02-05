﻿#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

using AutoMapper;

using vindinium.Infrastructure.Behaviors.Map;
using vindinium.Infrastructure.Behaviors.Models;
using vindinium.Infrastructure.DTOs;

#endregion

namespace vindinium
{
    public class Server
    {
        private readonly string key;

        private readonly string map;

        private readonly string serverUrl;

        private readonly IMapper mapper;

        private readonly bool trainingMode;

        private readonly uint turns;

        private string playUrl;

        public Server()
        {
        }

        //if training mode is false, turns and DefaultMapBuilder are ignored8
        public Server(string key, bool trainingMode, uint turns, string serverURL, string map, IMapper mapper)
        {
            this.key = key;
            this.trainingMode = trainingMode;
            this.serverUrl = serverURL;
            this.mapper = mapper;

            //the reaons im doing the if statement here is so that i dont have to do it later
            if (trainingMode)
            {
                this.turns = turns;
                this.map = map;
            }
        }

        public string ViewUrl { get; private set; }

        public IMapNode MyHero { get; private set; }

        public List<IMapNode> AllCharacters { get; set; }

        public IEnumerable<IMapNode> Villians { get; private set; }

        public int CurrentTurn { get; private set; }

        public int MaxTurns { get; private set; }

        public bool Finished { get; private set; }

        public bool Errored { get; private set; }

        public string ErrorText { get; private set; }

        public IMapNode[,] Board { get; private set; }

        //initializes a new game, its syncronised
        public void CreateGame()
        {
            this.Errored = false;

            string uri;

            if (trainingMode)
            {
                uri = this.serverUrl + "/api/training";
            }
            else
            {
                uri = this.serverUrl + "/api/arena";
            }

            var myParameters = "key=" + key;
            if (trainingMode)
            {
                myParameters += "&turns=" + turns;
            }
            if (map != null)
            {
                myParameters += "&map=" + map;
            }

            //make the request
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/X-www-form-urlencoded";
                try
                {
                    var result = client.UploadString(uri, myParameters);
                    this.Deserialize(result);
                }
                catch (WebException exception)
                {
                    this.Errored = true;
                    using (var reader = new StreamReader(exception.Response.GetResponseStream()))
                    {
                        this.ErrorText = reader.ReadToEnd();
                    }
                }
            }
        }

        public string GetDirection(CoOrdinates currentLocation, CoOrdinates moveTo)
        {
            var direction = "Stay";
            if (moveTo == null)
            {
                return direction;
            }
            if (moveTo.X > currentLocation.X)
            {
                direction = "East";
            }
            else if (moveTo.X < currentLocation.X)
            {
                direction = "West";
            }
            else if (moveTo.Y > currentLocation.Y)
            {
                direction = "South";
            }
            else if (moveTo.Y < currentLocation.Y)
            {
                direction = "North";
            }

            return direction;
        }

        public void MoveHero(string direction)
        {
            var myParameters = "key=" + key + "&dir=" + direction;

            //make the request
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/X-www-form-urlencoded";

                try
                {
                    var result = client.UploadString(this.playUrl, myParameters);
                    this.Deserialize(result);
                }
                catch (WebException exception)
                {
                    this.Errored = true;
                    using (var reader = new StreamReader(exception.Response.GetResponseStream()))
                    {
                        this.ErrorText = reader.ReadToEnd();
                    }
                }
            }
        }

        public void CreateBoard(int size, string data)
        {
            //check to see if the Board list is already created, if it is, we just overwrite its values
            if (this.Board == null || this.Board.Length != size)
            {
                this.Board = new IMapNode[size, size];
            }

            var x = 0;
            var y = 0;
            var charData = data.ToCharArray();

            for (var i = 0; i < charData.Length; i += 2)
            {
                switch (charData[i])
                {
                    case '#':
                        this.Board[x, y] = new MapNode(Tile.IMPASSABLE_WOOD,x,y)
                                               {
                                                   Id = i,
                                                   Passable = false
                                                };
                        break;
                    case ' ':
                        this.Board[x, y] = new MapNode(Tile.FREE, x, y)
                        {
                            Id = i,
                            Passable = true,
                            Type = Tile.FREE,
                        };
                        break;
                    case '@':
                        switch (charData[i + 1])
                        {
                            case '1':
                                this.Board[x, y] = AllCharacters.First(h => h.Type == Tile.HERO_1);
                                break;
                            case '2':
                                this.Board[x, y] = AllCharacters.First(h => h.Type == Tile.HERO_2);
                                break;
                            case '3':
                                this.Board[x, y] = AllCharacters.First(h => h.Type == Tile.HERO_3);
                                break;
                            case '4':
                                this.Board[x, y] = AllCharacters.First(h => h.Type == Tile.HERO_4);
                                break;
                        }
                        break;
                    case '[':
                        this.Board[x, y] = new MapNode(Tile.TAVERN, x, y);
                        break;
                    case '$':
                        switch (charData[i + 1])
                        {
                            case '-':
                                this.Board[x, y] = new MapNode(Tile.GOLD_MINE_NEUTRAL, x, y);
                                break;
                            case '1':
                                this.Board[x, y] = new MapNode(Tile.GOLD_MINE_1, x, y);
                                break;
                            case '2':
                                this.Board[x, y] = new MapNode(Tile.GOLD_MINE_2, x, y);
                                break;
                            case '3':
                                this.Board[x, y] = new MapNode(Tile.GOLD_MINE_3, x, y);
                                break;
                            case '4':
                                this.Board[x, y] = new MapNode(Tile.GOLD_MINE_4, x, y);
                                break;
                        }
                        break;
                }

                //time to increment X and Y
                x++;
                if (x == size)
                {
                    x = 0;
                    y++;
                }
            }
        }

        private void Deserialize(string json)
        {
            var byteArray = Encoding.UTF8.GetBytes(json);
            var stream = new MemoryStream(byteArray);

            var ser = new DataContractJsonSerializer(typeof(GameResponse));
            var gameResponse = (GameResponse)ser.ReadObject(stream);

            this.playUrl = gameResponse.playUrl;
            this.ViewUrl = gameResponse.viewUrl;

            this.MyHero = mapper.Map<HeroNode>(gameResponse.hero);
            this.Villians = this.mapper.Map<List<VillianNode>>(gameResponse.game.heroes.Where(h => h.id != MyHero.Id));
            this.AllCharacters = new List<IMapNode>();
            AllCharacters.Add(MyHero);
            AllCharacters.AddRange(Villians);

            this.CurrentTurn = gameResponse.game.turn;
            this.MaxTurns = gameResponse.game.maxTurns;
            this.Finished = gameResponse.game.finished;

            this.CreateBoard(gameResponse.game.board.size, gameResponse.game.board.tiles);
            PopulateNodeParents();

            VisualizeMap(this);

        }

        private void VisualizeMap(Server server)
        {
            Console.Clear();
            for (int i = 0; i < server.Board.GetLength(0); i++)
            {
                for (int j = 0; j < server.Board.GetLength(1); j++)
                {
                    switch (server.Board[i, j].Type)
                    {
                        case Tile.FREE:
                            Console.Write('_');
                            break;
                        case Tile.GOLD_MINE_1:
                        case Tile.GOLD_MINE_2:
                        case Tile.GOLD_MINE_3:
                        case Tile.GOLD_MINE_4:
                        case Tile.GOLD_MINE_NEUTRAL:
                            Console.Write('$');
                            break;
                        case Tile.HERO_1:
                        case Tile.HERO_2:
                        case Tile.HERO_3:
                        case Tile.HERO_4:
                            Console.Write('@');
                            break;
                        case Tile.TAVERN:
                            Console.Write('B');
                            break;
                        case Tile.IMPASSABLE_WOOD:
                            Console.Write('#');
                            break;
                        default:
                            Console.Write(' ');
                            break;
                    }

                }
                Console.WriteLine();
            }
        }

        private void PopulateNodeParents()
        {
            for (int i = 0; i < this.Board.GetLength(0); i++)
            {
                for (int j = 0; j < this.Board.GetLength(1); j++)
                {
                    var node = this.Board[i, j];
                    var parents = GetParents(this.Board[i, j]);
                    node.Parents = parents;
                }
            }
        }

        private List<IMapNode> GetParents(IMapNode sourceMapNode)
        {
            List<IMapNode> results = new List<IMapNode>();
            if (sourceMapNode.Location.Y - 1 >= 0)
            {
                var north = this.Board[sourceMapNode.Location.X, sourceMapNode.Location.Y - 1];
                results.Add(north);
            }

            if (sourceMapNode.Location.Y + 1 <= this.Board.GetLength(0) - 1)
            {
                var south = this.Board[sourceMapNode.Location.X, sourceMapNode.Location.Y + 1];
                results.Add(south);
            }

            if (sourceMapNode.Location.X + 1 <= this.Board.GetLength(1) - 1)
            {
                var east = this.Board[sourceMapNode.Location.X + 1, sourceMapNode.Location.Y];
                results.Add(east);
            }

            if (sourceMapNode.Location.X - 1 >= 0)
            {
                var west = this.Board[sourceMapNode.Location.X - 1, sourceMapNode.Location.Y];
                results.Add(west);
            }
            return results;
        }


    }
}