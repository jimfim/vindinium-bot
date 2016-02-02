#region

using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

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

        private readonly bool trainingMode;

        private readonly uint turns;

        private string playUrl;

        public Server()
        {
        }

        //if training mode is false, turns and Map are ignored8
        public Server(string key, bool trainingMode, uint turns, string serverURL, string map)
        {
            this.key = key;
            this.trainingMode = trainingMode;
            this.serverUrl = serverURL;

            //the reaons im doing the if statement here is so that i dont have to do it later
            if (trainingMode)
            {
                this.turns = turns;
                this.map = map;
            }
        }

        public string ViewUrl { get; private set; }

        public Hero MyHero { get; private set; }

        public List<Hero> Heroes { get; private set; }

        public int CurrentTurn { get; private set; }

        public int MaxTurns { get; private set; }

        public bool Finished { get; private set; }

        public bool Errored { get; private set; }

        public string ErrorText { get; private set; }

        public Tile[,] Board { get; private set; }

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
                this.Board = new Tile[size, size];
            }

            //convert the string to the List<List<Tile>>
            var x = 0;
            var y = 0;
            var charData = data.ToCharArray();

            for (var i = 0; i < charData.Length; i += 2)
            {
                switch (charData[i])
                {
                    case '#':
                        this.Board[x, y] = Tile.IMPASSABLE_WOOD;
                        break;
                    case ' ':
                        this.Board[x, y] = Tile.FREE;
                        break;
                    case '@':
                        switch (charData[i + 1])
                        {
                            case '1':
                                this.Board[x, y] = Tile.HERO_1;
                                break;
                            case '2':
                                this.Board[x, y] = Tile.HERO_2;
                                break;
                            case '3':
                                this.Board[x, y] = Tile.HERO_3;
                                break;
                            case '4':
                                this.Board[x, y] = Tile.HERO_4;
                                break;
                        }
                        break;
                    case '[':
                        this.Board[x, y] = Tile.TAVERN;
                        break;
                    case '$':
                        switch (charData[i + 1])
                        {
                            case '-':
                                this.Board[x, y] = Tile.GOLD_MINE_NEUTRAL;
                                break;
                            case '1':
                                this.Board[x, y] = Tile.GOLD_MINE_1;
                                break;
                            case '2':
                                this.Board[x, y] = Tile.GOLD_MINE_2;
                                break;
                            case '3':
                                this.Board[x, y] = Tile.GOLD_MINE_3;
                                break;
                            case '4':
                                this.Board[x, y] = Tile.GOLD_MINE_4;
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
            // convert string to stream
            var byteArray = Encoding.UTF8.GetBytes(json);
            //byte[] byteArray = Encoding.ASCII.GetBytes(json);
            var stream = new MemoryStream(byteArray);

            var ser = new DataContractJsonSerializer(typeof(GameResponse));
            var gameResponse = (GameResponse)ser.ReadObject(stream);

            this.playUrl = gameResponse.playUrl;
            this.ViewUrl = gameResponse.viewUrl;

            this.MyHero = gameResponse.hero;
            this.Heroes = gameResponse.game.heroes;

            this.CurrentTurn = gameResponse.game.turn;
            this.MaxTurns = gameResponse.game.maxTurns;
            this.Finished = gameResponse.game.finished;

            this.CreateBoard(gameResponse.game.board.size, gameResponse.game.board.tiles);
        }
    }
}